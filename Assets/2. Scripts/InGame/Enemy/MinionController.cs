using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    public EnemyData enemyData = new EnemyData();

    private Animator animator;
    private NavMeshAgent navAgent;
    private Transform player;

    [Header("# Chase")]
    [SerializeField]
    private float chaseDistance = 10f;
    [SerializeField]
    private float stopDistance = 3f;
    private float distanceToPlayer;
    private Vector3 startPosition;
    public GameObject chaseTarget;

    [Header("# AttackCollision")]
    [SerializeField]
    private GameObject meleeAttackCollison;

    [Header("# Destination")]
    [SerializeField]
    public Transform checkPoint;
    [SerializeField]
    public Transform finalPoint;

    private bool isCheckPoint; // üũ����Ʈ�� �����ߴ��� ����

    Coroutine toCheckPoint = null;
    Coroutine toFinalPoint = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.isStopped = true;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        isCheckPoint = false;
    }

    public void Init(EnemyData data)
    {
        enemyData = data;
    }

    private void Update()
    {
        //todo : patrol ���� ������ ��Ʈ ���� ���°ɷ�
        /*distanceToPlayer = Vector3.Distance(player.position, transform.position);

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
        }*/

        chaseTarget = CheckEnemy();
        if (chaseTarget != null)
        {
            StopAllCoroutines();
            Chase(chaseTarget);
            return;
        }
        /*if(!isCheckPoint && toCheckPoint == null)
        {
            toCheckPoint = StartCoroutine(ToCheckPoint());
        }
        if(isCheckPoint && toFinalPoint == null)
        {
            if(toCheckPoint != null)
                StopCoroutine(toCheckPoint);

            toFinalPoint = StartCoroutine(ToFinalPoint());
        }*/
        if (!isCheckPoint)
            ToCheckPoint();
        else
            ToFinalPoint();
    }

    private GameObject CheckEnemy() //Ÿ�� ã�°� �и��� ����
    {
        GameObject closestBlue = null;
        float tempDistance = float.MaxValue;
        Collider[] overlapColliders;
        if (gameObject.layer == 9)
            overlapColliders = Physics.OverlapSphere(transform.position, chaseDistance, LayerMask.GetMask("Blue", "Player"));
        else if (gameObject.layer == 8)
            overlapColliders = Physics.OverlapSphere(transform.position, chaseDistance, LayerMask.GetMask("Red"));
        else return null;

        if(overlapColliders != null && overlapColliders.Length > 0)
        {
            foreach(var collider in overlapColliders)
            {
                if(tempDistance > Vector3.Distance(transform.position, collider.transform.position))
                {
                    closestBlue = collider.gameObject;
                    tempDistance = Vector3.Distance(transform.position, collider.transform.position);
                }
            }
        }

        return closestBlue;
    }

    private void Chase(GameObject target)
    {
        if(Vector3.Distance(target.transform.position, transform.position) <= enemyData.attackRange)
        {
            navAgent.isStopped = true;
            animator.SetBool("isMove", false);
            animator.SetTrigger("OnAttack");
        }
        else
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(target.transform.position);
            animator.SetBool("isMove", true);
        }

        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    /*IEnumerator ToCheckPoint() // �߰� �б��� ��������
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(checkPoint.position);

        animator.SetBool("isMove", true);

        while(navAgent.pathPending || navAgent.remainingDistance > navAgent.stoppingDistance || !isCheckPoint)
        {
            yield return null;
        }

        isCheckPoint = true;
    }

    IEnumerator ToFinalPoint()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(finalPoint.position); //��𰡳�?

        animator.SetBool("isMove", true);

        while (navAgent.pathPending || navAgent.remainingDistance > navAgent.stoppingDistance)
        {
            yield return null;
        }
    }*/

    private void ToCheckPoint()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(checkPoint.position);
        animator.SetBool("isMove", true);
    }

    private void ToFinalPoint()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(finalPoint.position);
        animator.SetBool("isMove", true);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Hit " + damage);
        animator.SetTrigger("OnHit");

        enemyData.health -= damage;
        if(enemyData.health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.pool.Release(gameObject);
    }

    public void OnAttackCollision()
    {
        meleeAttackCollison.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
            isCheckPoint = true;
    }
}
