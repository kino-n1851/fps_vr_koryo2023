using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameTarget : MonoBehaviour
{
    float elapsedTime;
    public delegate void OnHitTargetDelegate(float time);
    public event OnHitTargetDelegate OnButtonHit;
    public float moveSpeed;
    [SerializeField]
    GameObject targetObject;
    Transform targetTransform;
    void Start()
    {
        elapsedTime = 0;
        targetTransform = this.transform.parent.transform;
        Debug.LogWarning(targetTransform.gameObject);
    }

    public void SetSpeed(float speed){
        moveSpeed = speed;
    }

    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        targetTransform.localPosition = targetTransform.localPosition + new Vector3(0, 0, -moveSpeed*Time.deltaTime);
        if(targetTransform.localPosition.z <= 10)
        {
            OnButtonHit?.Invoke(5.0f);
            Destroy(targetObject);
        }
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="bullet"){
            OnButtonHit?.Invoke(targetTransform.localPosition.z);
            Debug.LogWarning("destroy");

            Destroy(targetObject);
        }
    }
}

