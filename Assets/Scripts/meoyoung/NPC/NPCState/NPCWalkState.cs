using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalkState : MonoBehaviour, INPCState
{
    private NPCController _npcController;
    private float walkDuration;
    private float walkTimer;

    public void OnStateEnter(NPCController npcController)
    {
        if(!_npcController)
            _npcController = npcController;

        _npcController.anim.SetBool("Walk", true);
        walkDuration = Random.Range(5f, 7f);
        walkTimer = 0f;

        Vector3 randomDirection = Random.insideUnitSphere * 10f;
        randomDirection += _npcController.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10f, 1);
        Vector3 finalPosition = hit.position;

        _npcController._navMeshAgent.destination = finalPosition;
        _npcController._navMeshAgent.isStopped = false;
    }


    public void OnStateUpdate()
    {
        if (_npcController)
        {
            //Debug.Log("NPC Walk");

            // ������ �ð���ŭ �̵��߰ų�, �������� �������� ��� idle ���·� ��ȯ
            walkTimer += Time.deltaTime;
            if (walkTimer >= walkDuration)
            {
                _npcController.ChangeState(_npcController._idleState);
            }
            else
            {
                /*// E Ű �Է� ����
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _npcController.ChangeState(_npcController._lootatState);
                }*/

                if (_npcController._navMeshAgent.remainingDistance <= _npcController._navMeshAgent.stoppingDistance)
                {
                    if (!_npcController._navMeshAgent.pathPending)
                    {
                        _npcController.ChangeState(_npcController._idleState);
                    }
                }
            }
        }
    }
    public void OnStateExit()
    {
        _npcController.anim.SetBool("Walk", false);
        _npcController._navMeshAgent.isStopped = true;
    }

}
