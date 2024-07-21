using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Animator anim;
    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public NavMeshAgent _navMeshAgent;

    [Header("Movement")]
    public float moveSpeed = 5f;    
    public float gravity = 20f;

    public float CurrentSpeed
    {
        get; set;
    }

    public Vector3 CurrentDirection
    {
        get; set;
    }

    public INPCState CurrentState
    {
        get; set;
    }

    public INPCState _idleState, _walkState;


    private void Start()
    {
        _idleState = gameObject.AddComponent<NPCIdleState>();
        _walkState = gameObject.AddComponent<NPCWalkState>();

        _characterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        CurrentState = _idleState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void ChangeState(INPCState playerState)
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

    public CharacterController GetCharacterController()
    {
        return _characterController;
    }

}
