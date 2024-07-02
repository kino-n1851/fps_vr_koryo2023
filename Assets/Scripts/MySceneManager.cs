using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    FadeInOut fadeController;
    [SerializeField]
    SerialHandler serialHandler;

    public void LoadTrainScene()
    {
        serialHandler?.CloseSerial();
        fadeController.ChangeScene(new Action(() => {
            Debug.LogWarning("Loading Train Scene");
            SceneManager.LoadScene("TrainScene");
        }), 1.0f);
    }

    void Start()
    {
        fadeController.StartScene(1.0f);
    }

}
