using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SerialManager : MonoBehaviour
{
    public SerialHandler serialHandler;

    [SerializeField]
    TMP_Text text;
    [SerializeField] private UnityEvent OnTriggerPull;
    [SerializeField] private UnityEvent OnZoomIn;
    [SerializeField] private UnityEvent OnZoomOut;
    [SerializeField] private UnityEvent OnMeasureRange;
    [SerializeField] private UnityEvent OnChangeScope;
    //受信用変数
    public float data;              //受信データのfloat型版変数
    string receive_data;            //受信した生データを入れる変数

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    //データを受信したら
    void OnDataReceived(string message)
    {
        Debug.Log($"受信データ: {message}");
        text?.SetText(message);
        string trimMessage = message.TrimEnd('\r', '\n');
        switch(trimMessage)
        {
            case "shot":
                OnTriggerPull?.Invoke();
                break;

            case "zoomIn":
                OnZoomIn?.Invoke();
                break;

            case "zoomOut":
                OnZoomOut?.Invoke();
                break;

            case "measure":
                OnMeasureRange?.Invoke();
                break;

            case "change":
                OnChangeScope?.Invoke();
                break;
        }
    }

    void OnDestroy()
    {
        serialHandler.OnDataReceived -= OnDataReceived;
    }

    private void Update()
    {

    }

}