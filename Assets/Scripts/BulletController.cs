using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float windDrag = 0;
    [SerializeField]
    GameObject bulletHolePrefab;
    [SerializeField]
    GameObject circleObjectPrefab;
    [SerializeField]
    AudioSource speaker;
    [SerializeField]
    AudioClip NormalSE;
    [SerializeField]
    AudioClip MetalSE;
    MapEnvironment mapEnvironment;
    float elapsedTime = 0;
    private Rigidbody mRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        mapEnvironment = GameObject.Find("MapEnvironment").GetComponent<MapEnvironment>();
        mRigidBody = this.GetComponent<Rigidbody>();
        mapEnvironment.OnWindBlowEvent += OnWindBlow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        mapEnvironment.OnWindBlowEvent -= OnWindBlow;
    }

    private void DestroyWithoutAudio()
    {
        Component[] components = this.gameObject.GetComponents<Component>();
        foreach(var component in components)
            if(component.GetType().Name == "AudioSource")
                continue;
            else if(component.GetType().Name == "Transform")
                continue;
            else
                Destroy(component);
    }
    private void MakeBulletHole(Transform hitTrans)
    {
        GameObject holeProjector = Instantiate(bulletHolePrefab);
        holeProjector.transform.position = hitTrans.position;
        holeProjector.transform.rotation = hitTrans.rotation;
    }
    private void MakeBulletHole(Transform hitTrans, Transform parent)
    {
        GameObject holeProjector = Instantiate(bulletHolePrefab, parent);
        holeProjector.transform.position = hitTrans.position;
        holeProjector.transform.rotation = hitTrans.rotation;
    }

    private void MakeHitCircle(Transform hitTrans)
    {
        GameObject circleObject = Instantiate(circleObjectPrefab);
        circleObject.transform.position = hitTrans.position;
        circleObject.transform.rotation = hitTrans.rotation;
    }
    private void MakeHitCircle(Transform hitTrans, Transform parent)
    {
        GameObject circleObject = Instantiate(circleObjectPrefab, parent);
        circleObject.transform.position = hitTrans.position;
        circleObject.transform.rotation = hitTrans.rotation;
    }

    public void OnWindBlow(Vector3 windStrength)
    {
        //Debug.Log($"{windStrength*windDrag}");
        mRigidBody.AddForce(windStrength*windDrag);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="TrainTarget"){
            speaker?.PlayOneShot(NormalSE);
            Transform hitTrans = this.gameObject.transform;
            GameObject moveGroup = collision.gameObject.transform.parent.gameObject;
            if (moveGroup.name == "MoveGroup")
            {
                //的の移動に対応
                MakeBulletHole(hitTrans, moveGroup.transform);
                MakeHitCircle(hitTrans, moveGroup.transform);
            }else{
                MakeBulletHole(hitTrans);
                MakeHitCircle(hitTrans);
            }
            Destroy(this.gameObject, 1.5f);
            DestroyWithoutAudio();
        }
        if(collision.gameObject.tag=="Markable")
        {
            speaker?.PlayOneShot(NormalSE);
            Transform hitTrans = this.gameObject.transform;
            GameObject holeProjector = Instantiate(bulletHolePrefab);
            holeProjector.transform.position = hitTrans.position;
            holeProjector.transform.rotation = hitTrans.rotation;
            Destroy(this.gameObject, 1.5f);
            DestroyWithoutAudio();
        }
        if(collision.gameObject.tag=="Metal")
        {
            speaker?.PlayOneShot(MetalSE);
            Transform hitTrans = this.gameObject.transform;
            GameObject holeProjector = Instantiate(bulletHolePrefab);
            holeProjector.transform.position = hitTrans.position;
            holeProjector.transform.rotation = hitTrans.rotation;
            Destroy(this.gameObject, 1.5f);
            DestroyWithoutAudio();
        }
        
        if(collision.gameObject.tag=="Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
