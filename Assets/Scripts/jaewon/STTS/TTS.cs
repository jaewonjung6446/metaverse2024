using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Net;
using System;
using System.Text;
using Newtonsoft.Json;
using UnityEditor.PackageManager;
using UnityEditor;
using UnityEngine.UIElements;
public enum _sentiment
{
    nyuna = 0,
    ndain = 1,
    nminsang = 2,
    nsangdo = 3
}

public class TTS : MonoBehaviour
{
    public bool isReadyToTTS = false;
    public GPT gpt;
    public AudioRecorder audioRecorder;
    Coroutine a;
    string url = "https://naveropenapi.apigw.ntruss.com/tts-premium/v1/tts";
    string client_id = "jgw5cfcbb4";
    string client_secret = "m3XpLu0sLXzlO2DA1IcCevQQtbefRqITJVukm2kd";
    string avatar_name;
    public void Start()
    {
        avatar_name = PlayerCanSee.instance.closestObject.gameObject.name;
        TextToSpeech("�ȳ��ϼ���");
    }
    public void Update()
    {
        if (isReadyToTTS)
        {
            TextToSpeech(gpt.result);
            StartCoroutine(SendSentimentRequest(gpt.result));
        }
    }
    public void TextToSpeech(string sentence)
    {
        Debug.Log("���� ���� : " + sentence);
        AudioSource audio = GetComponent<AudioSource>();

        // API �߱��ϰ� ���� client_id, client_secret �ۼ�

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("X-NCP-APIGW-API-KEY-ID", client_id);
        request.Headers.Add("X-NCP-APIGW-API-KEY", client_secret);
        request.Method = "POST";

        // ��Ҹ� �̸� ���� 
        string avatar_name = PlayerCanSee.instance.closestObject.gameObject.name;
        byte[] byteDataParams = Encoding.UTF8.GetBytes($"speaker={avatar_name}&volume=0&speed=0&pitch=0&format=wav&text={sentence}");

        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteDataParams.Length;

        // TTS ��û
        Stream st = request.GetRequestStream();
        st.Write(byteDataParams, 0, byteDataParams.Length);
        st.Close();
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        // stream ���� ��û �� ���� 
        Stream input = response.GetResponseStream();

        // stream to byte
        var memoryStream = new MemoryStream();
        input.CopyTo(memoryStream);
        byte[] byteArray = memoryStream.ToArray();
        float[] f = ConvertByteToFloat(byteArray);

        // byte to audioclip 
        using (Stream s = new MemoryStream(byteArray))
        {
            AudioClip audioClip = AudioClip.Create("tts123", f.Length, 1, 24000, false);
            audioClip.SetData(f, 0);
            audio.clip = audioClip;
            audio.Play();
            TalkingUICon.instance.isTTS = false;
            TalkingUICon.instance.istalking = false;
            TalkingUICon.instance.isWaiting = true;
            isReadyToTTS = false;
            a = null;
            //if (a == null) StartCoroutine(SetisTTS());
        }
    }
    public IEnumerator SetisTTS()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        TalkingUICon.instance.isTTS = false;
        TalkingUICon.instance.istalking = false;
        TalkingUICon.instance.isWaiting = true;
        isReadyToTTS = false;
        a = null;
    }
    private float[] ConvertByteToFloat(byte[] array)
    {
        float[] floatArr = new float[array.Length / 2];
        for (int i = 0; i < floatArr.Length; i++)
        {
            floatArr[i] = BitConverter.ToInt16(array, i * 2) / 32768.0f;
        }
        return floatArr;
    }
    IEnumerator SendSentimentRequest(string content)
    {
        // ���� �м� API�� URL
        string sentimentApiUrl = "https://naveropenapi.apigw.ntruss.com/sentiment-analysis/v1/analyze";

        // JSON ������ ����
        var jsonData = new { content = content };
        string jsonBody = JsonConvert.SerializeObject(jsonData);

        // UnityWebRequest ����
        UnityWebRequest request = new UnityWebRequest(sentimentApiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", client_id); // ���� �м� API�� Ŭ���̾�Ʈ ID Ȯ�� �ʿ�
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", client_secret); // ���� �м� API�� ��ũ�� Ű Ȯ�� �ʿ�

        // API ȣ�� �� ���� ���
        yield return request.SendWebRequest();

        // ���� ��� ó��
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Raw Response ���
            Debug.Log("Raw Response: " + request.downloadHandler.text);

            try
            {
                // JSON ���� �Ľ�
                var responseJson = JsonConvert.DeserializeObject<SentimentResponse>(request.downloadHandler.text);

                if (responseJson != null)
                {
                    // Document�� sentiment �� confidence ���
                    Debug.Log("Document Sentiment: " + responseJson.document.sentiment);
                    Debug.Log("Document Confidence - Positive: " + responseJson.document.confidence.positive);
                    Debug.Log("Document Confidence - Neutral: " + responseJson.document.confidence.neutral);
                    Debug.Log("Document Confidence - Negative: " + responseJson.document.confidence.negative);
                    GetMaxPositive(responseJson.document.confidence.positive);
                }
                else
                {
                    Debug.LogError("responseJson�� null�Դϴ�.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("JSON �Ľ� ����: " + ex.Message);
            }
        }
    }
    public void GetMaxPositive(float positive)
    {
        string name = avatar_name;
        int positiveValue = -1;
        _sentiment result;
        if (Enum.TryParse(name, out result))
        {
            // ��ȯ�� enum ���� �����Ǵ� ���� ȣ��
            positiveValue = (int)result;
            Debug.Log(name);
        }
        else
        {
            Debug.LogError("��ȯ ����: ���ڿ��� enum ���� ��ġ���� �ʽ��ϴ�.");
        }
        if (positive > GameManager.Instance.sentiment[positiveValue])
        {
            Debug.Log("�ִ밪 ��ȭ" + positive);
            GameManager.Instance.sentiment[positiveValue] = positive;
        }
        Debug.Log(name + "���� �ִ� = " + GameManager.Instance.sentiment[positiveValue]);
    }
}

// ���� �����Ϳ� ���� Ŭ����
[System.Serializable]
public class SentimentResponse
{
    public Document document { get; set; }
    public List<Sentence> sentences { get; set; }
}

[System.Serializable]
public class Document
{
    public string sentiment { get; set; }
    public Confidence confidence { get; set; }
}

[System.Serializable]
public class Sentence
{
    public string content { get; set; }
    public int offset { get; set; }
    public int length { get; set; }
    public string sentiment { get; set; }
    public Confidence confidence { get; set; }
    public List<Highlight> highlights { get; set; }
}

[System.Serializable]
public class Confidence
{
    public float negative { get; set; }
    public float positive { get; set; }
    public float neutral { get; set; }
}

[System.Serializable]
public class Highlight
{
    public int offset { get; set; }
    public int length { get; set; }
}
