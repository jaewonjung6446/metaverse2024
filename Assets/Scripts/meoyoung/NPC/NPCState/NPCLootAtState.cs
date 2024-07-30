using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLootAtState : MonoBehaviour, INPCState
{
    private NPCController _npcController;
    private float lookTimer;
    public void OnStateEnter(NPCController npcController)
    {
        if (!_npcController)
            _npcController = npcController;

        _npcController._navMeshAgent.isStopped = true; // NPC ������ ����
        _npcController.anim.SetBool("LootAt", true);

        lookTimer = 0;
    }

    //target�� �ٶ󺸴� lookat ����. target�� 3�ʵ��� �Ĵٺ� �� walkState�� ����
    // qŰ�� ������ nod ���·� ����
    public void OnStateUpdate()
    {
        //Debug.Log("NPC LootAt Q");
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _npcController.ChangeState(_npcController._nodState);
        }


        if (lookTimer >= 3f)
        {
            _npcController.ChangeState(_npcController._walkState);
        }
        else
        {
            Vector3 direction = (_npcController.target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            lookTimer += Time.deltaTime;
        }
    }

    public void OnStateExit()
    {
        _npcController._navMeshAgent.isStopped = false; // NPC ������ �簳
        _npcController.anim.SetBool("LootAt", false);
    }
}
