using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysButton : MonoBehaviour
{
    [SerializeField] private UnityEvent OnButtonHit;
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="bullet"){
            OnButtonHit?.Invoke();
        }
    }
}

