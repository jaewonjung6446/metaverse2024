using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KimController : MonoBehaviour
{
   // public Animator anim;

    public IKimState CurrentState
    {
        get; set;
    }

    public IKimState _idleState, _lookatState, _nodState, _thinkState, _goodState, _badState;


    // NPC�� ���̵� �� �ִ� ���¿� ���� ��ũ��Ʈ�� �ҷ��� ��, �ʱ� ���¸� Idle State�� ��ȯ
    private void Start()
    {
        _idleState = gameObject.AddComponent<KimIdleState>();
        _lookatState = gameObject.AddComponent<KimLootAtState>();
        _nodState = gameObject.AddComponent<KimNodState>();
        _thinkState = gameObject.AddComponent<KimThinkState>() ;
        _goodState = gameObject.AddComponent<KimGoodState>();
        _badState = gameObject.AddComponent<KimBadState>();

        CurrentState = _idleState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void ChangeState(IKimState playerState)
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
