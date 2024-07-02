using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using TMPro;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    //ポート名
    //例
    //Linuxでは/dev/ttyUSB0
    //windowsではCOM1
    //Macでは/dev/tty.usbmodem1421など
    public string portName = "COM8";
    public int baudRate    = 9600;

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;
    [SerializeField]
    TMP_Text errText;


    void Awake()
    {
        Debug.Log($"serial {portName}, {baudRate}");
        try{
            Open();
        }catch(System.IO.IOException e)
        {
            Debug.LogWarning($"[Serial] Port Read error {e}");
            errText?.SetText($"{portName} does not exist!");
        }
    }

    void Update()
    {
        if (isNewMessageReceived_) {
            Debug.Log("serial new data ");
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false;
    }

    void OnDestroy()
    {
        Close();
    }

    public void CloseSerial()
    {
        Debug.Log($"closing serial");
        Close();
    }

    private void Open()
    {
        Debug.Log("serial open");
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
         //または
         //serialPort_ = new SerialPort(portName, baudRate);
        serialPort_.Open();
        serialPort_.ReadTimeout = 100;

        isRunning_ = true;

        thread_ = new Thread(Read);
        thread_.Start();
    }

    private void Close()
    {
        isNewMessageReceived_ = false;
        isRunning_ = false;
        Debug.Log($"serial {thread_}, {thread_.IsAlive}");
        if (thread_ != null && thread_.IsAlive) {
            thread_.Join();
        }
        Debug.Log($"serial {thread_}, {thread_.IsAlive}");

        if (serialPort_ != null && serialPort_.IsOpen) {
            serialPort_.Close();
            serialPort_.Dispose();
        }
        Debug.Log($"serial {serialPort_}, {serialPort_.IsOpen}");

    }

    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen) {
            try {
                message_ = serialPort_.ReadLine();
                isNewMessageReceived_ = true;
            } catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
        Debug.Log("serial Exit loop");
    }

    public void Write(string message)
    {
        try {
            serialPort_.Write(message);
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}