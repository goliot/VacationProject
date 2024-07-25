using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CreatureState
{
    public State state;
    public Stats stats;

    private Animator anim;
    // NavMeshAgent�� ���� ����
    private NavMeshAgent navAgent;

    // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private Transform player;

    // ���Ͱ� �÷��̾ ������ �Ÿ�
    public float chaseDistance = 10f;

    // ������ �ʱ� ��ġ�� ������ ����
    private Vector3 startPosition;

    // ���Ͱ� �÷��̾ ������ �ּ� �Ÿ�
    public float stopDistance = 2f;

    // ���Ͱ� ������ ��ġ �迭
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        // Player �±׸� ���� ������Ʈ�� ã�Ƽ� �÷��̾�� ����
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ������ �ʱ� ��ġ ����
        startPosition = transform.position;
    }

    void Update()
    {
        // �÷��̾���� �Ÿ��� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

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

        ChangeAnimation();
    }

    // �÷��̾ �����ϴ� �Լ�
    void ChasePlayer(float distanceToPlayer)
    {
        // �÷��̾ �ʹ� ������ ���� ���
        if (distanceToPlayer <= stopDistance)
        {
            // �����
            navAgent.isStopped = true;
            state = State.Attack;
        }
        else
        {
            // �÷��̾ ���󰣴�
            navAgent.isStopped = false;
            navAgent.SetDestination(player.position);
            state = State.Move;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Melee")
        {
            if(other.GetComponent<WeaponManager>().player.state == State.Attack)
                Debug.Log("Melee" + other.GetComponent<WeaponManager>().atk);
        }
    }

    protected override void ChangeAnimation()
    {
        switch (state)
        {
            case State.Temp: //�ٸ� ������Ʈ�� ��ȯ�� ���� null ����
                break;
            case State.Idle:
                anim.Play("Idle");
                break;
            case State.Move:
                anim.Play("RunForward");
                break;
            case State.Dash:
                anim.Play("Sprint");
                break;
            case State.Jump:
                anim.Play("Jump");
                break;
            case State.JumpWhileRun:
                anim.Play("JumpWhileRunning");
                break;
            case State.Attack:
                Attack();
                break;
            case State.Die:
                break;
        }
    }

    private void Attack()
    {
        anim.Play("Punch");
    }
}
