using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData = new EnemyData();

    private Animator animator;
    private NavMeshAgent navAgent;
    private Transform player;

    [Header("# Chase")]
    public float chaseDistance = 10f;
    public float stopDistance = 3f;
    private float distanceToPlayer;
    private Vector3 startPosition;

    [Header("# Patrol")]
    [SerializeField]
    private float patrolRadius = 20f;
    [SerializeField]
    private float patrolDelay = 2f;
    private bool isPatrolling = false;
    private bool isChasing = false;

    private Coroutine patrolCoroutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        StartPatrolling();
    }

    public void Init(EnemyData data)
    {
        enemyData = data;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseDistance)
        {
            if (!isChasing)
            {
                isChasing = true;
                StopPatrolling();
            }
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                StartPatrolling();
            }
        }
    }

    void ChasePlayer(float distanceToPlayer)
    {
        if (distanceToPlayer <= stopDistance)
        {
            navAgent.isStopped = true;
            animator.SetBool("isMove", false);
        }
        else
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(player.position);
            animator.SetBool("isMove", true);
        }

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    IEnumerator Patrol()
    {
        isPatrolling = true;

        while (isPatrolling)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += startPosition;

            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, patrolRadius, -1);

            navAgent.SetDestination(navHit.position);
            animator.SetBool("isMove", true);

            while (navAgent.pathPending || navAgent.remainingDistance > navAgent.stoppingDistance)
            {
                yield return null;
            }

            animator.SetBool("isMove", false);
            yield return new WaitForSeconds(patrolDelay);
        }
    }

    void StartPatrolling()
    {
        if (patrolCoroutine == null)
        {
            patrolCoroutine = StartCoroutine(Patrol());
        }
    }

    void StopPatrolling()
    {
        if (patrolCoroutine != null)
        {
            StopCoroutine(patrolCoroutine);
            patrolCoroutine = null;
            animator.SetBool("isMove", false);
            navAgent.isStopped = true;
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Hit " + damage);
        animator.SetTrigger("OnHit");
    }
}
