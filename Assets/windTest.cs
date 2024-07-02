using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windTest : MonoBehaviour
{
    [SerializeField]
    MapEnvironment mapEnvironment;
    Rigidbody mRigidbody;
    float windDrag = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        mapEnvironment.OnWindBlowEvent += OnWindBlow;
        mRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnWindBlow(Vector3 windStrength)
    {
        //Debug.Log($"{windStrength*windDrag}");
        mRigidbody.AddForce(windStrength*windDrag);
    }
}
