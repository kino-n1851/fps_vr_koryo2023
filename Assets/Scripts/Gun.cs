using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun
{
    private double HorizontalBulletSpeed;
    private Transform muzzleTransform;
    private GameObject bulletPrefab;
    public RifleScope activeScope{get; private set;}
    private RifleScope holoSight;
    private RifleScope sniperScope;
    private Animator slideAnim;
    private GameObject chamber;
    private Transform chamberTransform;
    private GameObject cartridgePrefab;
    private AudioSource chamberAudio;
    private AudioClip shotSE;
    
    public enum Scopes
    {
        SNIPER,
        HOLO
    }

    public Gun(double HSpeed, GameObject rifleObj, GameObject _bulletPrefab, GameObject _cartridgePrefab, RifleScope _sniperScope, RifleScope _holoSight, Scopes activeScopeType, Animator _slideAnim, AudioClip _shotSE)
    {
        HorizontalBulletSpeed = HSpeed;
        muzzleTransform = rifleObj.transform.Find("muzzle").transform;
        chamber = rifleObj.transform.Find("Chamber").gameObject;
        chamberAudio = chamber.GetComponent<AudioSource>();
        chamberTransform = chamber.transform;
        bulletPrefab = _bulletPrefab;
        cartridgePrefab = _cartridgePrefab;
        sniperScope = _sniperScope;
        holoSight = _holoSight;
        SetActiveScope(activeScopeType);
        slideAnim = _slideAnim;
        shotSE = _shotSE;
    }

    public void Shoot()
    {
        slideAnim.SetTrigger("BlowBackTrigger");
        double expectedTime = activeScope.GetZeroInDistance()/HorizontalBulletSpeed;
        double initialYVelocity = -Physics.gravity.y*expectedTime/2.0f;

        GameObject bulletObj = GameObject.Instantiate(bulletPrefab) as GameObject;
        bulletObj.transform.position = muzzleTransform.position;
        bulletObj.transform.rotation = muzzleTransform.rotation;
        bulletObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GameObject cartridgeObj = GameObject.Instantiate(cartridgePrefab) as GameObject;
        cartridgeObj.transform.position = chamberTransform.position;
        cartridgeObj.transform.rotation = chamberTransform.rotation;
        bulletObj.GetComponent<Rigidbody>().velocity = muzzleTransform.forward*(float)HorizontalBulletSpeed + muzzleTransform.up*(float)initialYVelocity;
        cartridgeObj.GetComponent<Rigidbody>().velocity = chamberTransform.right*2.0f;
        cartridgeObj.GetComponent<Rigidbody>().AddTorque(chamberTransform.up*1.0f);
        if(!GunManager.useRealGun)
        {
            chamberAudio?.PlayOneShot(shotSE);
        }
    }

    public void SetActiveScope(Scopes scope)
    {
        switch(scope)
        {
            case Scopes.SNIPER:
                activeScope = sniperScope;
                sniperScope.Enable_();
                holoSight.Disable_();
                break;
            
            case Scopes.HOLO:
                activeScope = holoSight;
                holoSight.Enable_();
                sniperScope.Disable_();
                break;
        }
        activeScope.SetNormalizedMagnification(0.0f);
    }

    public void SwitchScope()
    {
        switch(activeScope.scopeType)
        {
            case Scopes.SNIPER:
                SetActiveScope(Scopes.HOLO);
                break;

            case Scopes.HOLO:
                SetActiveScope(Scopes.SNIPER);
                break;
        }
    }

    public void MeasureRange()
    {
        if(activeScope.hasRangeFinder)
        {
            float range = activeScope.MeasureRange(muzzleTransform);
            Debug.Log($"range: {range}");
        }else{
            Debug.LogWarning($"has no rangefinder!");
        }
    }

    public void SetNormalizedMagnificationIF(float normalizedMagnification)
    {//設計ミス?
        activeScope.SetNormalizedMagnification(normalizedMagnification);
    }

}

