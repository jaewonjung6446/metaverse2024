using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalkingUICon : MonoBehaviour
{
    public AudioRecorder audioRecorder;
    public TTS tts;
    public GPT gpt;
    public Text instruction;
    public Image Recording;
    public void Update()
    {
        if (!audioRecorder.istalking)
        {
            instruction.text = "Space�� ���� ���� �ɾ����!";
        }
        if(audioRecorder.istalking && audioRecorder.isRecording)
        {
            Recording.gameObject.SetActive(true);
        }else if(audioRecorder.istalking && !audioRecorder.isRecording)
        {
            instruction.text = "���� ��...";
            Recording.gameObject.SetActive(false);
        }
    }
}