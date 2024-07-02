using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetRangeSelector : MonoBehaviour
{
    [SerializeField]
    float Distance;
    [SerializeField]
    GameObject trainTarget;
    [SerializeField]
    TMP_Text RangeText;
    float textDistance;
    // Start is called before the first frame update
    void Start()
    {
        textDistance = Distance/10.0f;
        textDistance = Mathf.Round(textDistance);
        textDistance *= 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="bullet"){
            RangeText?.SetText($"{textDistance.ToString("f0")}m");
            trainTarget.transform.localPosition = new Vector3(trainTarget.transform.localPosition.x, 0, Distance);
        }
    }
}
