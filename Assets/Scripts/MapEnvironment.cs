using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapEnvironment : MonoBehaviour
{
    [SerializeField]
    float WindSpeed;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject windParticle;
    Vector3 WindDirection;
    Vector3 playerPosition;
    private bool windOn = false;
    private float currentSpeed;
    public delegate void OnWindBlowDelegate(Vector3 windStrength);
    public event OnWindBlowDelegate OnWindBlowEvent;
    // Start is called before the first frame update
    void Start()
    {
        windOn = false;
        windParticle.SetActive(false);
        currentSpeed = 0;
        WindDirection = new Vector3(0, 0, 0);
        playerPosition = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        OnWindBlowEvent?.Invoke(currentSpeed*WindDirection);
    }

    public void ChangeWind()
    {
        if(windOn)
        {
            windOn = false;
            currentSpeed = 0;
            windParticle.SetActive(false);
        }else
        {
            windParticle.SetActive(true);
            windOn = true;
            currentSpeed = WindSpeed;
            int angle = Random.Range(0, 360);
            float x = Mathf.Cos(Mathf.Deg2Rad*angle);
            float z = Mathf.Sin(Mathf.Deg2Rad*angle);
            WindDirection = new Vector3(x, 0, z);
            windParticle.transform.localEulerAngles = new Vector3(0, -angle, 0);
            windParticle.transform.position = playerPosition + new Vector3(-x, 0, -z);
        }

    }
    public void SetWind(int angle, float windSpeed)
    {
        windParticle.SetActive(true);
        currentSpeed = windSpeed;
        float x = Mathf.Cos(Mathf.Deg2Rad*angle);
        float z = Mathf.Sin(Mathf.Deg2Rad*angle);
        WindDirection = new Vector3(x, 0, z);
        windParticle.transform.localEulerAngles = new Vector3(0, -angle, 0);
        windParticle.transform.position = playerPosition + new Vector3(-x*7, 0, -z*7);
    }
}