public class RifleScope
{
    private Camera scopeCamera;
    private float minMagnification = 8.0f;
    private float maxMagnification = 32.0f;
    private double zeroInDistance = 100.0f;
    private float defaultFoV;
    private float magnification;
    private GameObject scopeObj;
    private Image reticleImage;
    private TMP_Text zeroInInfoText = null;
    private TMP_Text rangeInfoText = null;
    private GameObject InfoCanvasObj;
    public bool hasRangeFinder {get;} = false;
    public Gun.Scopes scopeType {get; private set;}

    public RifleScope(Camera _scopeCamera, float _minMagnification, float _maxMagnification, double _zeroInDistance, GameObject _scopeObj, Image _reticleImage)
    {
        scopeCamera = _scopeCamera;
        minMagnification = _minMagnification;
        maxMagnification = _maxMagnification;
        magnification = _minMagnification;
        defaultFoV = 80.0f;//Camera.main.fieldOfView;
        scopeObj = _scopeObj;
        reticleImage = _reticleImage;
        SetZeroInDistance(_zeroInDistance);
        scopeType = Gun.Scopes.HOLO;
    }
    public RifleScope(Camera _scopeCamera, float _minMagnification, float _maxMagnification, double _zeroInDistance, GameObject _scopeObj, Image _reticleImage, TMP_Text _zeroInText, TMP_Text _rangeInfoText, GameObject _InfoCanvasObj, bool _hasRangeFinder)
    {
        scopeCamera = _scopeCamera;
        minMagnification = _minMagnification;
        maxMagnification = _maxMagnification;
        magnification = _minMagnification;
        defaultFoV = 80.0f;//Camera.main.fieldOfView;
        scopeObj = _scopeObj;
        reticleImage = _reticleImage;
        zeroInInfoText = _zeroInText;
        rangeInfoText = _rangeInfoText;
        InfoCanvasObj = _InfoCanvasObj;
        hasRangeFinder = _hasRangeFinder;
        SetZeroInDistance(_zeroInDistance);
        scopeType = Gun.Scopes.SNIPER;
    }

    public void SetMagnification(float _magnification)
    {
        magnification = Mathf.Clamp(_magnification, minMagnification, maxMagnification);
        scopeCamera.fieldOfView = defaultFoV/magnification;
    }
    public void SetNormalizedMagnification(float normalizedMagnification)
    {
        float magnificationRange = maxMagnification - minMagnification;
        magnification = Mathf.Clamp(normalizedMagnification*magnificationRange + minMagnification, minMagnification, maxMagnification);
        scopeCamera.fieldOfView = defaultFoV/magnification;
    }

    public void Enable_()
    {
        scopeObj.SetActive(true);
        reticleImage.enabled = true;
        InfoCanvasObj?.SetActive(true);
        this.SetMagnification(magnification);
    }

    public void Disable_()
    {
        scopeObj.SetActive(false);
        reticleImage.enabled = false;
        InfoCanvasObj?.SetActive(false);
    }

    public void SetZeroInDistance(double distance)
    {
        zeroInDistance = distance;
        zeroInInfoText?.SetText($"{distance.ToString("f0")}m");
    }

    public double GetZeroInDistance()
    {
        return zeroInDistance;
    }

    public float MeasureRange(Transform muzzleTransform)
    {
        RaycastHit hit;
        float maxDistance = 400.0f;
        float range = float.NegativeInfinity;
        if(Physics.Raycast(muzzleTransform.position, muzzleTransform.forward, out hit, maxDistance))
        {
            range = hit.distance;
        }
        if(rangeInfoText != null)
        {
            if(range >= 0)
            {
                rangeInfoText.SetText($"{range.ToString("f0")}m");
                rangeInfoText.color = Color.green;
            }
            else{
                rangeInfoText.SetText($"---m");
                rangeInfoText.color = Color.red;
            }
        }
        return range;
    }

}
