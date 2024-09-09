using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RecommandDialogue : MonoBehaviour
{
    public RecommendNPC npc;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogues;
    [SerializeField] private List<string> text;
    private string apiKey;


    private bool isDialogue = false;
    private bool isEscapeKeyActivated = false;
    private int currentCounter = 0; //���� ��� �ε���
    private WaitForSeconds typingTime = new WaitForSeconds(0.05f); //�� ���ھ� Ÿ���� �Ǵ� �ӵ�
    private void OnEnable()
    {
        apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        StartCoroutine(SendRequestToGPT());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("EŬ��");
            if (!isDialogue)
            {
                ActiveDialogue();
            }
        }
    }
    public void ActiveDialogue()
    {
        if (text.Count > currentCounter)
        {
            StartCoroutine(ActiveDialogueCoroutine());
        }
        else
        {
            dialogues.gameObject.SetActive(false);
            dialoguePanel.SetActive(false);
            npc.enabled = true;
            this.enabled = false;
        }
    }


    // Ű����� �޼��� �����ִ� ����
    private IEnumerator ActiveDialogueCoroutine()
    {
        isDialogue = true;
        dialoguePanel.SetActive(true);
        dialogues.text = ""; // �ؽ�Ʈ �ʱ�ȭ

        foreach (char letter in text[currentCounter])
        {
            if (Input.GetKey(KeyCode.Space))
            {
                dialogues.text = text[currentCounter];
                break;
            }
            dialogues.text += letter;
            yield return typingTime;
        }
        currentCounter++;
        isDialogue = false;
    }
    public IEnumerator SendRequestToGPT()
    {
        string apiUrl = "https://api.openai.com/v1/chat/completions";
        var requestData = new
        {
            model = "gpt-4-turbo", // Use GPT-4 Turbo model
            messages = new[]
            {
                new { role = "system", content = $"{GameManager.Instance.talkings}�� �߾���� �������� ������� ������ �м��Ͽ� �� �ΰݿ� �´� ������ ��� �Ѱ��� ��." +
                $" �ٸ������� ��� ��������, ���࿡ ��ȭ�� ���ٸ�, �ƹ�����̳� ��, �ѱ���� �����ؼ� ��" },
                new { role = "user", content = "" }
            },
            max_tokens = 100,
            temperature = 1
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
            GameManager.Instance.result = response.choices[0].message.content.Trim();
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

}
