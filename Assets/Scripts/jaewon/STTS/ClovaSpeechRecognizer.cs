using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;

public class ClovaSpeechRecognizer : MonoBehaviour
{
    private string clientId = "jgw5cfcbb4";  // ���̹� Ŭ���� �ֿܼ��� �߱޹��� Ŭ���̾�Ʈ ID
    private string clientSecret = "m3XpLu0sLXzlO2DA1IcCevQQtbefRqITJVukm2kd";  // ���̹� Ŭ���� �ֿܼ��� �߱޹��� Ŭ���̾�Ʈ ��ũ��
    private string apiUrl = "https://naveropenapi.apigw.ntruss.com/recog/v1/stt?lang=Kor";  // �ùٸ� API URL
    public string getResult;
    public GPT gpt;
    public STTS_save save;

    public void SendAudioClip(string filePath)
    {
        StartCoroutine(UploadAudio(filePath));
    }

    private IEnumerator UploadAudio(string filePath)
    {
        byte[] audioBytes = File.ReadAllBytes(filePath);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(audioBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/octet-stream");
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientId);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string result = ExtractValueFromJson(request.downloadHandler.text);
            Debug.Log(result);
            GameManager.Instance.talkings += result + "/";
            getResult = result;
            save.currentString += getResult + "/";
            gpt.isReadyToGetGPT = true;
            // ���� ó��
        }
        else
        {
            Debug.LogError("Error: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }
    string ExtractValueFromJson(string input)
    {
        // ���ڿ����� "text": �κ��� �����ϰ� ������ ���� ����
        string pattern = "\"text\":\"(.*?)\"";
        System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(input, pattern);

        if (match.Success)
        {
            return match.Groups[1].Value; // �׷� 1 (ĸó�� �κ�) ��ȯ
        }
        else
        {
            return null;
        }
    }
}
