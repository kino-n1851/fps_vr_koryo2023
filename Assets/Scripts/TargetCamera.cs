using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger");
        if(other.gameObject.tag=="bullet"){
            var currentRenderTexture = RenderTexture.active;
            RenderTexture.active = targetCamera.targetTexture;
            targetCamera.Render();

            RenderTexture.active = currentRenderTexture;
        }
    }
}
