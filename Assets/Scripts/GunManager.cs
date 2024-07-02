using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunManager: MonoBehaviour
{
    [SerializeField]
    GameObject sniperRifleObj;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject cartridgePrefab;
    [SerializeField]
    Animator slideAnim;
    [SerializeField]
    AudioClip shotSE;
    [SerializeField]
    Camera scopeCamera;
    [SerializeField]
    Image sniperReticle;
    [SerializeField]
    GameObject sniperScopeObj;
    [SerializeField]
    TMP_Text zeroInInfoText;
    [SerializeField]
    TMP_Text rangeInfoText;
    [SerializeField]
    GameObject InfoCanvasObj;
    [SerializeField]
    Image holoReticle;
    [SerializeField]
    GameObject holoSightObj;
    [SerializeField]
    float sniperMagnification;


    private RifleScope sniperScope;
    private RifleScope holoSight;
    private Gun sniperRifle;
    private float normalizedMagnification = 0.0f;

    public static readonly bool useRealGun = false;
    private float shotCicle = 0.1f;
    private float coolingTime = 0.0f;

    void Start()
    {
        sniperScope = new RifleScope(scopeCamera, 16.0f, 60.0f, 150.0f, sniperScopeObj, sniperReticle, zeroInInfoText, rangeInfoText, InfoCanvasObj, true);
        holoSight = new RifleScope(scopeCamera, 10.0f, 18.0f, 50.0f, holoSightObj, holoReticle);
        sniperRifle = new Gun(200.0, sniperRifleObj, bulletPrefab, cartridgePrefab, sniperScope, holoSight, Gun.Scopes.SNIPER, slideAnim, shotSE);
    }

    public void FireRifle()
    {
        if(coolingTime <= 0.0f)
        {
            sniperRifle.Shoot();
            coolingTime = shotCicle;
        }
    }

    public void MeasureRange()
    {
        sniperRifle.MeasureRange();
    }

    public void SwitchScope()
    {
        sniperRifle.SwitchScope();
    }

    public void ChangeMagnification(Vector2 v2)
    {
        //Debug.Log($"change zoom {v2}");
        normalizedMagnification = Mathf.Clamp(normalizedMagnification += v2.y*0.01f, 0, 1);
        sniperRifle.SetNormalizedMagnificationIF(normalizedMagnification);
    }
    void Update()
    {
        if(useRealGun)return;

    }
    void FixedUpdate()
    {
        if(coolingTime >= 0.0f)
        coolingTime -= Time.deltaTime;
    }
}

