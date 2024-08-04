using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YunaNodState : MonoBehaviour, IYunaState
{
    private YunaController _yunaController;
   
    public void OnStateEnter(YunaController yunaController)
    {
        if (!_yunaController)
            _yunaController = yunaController;

        _yunaController.anim.SetBool("Nod", true);
    }

    // qŰ�� ������ thinkState�� ����
    public void OnStateUpdate()
    {
        //Debug.Log("NPC Nod Q");
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _yunaController.ChangeState(_yunaController._thinkState);
        }
    }

    public void OnStateExit()
    {
        _yunaController.anim.SetBool("Nod", false);
    }

}

