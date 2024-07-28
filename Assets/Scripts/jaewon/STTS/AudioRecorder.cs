using UnityEngine;
using System.Collections;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip audioClip;
    private string microphone;
    public ClovaSpeechRecognizer clovaSpeechRecognizer;
    public bool isRecording = false;
    public bool istalking = false;
    void Start()
    {
        microphone = Microphone.devices[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            istalking = true;
            isRecording = true;
            StartRecording();
            Debug.Log("��ȭ ����");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopRecording();
            isRecording = false;
            Debug.Log("��ȭ ����");
            clovaSpeechRecognizer.SendAudioClip(Application.persistentDataPath + "/audio.wav");
        }
    }
    public void StartRecording()
    {
        audioClip = Microphone.Start(microphone, false, 10, 44100);
    }

    public void StopRecording()
    {
        Microphone.End(microphone);
        SaveClip(audioClip);
    }

    private void SaveClip(AudioClip clip)
    {
        string filePath = Application.persistentDataPath + "/audio.wav";
        SavWav.Save(filePath, clip);
        Debug.Log("��ȭ ���̺� �Ϸ�");
    }
}
