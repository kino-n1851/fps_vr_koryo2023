using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGame : MonoBehaviour
{
    [SerializeField]
    GameObject targetPrefab;
    [SerializeField]
    AudioSource speaker;
    [SerializeField]
    AudioClip StartSE;
    [SerializeField]
    TMP_Text countText;
    [SerializeField]
    GameObject trainObject;
    [SerializeField]
    MapEnvironment mapEnvironment;
    [SerializeField]
    TMP_Text FinishText;
    [SerializeField]
    PlayerController playerController;
    bool IsStarted;
    bool RemainTarget;
    int wave = 4;
    int passedWave = 0;
    int TargetCount = 5;
    float passedTime = 0;
    float score;
    // Start is called before the first frame update
    void Start()
    {
        IsStarted = false;
        RemainTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(IsStarted)
        {
            if(!RemainTarget && passedWave <= wave)
            {
                Debug.LogWarning($"spawn");
                speaker?.PlayOneShot(StartSE);
                StartCoroutine(StartAction(GenTargets));
                passedWave ++;
                RemainTarget = true;
            }

        }
    }
    public void EndGame()
    {
        int shotCount = playerController.GetShotCount();
        score -= shotCount*10;
        FinishText?.SetText($"ゲームクリア！ \n {score.ToString("f0")}点");
    }

    public void GameStart()
    {
        mapEnvironment.SetWind(70, 50.0f);
        trainObject.SetActive(false);
        playerController.GameStart();
        speaker?.PlayOneShot(StartSE);
        StartCoroutine(StartAction(GenTargets));
    }

    public void GenTargets()
    {
        TargetCount = 5;
        Debug.LogWarning("gentarget");
        GameObject target1 = Instantiate(targetPrefab, this.transform) as GameObject;
        GameObject target2 = Instantiate(targetPrefab, this.transform) as GameObject;
        GameObject target3 = Instantiate(targetPrefab, this.transform) as GameObject;
        GameObject target4 = Instantiate(targetPrefab, this.transform) as GameObject;
        GameObject target5 = Instantiate(targetPrefab, this.transform) as GameObject;
        target1.transform.localPosition = new Vector3(UnityEngine.Random.Range(-30, 0), UnityEngine.Random.Range(3, 16), 180);
        target2.transform.localPosition = new Vector3(UnityEngine.Random.Range(-30, 0), UnityEngine.Random.Range(3, 16), 200);
        target3.transform.localPosition = new Vector3(UnityEngine.Random.Range(-30, 0), UnityEngine.Random.Range(3, 16), 250);
        target4.transform.localPosition = new Vector3(UnityEngine.Random.Range(-30, 0), UnityEngine.Random.Range(3, 16), 220);
        target5.transform.localPosition = new Vector3(UnityEngine.Random.Range(-30, 0), UnityEngine.Random.Range(3, 16), 240);
        target1.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().OnButtonHit += OnHitButton;
        target2.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().OnButtonHit += OnHitButton;
        target3.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().OnButtonHit += OnHitButton;
        target4.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().OnButtonHit += OnHitButton;
        target5.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().OnButtonHit += OnHitButton;
        target1.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().SetSpeed(8.0f);
        target2.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().SetSpeed(4.0f);
        target3.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().SetSpeed(10.0f);
        target4.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().SetSpeed(6.0f);
        target5.transform.Find("Target").gameObject.GetComponent<MiniGameTarget>().SetSpeed(8.0f);

    }
    private IEnumerator StartAction(Action ev)
    {
        float elapsedTime = 0.0f;
        bool three = false;
        bool two = false;
        bool one = false;
        while (elapsedTime < 4.0f)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 0.0f && !three){
                three = true;
                countText.SetText("3");
            }
            if (elapsedTime >= 1.0f && !two){
                two = true;
                countText.SetText("2");
            }
            if (elapsedTime >= 2.0f && !one){
                one = true;
                countText.SetText("1");
            }
            yield return new WaitForEndOfFrame();
        }
        countText.SetText("");
        IsStarted = true;
        RemainTarget = true;
        ev?.Invoke();
    }

    public void OnHitButton(float zPos)
    {
        TargetCount--;
        Debug.LogWarning($"HitButton {TargetCount}");
        if(TargetCount <= 0)
        {
            RemainTarget = false;
            score += 100;
            score += zPos;
            if(passedWave >= wave)
            {
                IsStarted = false;
                EndGame();
            }
        }
    }
}
