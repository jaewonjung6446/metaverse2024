using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoPassZone : MonoBehaviour
{
    public Transform playerCamera;
    public float interactionDistance = 5f;
    public Canvas canvas;
    public Text interactionText;
    public string message = "�ű� ���� ������ �� ���� ���̾�";
    private bool isInteracting = false;

    // �÷��̾ ���� �� ������Ʈ�� ���� �Ÿ��� �ִ� ���¿��� EŰ�� ������ �ؽ�Ʈ ǥ��
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInteracting && PlayerCanSee.instance.closestObject == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactionDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    StartCoroutine(ShowMessage(message));
                }
            }
        }
    }

    // Ű����� �޼��� �����ִ� ����
    IEnumerator ShowMessage(string message)
    {
        isInteracting = true;
        interactionText.text = "";
        canvas.gameObject.SetActive(true);

        foreach (char c in message)
        {
            interactionText.text += c;
            yield return new WaitForSeconds(0.05f); // Ÿ���� �ӵ� ����
        }

        yield return new WaitForSeconds(1f); // �޽����� ���� �ð� ���� ������
        canvas.gameObject.SetActive(false);
        isInteracting = false;
    }

}
