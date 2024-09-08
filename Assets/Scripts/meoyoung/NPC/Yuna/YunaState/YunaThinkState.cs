using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YunaThinkState : MonoBehaviour, IYunaState
{
    private YunaController _yunaController;

    public void OnStateEnter(YunaController yunaController)
    {
        if (!_yunaController)
            _yunaController = yunaController;

        _yunaController.anim.SetBool("Think", true);
    }

    // QŰ�� ������ goodState�� ����.
    // EŰ�� ������ badState�� ����
    public void OnStateUpdate()
    {
        //Debug.Log("NPC Think Q E");
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            _yunaController.ChangeState(_yunaController._goodState);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _yunaController.ChangeState(_yunaController._badState);
        }*/
    }

    public void OnStateExit()
    {
        _yunaController.anim.SetBool("Think", false);
    }

}
