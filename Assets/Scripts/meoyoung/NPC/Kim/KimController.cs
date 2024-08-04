using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KimController : MonoBehaviour
{
    public Animator anim;

    public IKimState CurrentState
    {
        get; set;
    }

    public IKimState _idleState;


    // NPC�� ���̵� �� �ִ� ���¿� ���� ��ũ��Ʈ�� �ҷ��� ��, �ʱ� ���¸� Idle State�� ��ȯ
    private void Start()
    {
        _idleState = gameObject.AddComponent<KimIdleState>();
        
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
