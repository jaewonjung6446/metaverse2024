using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimNodState : MonoBehaviour, IKimState
{
    private KimController _kimController;
   
    public void OnStateEnter(KimController kimController)
    {
        if (!_kimController)
            _kimController = kimController;
        //_kimController.anim.SetBool("Nod", true);
    }

    // qŰ�� ������ thinkState�� ����
    public void OnStateUpdate()
    {
        //Debug.Log("NPC Nod Q");
        /*if (Input.GetKeyUp(KeyCode.Space))
        {
            _kimController.ChangeState(_kimController._thinkState);
        }*/
    }

    public void OnStateExit()
    {
        //_kimController.anim.SetBool("Nod", false);
    }

}

