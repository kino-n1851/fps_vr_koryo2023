using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorController : MonoBehaviour
{
    private float elapsedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 5.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
