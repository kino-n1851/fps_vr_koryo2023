using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject keyMousePlayer;
    [SerializeField]
    GameObject keyMouseRifle;
    [SerializeField]
    GameObject VRPlayer;
    [SerializeField]
    GameObject vrRifle;
    Quaternion cameraRot;
    Quaternion plRot;
    float minX = -89f, maxX = 89f;
    float mouseSens = 1.0f;
    private Vector2 _currentLookInputValue = Vector2.zero;
    GunManager activeGun;
    bool isGameStarted = false;
    int shotCount = 0;
    enum PlayMode
    {
        KEYBOARD_MOUSE,
        VR,
    }
    PlayMode playMode = PlayMode.VR;
    // Start is called before the first frame update
    void Start()
    {
        switch(playMode)
        {
            case PlayMode.KEYBOARD_MOUSE:
                keyMousePlayer.SetActive(true);
                VRPlayer.SetActive(false);
                activeGun = keyMouseRifle.GetComponent<GunManager>();
                cameraRot = keyMouseRifle.transform.localRotation;
                plRot = keyMousePlayer.transform.localRotation;
                break;

            case PlayMode.VR:
                //keyMousePlayer.SetActive(false);
                VRPlayer.SetActive(true);
                activeGun = vrRifle.GetComponent<GunManager>();
                OVRManager.foveatedRenderingLevel = OVRManager.FoveatedRenderingLevel.High;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if(isGameStarted)
            {
                shotCount++;
            }
            activeGun.FireRifle();
        }
    }

    public void OnFire()
    {
        if(isGameStarted)
        {
            shotCount++;
        }
        activeGun.FireRifle();
    }

    public void GameStart()
    {
        isGameStarted = true;
    }

    public int GetShotCount()
    {
        return shotCount;
    }

    public void OnMoveView(InputAction.CallbackContext context)
    {
        if(playMode == PlayMode.VR)return;

        _currentLookInputValue = mouseSens*Time.deltaTime*context.ReadValue<Vector2>();
        cameraRot *= Quaternion.Euler(-_currentLookInputValue.y, 0, 0);
        plRot *= Quaternion.Euler(0, _currentLookInputValue.x, 0);
        cameraRot = ClampRotation(cameraRot);
        keyMousePlayer.transform.localRotation = plRot;
        keyMouseRifle.transform.localRotation = cameraRot;
    }

    public void OnCursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnEscapeWindow()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnMeasureRange(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            activeGun.MeasureRange();
        }
    }

    public void OnSwitchScope(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            activeGun.SwitchScope();
        }
    }

    public void OnChangeMagnification(InputAction.CallbackContext context)
    {
        activeGun.ChangeMagnification(context.ReadValue<Vector2>());
    }

    private Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX,minX,maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
