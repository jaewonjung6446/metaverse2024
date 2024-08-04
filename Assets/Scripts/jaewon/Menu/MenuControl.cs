using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private GameObject Diary;
    [SerializeField] private GameObject Ctrl;
    [SerializeField] private GameObject Buttons;

    public Vector3 RightPosition = new Vector3(2000, 0, 0);
    public Vector3 LeftPosition = new Vector3(-2000, 0, 0);

    public Vector3 CenterPosition = new Vector3(0, 0, 0); // �� ��ġ (ȭ�� ��)
    float duration = 0.3f; // �̵� �ð�

    public void OnEnable()
    {
        Diary.SetActive(false);
        Ctrl.SetActive(false);
        Buttons.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            Buttons.GetComponent<RectTransform>().anchoredPosition = CenterPosition;
            Diary.GetComponent<RectTransform>().anchoredPosition = RightPosition;
            Ctrl.GetComponent<RectTransform>().anchoredPosition = RightPosition;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void GetExit() {
        Application.Quit();
    }
    public void GetDiary()
    {
        Diary.SetActive(true);
        StartCoroutine(Slide(Buttons.GetComponent<RectTransform>(), CenterPosition, LeftPosition));
        StartCoroutine(Slide(Diary.GetComponent<RectTransform>(), RightPosition, CenterPosition));
    }
    public void GetCtrl()
    {
        Ctrl.SetActive(true);
        StartCoroutine(Slide(Buttons.GetComponent<RectTransform>(), CenterPosition, LeftPosition));
        StartCoroutine(Slide(Ctrl.GetComponent<RectTransform>(), RightPosition, CenterPosition));
    }
    public void StartSlide(RectTransform uiElement, Vector3 start, Vector3 end)
    {
        StartCoroutine(Slide(uiElement, start, end));
    }
    public IEnumerator Slide(RectTransform uiElement, Vector3 start, Vector3 end)
    {
        Debug.Log(uiElement.name+"UI�̵�����");

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            Debug.Log(uiElement.name + "UI�̵� ��");

            // �ð��� ���� ����
            uiElement.anchoredPosition = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // ���� ��ġ�� ����
        uiElement.anchoredPosition = end;
    }
}
