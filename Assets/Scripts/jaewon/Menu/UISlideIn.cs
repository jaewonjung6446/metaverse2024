using System.Collections;
using UnityEngine;

public class UISlideIn : MonoBehaviour
{
    public RectTransform uiElement; // �̵��� UI ���
    public Vector3 endPosition = new Vector3(0, 0, 0); // �� ��ġ (ȭ�� ��)
    public float duration = 0.7f; // �̵� �ð�

    private void Start()
    {
        if (uiElement != null)
        {
            // ���� ��ġ�� ����
            uiElement.anchoredPosition = new Vector3(Screen.width, 0, 0);
        }
    }

    public IEnumerator SlideIn(RectTransform uiElement)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // �ð��� ���� ����
            uiElement.anchoredPosition = Vector3.Lerp(new Vector3(Screen.width, 0, 0), endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        // ���� ��ġ�� ����
        uiElement.anchoredPosition = endPosition;
    }
}
