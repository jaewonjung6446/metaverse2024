using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YunaController : MonoBehaviour
{
    public Animator anim;

    public IYunaState CurrentState
    {
        get; set;
    }

    public IYunaState _idleState, _lookatState, _nodState, _thinkState, _goodState, _badState;


    // NPC�� ���̵� �� �ִ� ���¿� ���� ��ũ��Ʈ�� �ҷ��� ��, �ʱ� ���¸� Idle State�� ��ȯ
    private void Start()
    {
        _idleState = gameObject.AddComponent<YunaIdleState>();
        _lookatState = gameObject.AddComponent<YunaLootAtState>();
        _nodState = gameObject.AddComponent<YunaNodState>();
        _thinkState = gameObject.AddComponent<YunaThinkState>() ;
        _goodState = gameObject.AddComponent<YunaGoodState>();
        _badState = gameObject.AddComponent<YunaBadState>();

        CurrentState = _idleState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void ChangeState(IYunaState playerState)
    {
        if (CurrentState != null)
            CurrentState.OnStateExit();
        CurrentState = playerState;
        CurrentState.OnStateEnter(this);
    }

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }
}
