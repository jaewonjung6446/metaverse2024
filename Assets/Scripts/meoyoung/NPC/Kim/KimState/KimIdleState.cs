using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimIdleState : MonoBehaviour, IKimState
{
    private KimController _kimController;

    public void OnStateEnter(KimController kimController)
    {
        if (!_kimController)
            _kimController = kimController;
    }
    // Idle State�� ���� 3�ʰ� ������ walkState�� ����
    public void OnStateUpdate()
    {
        
    }

    public void OnStateExit()
    {

    }

}
