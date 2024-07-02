using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip cartridgeSE;
    private bool isHitGround = false;
    float elapsedTime = 0;
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
        if(isHitGround && elapsedTime >= 2.0f)
        {
            Destroy(this.gameObject);
        }
        if(elapsedTime >= 6.0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ground" && !isHitGround){
            audioSource?.PlayOneShot(cartridgeSE);
            isHitGround = true;
        }
    }
}
