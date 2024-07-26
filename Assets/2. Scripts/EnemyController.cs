using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.SceneView;

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
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Player �±׸� ���� ������Ʈ�� ã�Ƽ� �÷��̾�� ����
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ������ �ʱ� ��ġ ����
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        state = State.Idle;
    }

    private void Update()
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
            state = State.Idle;
        }

        StateMachine();
        CheckAnimationEnd();
        ChangeAnimation();
    }

    protected override void StateMachine()
    {
        /*switch (state)
        {
            case State.Idle:
                if (distanceToPlayer <= chaseDistance)
                {
                    state = State.Move;
                }
                else if (distanceToPlayer <= stopDistance)
                {
                    state = State.Attack;
                    Attack();
                }
                break;
            case State.Move:
                if(distanceToPlayer <= stopDistance)
                {
                    state = State.Attack;
                    Attack();
                }
                else
                {
                    state = State.Idle;
                }
                break;
            case State.Attack:
                if (distanceToPlayer <= chaseDistance) 
                {
                    state = State.Move;
                }
                else
                {
                    state = State.Idle;
                }
                break;
        }*/
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Melee")
        {
            if(other.GetComponent<WeaponManager>().player.state == State.Attack)
                Debug.Log("Melee" + other.GetComponent<WeaponManager>().atk);
        }
    }

    protected override void CheckAnimationEnd()
    {
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (animStateInfo.IsName("Jump") || animStateInfo.IsName("JumpWhileRunning"))
            return;

        if (animStateInfo.normalizedTime >= 1.0f && !animStateInfo.loop)
        {
            state = State.Idle;
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
        anim.Play("PunchRight");
    }
}
