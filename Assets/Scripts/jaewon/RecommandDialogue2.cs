using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RecommandDialogue2 : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogues;
    [SerializeField] private List<string> text;
    public GameObject Panel;
    public ending end;
    private bool isDialogue = false;
    private bool isEscapeKeyActivated = false;
    private int currentCounter = 0; //���� ��� �ε���
    private WaitForSeconds typingTime = new WaitForSeconds(0.05f); //�� ���ھ� Ÿ���� �Ǵ� �ӵ�
    private void OnEnable()
    {
        dialoguePanel.SetActive(true);
        dialogues.gameObject.SetActive(true);
        dialogues.text = "";
        text.Add("���� �޽��� �ʿ��ϸ� �������� �������� ã�ƿ���");
        text.Add("�츰 �ʸ� ��ٸ���");
        text.Add("�츮 �̾߱�� ������ �ʾҾ�");
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
            Panel.gameObject.SetActive(true);
            this.enabled = false;
            end.enabled = true;
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

}
