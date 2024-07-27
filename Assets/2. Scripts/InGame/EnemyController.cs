using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.SceneView;

public class EnemyController : MonoBehaviour
{
    public Stats stats;

    private Animator animator;
    // NavMeshAgent�� ���� ����
    private NavMeshAgent navAgent;

    // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Transform player;

    // ���Ͱ� �÷��̾ ������ �Ÿ�
    public float chaseDistance = 10f;

    // ���Ϳ� �÷��̾��� �Ÿ�
    private float distanceToPlayer;

    // ������ �ʱ� ��ġ�� ������ ����
    private Vector3 startPosition;

    // ���Ͱ� �÷��̾ ������ �ּ� �Ÿ�
    public float stopDistance = 3f;

    // ���Ͱ� ������ ��ġ �迭
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Player �±׸� ���� ������Ʈ�� ã�Ƽ� �÷��̾�� ����
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ������ �ʱ� ��ġ ����
        startPosition = transform.position;
    }

    /*private void Update()
    {
        // �÷��̾���� �Ÿ��� ���
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // �÷��̾ ���� �Ÿ� �ȿ� ���� ���
        if (distanceToPlayer <= chaseDistance)
        {
            // �÷��̾ ����
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            // ���� �������� �̵�
            //Patrol();
        }
    }*/

    // �÷��̾ �����ϴ� �Լ�
    void ChasePlayer(float distanceToPlayer)
    {
        // �÷��̾ �ʹ� ������ ���� ���
        if (distanceToPlayer <= stopDistance)
        {
            // �����
            navAgent.isStopped = true;
        }
        else
        {
            // �÷��̾ ���󰣴�
            navAgent.isStopped = false;
            navAgent.SetDestination(player.position);
        }

        // �׻� �÷��̾ �ٶ󺸵��� ����
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // �����ϴ� �Լ�
    void Patrol()
    {
        // ���� ���� ������ �����ߴ��� Ȯ��
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            // ���� ���� �������� �̵�
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Hit " + damage);
        animator.SetTrigger("OnHit");
    }
}
