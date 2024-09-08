using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public CharacterController cc;
    public GameObject player;
    public float Lerppercentage = 1f;
    public Vector3 Quad = new Vector3(0f, 10f, -10f);
    public Vector3 Angle = new Vector3(30f, 0f, 0f);
    public Vector3 Skyposition = new Vector3(0f, 7f, 0f);
    public Vector3 Skyangle = new Vector3(30f, 0f, 0f);
    public Vector3 Oceanposition = new Vector3(4f, 2f, -6f);
    public Vector3 Oceanangle = new Vector3(0f, -30f, 0f);


    public static short state = 0;  // 0 : �Ϲ� ���ͺ� / 1 : �ϴú��� / 2 : �ٴٺ��� / 3 : ��ȭ��
    public static bool before = false;

    public static Action viewsky;
    public static Action viewquad;
    public static Action viewocean;

    private void Awake()
    {
        viewsky = () => { ViewSky(); };
        viewquad = () => { ViewQuad(); };
        viewocean = () => { ViewOcean(); };
    }


    void Start()
    {
        Vector3 capsule = cc.transform.position;
        transform.position = capsule + Quad;
        transform.eulerAngles = Angle;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0) //��� ���ͺ�
        {
            ViewQuad();
        }
        else if (state == 1)
        {
            ViewSky();
        }
        else if (state == 2)
        {
            ViewOcean();
        }
    }

    void ViewQuad()
    {
        // ���� ȸ���� ��ǥ ȸ�� ����
        Quaternion currentAngle = transform.rotation;
        Quaternion targetAngle = Quaternion.Euler(Angle);

        // �÷��̾��� ���� ��ġ�� ������� ī�޶� ��ǥ ��ġ ����
        Vector3 capsule = player.transform.position;
        Vector3 targetPosition = capsule + Quad;

        // ���� ������ ���ϱ� ���� t���� ��� (Time.deltaTime�� ���� ��ȭ���� ���������� ����)
        float t = Lerppercentage * Time.deltaTime;

        // ī�޶��� ��ġ�� ȸ���� ����
        transform.position = Vector3.Lerp(transform.position, targetPosition, t);
        transform.rotation = Quaternion.Lerp(currentAngle, targetAngle, t);

        // ��ǥ ��ġ�� ������ ���� �������� �� ������ ���ߵ��� ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f && Quaternion.Angle(currentAngle, targetAngle) < 0.01f)
        {
            // ��ǥ ��ġ�� ������ �ٷ� ����
            transform.position = targetPosition;
            transform.rotation = targetAngle;
        }
    }

    void ViewOcean()
    {
        // ���� ȸ���� ��ǥ ȸ�� ����
        Quaternion currentAngle = transform.rotation;
        Quaternion targetAngle = Quaternion.Euler(Oceanangle);

        // �÷��̾��� ���� ��ġ�� ������� ī�޶� ��ǥ ��ġ ����
        Vector3 capsule = player.transform.position;
        Vector3 targetPosition = capsule + Oceanposition;

        // ���� ������ ���ϱ� ���� t���� ��� (Time.deltaTime�� ���� ��ȭ���� ���������� ����)
        float t = Lerppercentage * Time.deltaTime;

        // ī�޶��� ��ġ�� ȸ���� ����
        transform.position = Vector3.Lerp(transform.position, targetPosition, t);
        transform.rotation = Quaternion.Lerp(currentAngle, targetAngle, t);

        // ��ǥ ��ġ�� ������ ���� �������� �� ������ ���ߵ��� ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f && Quaternion.Angle(currentAngle, targetAngle) < 0.01f)
        {
            // ��ǥ ��ġ�� ������ �ٷ� ����
            transform.position = targetPosition;
            transform.rotation = targetAngle;
        }
    }

    void ViewSky()
    {
        // ���� ȸ���� ��ǥ ȸ�� ����
        Quaternion currentAngle = transform.rotation;
        Quaternion targetAngle = Quaternion.Euler(Skyangle);

        // �÷��̾��� ���� ��ġ�� ������� ī�޶� ��ǥ ��ġ ����
        Vector3 capsule = player.transform.position;
        Vector3 targetPosition = capsule + Skyposition;

        // ���� ������ ���ϱ� ���� t���� ��� (Time.deltaTime�� ���� ��ȭ���� ���������� ����)
        float t = Lerppercentage * Time.deltaTime;

        // ī�޶��� ��ġ�� ȸ���� ����
        transform.position = Vector3.Lerp(transform.position, targetPosition, t);
        transform.rotation = Quaternion.Lerp(currentAngle, targetAngle, t);

        // ��ǥ ��ġ�� ������ ���� �������� �� ������ ���ߵ��� ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f && Quaternion.Angle(currentAngle, targetAngle) < 0.01f)
        {
            // ��ǥ ��ġ�� ������ �ٷ� ����
            transform.position = targetPosition;
            transform.rotation = targetAngle;
        }
    }

    // Coroutine to adjust the camera over Time.deltaTime duration
    public IEnumerator ViewTalk(GameObject closestObject, float cameraDistance)
    {
        if (closestObject == null)
        {
            Debug.LogWarning("No closest object detected. Cannot adjust camera.");
            yield break;
        }

        // �÷��̾�� NPC ������ �߰� ������ ���
        Vector3 middlePoint = (cc.gameObject.transform.position + closestObject.transform.position) / 2;

        // ī�޶� �ٶ� ������ ���
        Vector3 directionToLook = (closestObject.transform.position - cc.gameObject.transform.position).normalized;

        // Y�� ȸ���� -45������ 45�� ���̷� ����
        float targetYAngle = Mathf.Clamp(Quaternion.LookRotation(directionToLook).eulerAngles.y, -45f, 45f);
        Quaternion targetRotation = Quaternion.Euler(Angle.x, targetYAngle, Angle.z);

        // ī�޶��� �ʱ� ��ġ�� ȸ�� ����
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;

        // ī�޶��� ��ǥ ��ġ ���
        Vector3 cameraOffset = targetRotation * Vector3.forward * cameraDistance; // �߰� �������κ��� ������ ��ġ ���
        Vector3 targetPosition = middlePoint - cameraOffset; // ��ǥ ��ġ�� �ణ�� ������ �߰�

        // �ε巯�� ��ȯ�� ���� ����
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            if (state != 3)
                yield break;
            // ���� ��� ���
            float t = elapsedTime / 1f;

            // ī�޶��� ��ġ�� ȸ���� ����
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            // �ð� ��� ������Ʈ
            elapsedTime += Time.deltaTime;

            // ���� �����ӱ��� ���
            yield return null;
        }

        // ���� ��ġ�� ȸ���� ��Ȯ�� ����
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

}
