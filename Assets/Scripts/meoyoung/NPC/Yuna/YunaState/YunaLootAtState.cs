using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YunaLootAtState : MonoBehaviour, IYunaState
{
    private YunaController _yunaController;
    private float lookTimer;
    public void OnStateEnter(YunaController yunaController)
    {
        if (!_yunaController)
            _yunaController = yunaController;

        _yunaController.anim.SetBool("Lookat", true);

        lookTimer = 0;
    }

    //target�� �����ϴ� lookat ����. target���� 3�ʵ��� ������ �� idleState�� ����
    // spaceŰ�� ������ nod ���·� ����
    public void OnStateUpdate()
    {
        //Debug.Log("NPC LootAt Q");
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            _yunaController.ChangeState(_yunaController._nodState);
        }*/

        if (lookTimer >= 3f)
        {
            _yunaController.ChangeState(_yunaController._idleState);
        }
        else
        {
            lookTimer += Time.deltaTime;
        }
    }

    public void OnStateExit()
    {
        _yunaController.anim.SetBool("Lookat", false);
    }
}
