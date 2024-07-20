using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    private bool isLookingAtPlayer = false;
    private float timer;

    private NavMeshAgent agent;
    public Animator anim;
    public GameObject target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        if (!isLookingAtPlayer)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
            // NPC�� �Ȱ� �ִ��� �ƴ��� üũ
            if (agent.velocity.magnitude > 0.1f)
            {
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }
        }
        // E Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LookAtPlayer());
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    private IEnumerator LookAtPlayer()
    {
        isLookingAtPlayer = true;
        agent.isStopped = true; // NPC ������ ����
        anim.SetBool("Walk", false); // �ȱ� �ִϸ��̼� ����

        float lookTime = 0;
        while (lookTime < 5f)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            lookTime += Time.deltaTime;
            yield return null;
        }

        agent.isStopped = false; // NPC ������ �簳
        isLookingAtPlayer = false;

    }
}
