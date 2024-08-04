using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YunaIdleState : MonoBehaviour, IYunaState
{
    private YunaController _yunaController;

    public void OnStateEnter(YunaController yunaController)
    {
        if (!_yunaController)
            _yunaController = yunaController;
    }
    // Idle State�� ���� 3�ʰ� ������ walkState�� ����
    public void OnStateUpdate()
    {
        
    }

    public void OnStateExit()
    {

    }

}
