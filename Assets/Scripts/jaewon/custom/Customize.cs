using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using System.Net;

public class Customize : MonoBehaviour
{
    public InputField inputField;
    public Text text;
    public string character;
    private AudioClip audioClip;
    private string microphone;
    private string clientId = "jgw5cfcbb4";  // ���̹� Ŭ���� �ֿܼ��� �߱޹��� Ŭ���̾�Ʈ ID
    private string clientSecret = "m3XpLu0sLXzlO2DA1IcCevQQtbefRqITJVukm2kd";  // ���̹� Ŭ���� �ֿܼ��� �߱޹��� Ŭ���̾�Ʈ ��ũ��
    private string apiUrl = "https://naveropenapi.apigw.ntruss.com/recog/v1/stt?lang=Kor";  // �ùٸ� API URL
    public string getResult;
    private string apiKey;
    public string result;
    private void Start()
    {
        inputField.onEndEdit.AddListener(SetCharacter);
        // ȯ�� �������� API Ű�� �о��
        apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        // API Ű�� �������� ���� ��� ���� �޽��� ���
        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("API key is not set in environment variables.");
            return;
        }
    }

    public void SetCharacter(string input)
    {
        character = input;
    }
    public void Escape()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked; // Ŀ�� �� ����
        Cursor.visible = false; // Ŀ�� ���̰� ����
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartRecording();
            Debug.Log("��ȭ ����");
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            StopRecording();
            Debug.Log("��ȭ ����");
            SendAudioClip(Application.persistentDataPath + "/audio.wav");
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
            StartCoroutine(SendRequestToGPT(result));
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
    //gpt��Ʈ
    public IEnumerator SendRequestToGPT(string prompt)
    {
        string apiUrl = "https://api.openai.com/v1/chat/completions";
        var requestData = new
        {
            model = "gpt-4-turbo", // Use GPT-4 Turbo model
            messages = new[]
            {
                new { role = "system", content = $"�ʴ� {character}�� �ι� ������ ���� ���ݰ� ������ ���ϴ� ����̾�. �ѱ���� 20�ܾ �ѱ��� ������" },
                new { role = "user", content = prompt }
            },
            max_tokens = 300,
            temperature = 0.6
        };

        string jsonData = JsonConvert.SerializeObject(requestData);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // API Ű�� null�̰ų� �� ���ڿ����� Ȯ��
        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("API key is null or empty.");
            yield break;
        }

        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var jsonResponse = request.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<OpenAIResponse>(jsonResponse);
            Debug.Log("GPT ��� : " + response.choices[0].message.content.Trim());
            result = response.choices[0].message.content.Trim();
            text.text = result;
            TextToSpeech(result);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }

    [Serializable]
    private class OpenAIResponse
    {
        public Choice[] choices;
    }

    [Serializable]
    private class Choice
    {
        public Message message;
    }

    [Serializable]
    private class Message
    {
        public string role;
        public string content;
    }
    //TTS
    public void TextToSpeech(string sentence)
    {
        Debug.Log("���� ���� : " + sentence);
        AudioSource audio = GetComponent<AudioSource>();

        // API �߱��ϰ� ���� client_id, client_secret �ۼ�
        string client_id = "jgw5cfcbb4";
        string client_secret = "m3XpLu0sLXzlO2DA1IcCevQQtbefRqITJVukm2kd";

        string url = "https://naveropenapi.apigw.ntruss.com/tts-premium/v1/tts";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("X-NCP-APIGW-API-KEY-ID", client_id);
        request.Headers.Add("X-NCP-APIGW-API-KEY", client_secret);
        request.Method = "POST";

        // ��Ҹ� �̸� ���� 
        string avatar_name = "nsangdo";
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
        }
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

}
