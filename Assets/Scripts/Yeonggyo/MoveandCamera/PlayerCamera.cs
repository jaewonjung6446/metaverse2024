using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public CharacterController cc;
    public float Lerppercentage = 1f;
    public Vector3 Quad = new Vector3(0f, 10f, -10f);
    public Vector3 Angle = new Vector3(30f, 0f, 0f);
    public Vector3 Skyposition = new Vector3(0f, 7f, 0f);
    public Vector3 Skyangle = new Vector3(30f, 0f, 0f);
    public Vector3 Oceanposition = new Vector3(4f, 2f, -6f);
    public Vector3 Oceanangle = new Vector3(0f, -30f, 0f);

    
    public static bool normal = true;
    public static bool sky = false;
    public static bool ocean = false;


    public static Action viewsky;
    public static Action viewquad;
    public static Action viewocean;
    public static Action<GameObject> viewtalk;

    private void Awake()
    {
        viewsky = () => { ViewSky(); };
        viewquad = () => { ViewQuad(); };
        viewocean = () => { ViewOcean(); };
        viewtalk = (GameObject g) => { ViewTalk(g); };
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
        if (normal) //��� ���ͺ�
        {
            ViewQuad();
        }
        else if (sky)
        {
            ViewSky();
        }
        else if (ocean)
        {
            ViewOcean();
        }
    }

    void ViewQuad()
    {
        Quaternion currentangle = transform.rotation;
        Quaternion targetangle = Quaternion.Euler(Angle);

        Vector3 capsule = cc.transform.position;
        transform.position = Vector3.Lerp(transform.position, capsule + Quad, Time.deltaTime * Lerppercentage);
        transform.rotation = Quaternion.Lerp(currentangle, targetangle, Time.deltaTime * Lerppercentage);
    }

    void ViewSky()
    {
        Vector3 capsule = cc.transform.position;
        transform.position = Vector3.Lerp(transform.position, capsule + Skyposition, Time.deltaTime * Lerppercentage);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Skyangle, Time.deltaTime * Lerppercentage);
    }

    void ViewOcean()
    {
        Quaternion currentangle = transform.rotation;
        Quaternion targetangle = Quaternion.Euler(Oceanangle);

        Vector3 capsule = cc.transform.position;
        transform.position = Vector3.Lerp(transform.position, capsule + Oceanposition, Time.deltaTime * Lerppercentage);
        transform.rotation = Quaternion.Lerp(currentangle, targetangle, Time.deltaTime * Lerppercentage);
    }

    // Coroutine to adjust the camera over Time.deltaTime duration
    public IEnumerator ViewTalk(GameObject closestObject)
    {
        if (closestObject == null)
        {
            Debug.LogWarning("No closest object detected. Cannot adjust camera.");
            yield break;
        }

        // �÷��̾�� NPC ������ �߰� �������� ���
        Vector3 middlePoint = (cc.gameObject.transform.position + closestObject.transform.position) / 2;

        // ī�޶� �ٶ� ������ ���
        Vector3 directionToLook = (cc.gameObject.transform.position - closestObject.transform.position).normalized;

        // Y�� ȸ���� -45������ 45�� ���̷� ����
        float targetYAngle = Mathf.Clamp(Quaternion.LookRotation(directionToLook).eulerAngles.y, -45f, 45f);
        Vector3 targetAngle = new Vector3(30f, targetYAngle, this.Angle.z);
        Quaternion targetRotation = Quaternion.Euler(targetAngle);

        // ī�޶��� �ʱ� ��ġ�� ȸ�� ����
        Vector3 initialPosition = this.transform.position;
        Quaternion initialRotation = this.transform.rotation;

        // ī�޶��� ��ǥ ��ġ ���
        float cameraDistance = 4f; // ī�޶� ������ �Ÿ�
        Vector3 cameraOffset = -targetAngle.normalized * cameraDistance; // �߰� �������κ��� ������ ��ġ ���
        Vector3 targetPosition = middlePoint + cameraOffset; // ��ǥ ��ġ

        // �ε巯�� ��ȯ�� ���� ����
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            // ���� ��� ���
            float t = elapsedTime / 1f;

            // ī�޶��� ��ġ�� ȸ���� ����
            this.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            this.transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            // �ð� ��� ������Ʈ
            elapsedTime += Time.deltaTime;

            // ���� �����ӱ��� ���
            yield return null;
        }

        // ���� ��ġ�� ȸ���� ��Ȯ�� ����
        this.transform.position = targetPosition;
        this.transform.rotation = targetRotation;
    }





}
