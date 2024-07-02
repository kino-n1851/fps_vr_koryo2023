using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWShot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject fwp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShotFW()
    {
        GameObject fwo = Instantiate(fwp) as GameObject;
        fwo.transform.position = this.transform.position;
        fwo.GetComponent<Rigidbody>().AddForce(0, 50, 0, ForceMode.Impulse);
    }
}
