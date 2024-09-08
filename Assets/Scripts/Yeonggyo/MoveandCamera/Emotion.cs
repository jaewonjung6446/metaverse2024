using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDirectionAnimation : MonoBehaviour
{

    public Animator animator;

    private Vector3 initialMousePosition;
    private bool isGKeyPressed;

    void Start()
    {
        // ������ �� ���콺 Ŀ���� ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // g Ű�� ���� �� ���콺 Ŀ���� ȭ�� �߾����� �̵���Ű�� ǥ��
        // 'A' Ű�� ������ �Ϲ� �޴� ����
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGKeyPressed = true;
            initialMousePosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Cursor.SetCursor(null, initialMousePosition, CursorMode.Auto);
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetKey(KeyCode.G)) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // g Ű�� �� �� ���콺 ���� ��� �� �ִϸ��̼� ���
        if (Input.GetKeyUp(KeyCode.G) && isGKeyPressed)
        {
            isGKeyPressed = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Vector3 finalMousePosition = Input.mousePosition;
            Vector3 direction = finalMousePosition - initialMousePosition;
            PlayAnimationBasedOnDirection(direction);
        }
    }

    private void PlayAnimationBasedOnDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle >= -45 && angle < 45)
        {
            // ������ ���� �ִϸ��̼�
            animator.SetTrigger("Dance");
            Debug.Log("Dance");
        }
        else if (angle >= 45 && angle < 135)
        {
            // ���� ���� �ִϸ��̼�
            animator.SetTrigger("Enjoy");
            Debug.Log("Enjoy");
        }
        else if (angle >= -135 && angle < -45)
        {
            // �Ʒ��� ���� �ִϸ��̼�
            animator.SetTrigger("Angry");
            Debug.Log("Angry");
        }
        else
        {
            // ���� ���� �ִϸ��̼�
            animator.SetTrigger("Sad");
            Debug.Log("Sad");
        }
    }
}
