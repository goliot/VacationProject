using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    public EnemyData enemyData = new EnemyData();

    private Animator animator;
    private NavMeshAgent navAgent;

    [Header("# Chase")]
    [SerializeField]
    private float chaseDistance = 10f;
    [SerializeField]
    private float stopDistance = 3f;
    public GameObject chaseTarget;

    [Header("# AttackCollision")]
    [SerializeField]
    private GameObject meleeAttackCollison;

    [Header("# Destination")]
    [SerializeField]
    public Transform checkPoint;
    [SerializeField]
    public Transform finalPoint;

    private bool isCheckPoint; // 체크포인트에 도달했는지 여부
    private bool isDead;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.isStopped = true;
    }

    private void Start()
    {
        isCheckPoint = false;
        isDead = false;
    }

    public void Init(EnemyData data)
    {
        enemyData = data;
    }

    private void Update()
    {
        if (isDead)
            return;

        chaseTarget = CheckEnemy();
        if (chaseTarget != null)
        {
            StopAllCoroutines();
            Chase(chaseTarget);
            return;
        }

        if (!isCheckPoint)
            ToCheckPoint();
        else
            ToFinalPoint();
    }

    private GameObject CheckEnemy() //타겟 찾는거 분리좀 하자
    {
        GameObject target = null;
        float tempDistance = float.MaxValue;
        Collider[] overlapColliders;
        if (gameObject.layer == 9) // red minion
            overlapColliders = Physics.OverlapSphere(transform.position, chaseDistance, LayerMask.GetMask("Blue", "Player"));
        else if (gameObject.layer == 8) // blue minion
            overlapColliders = Physics.OverlapSphere(transform.position, chaseDistance, LayerMask.GetMask("Red"));
        else return null;

        if(overlapColliders != null && overlapColliders.Length > 0)
        {
            foreach(var collider in overlapColliders)
            {
                if(tempDistance > Vector3.Distance(transform.position, collider.transform.position))
                {
                    target = collider.gameObject;
                    tempDistance = Vector3.Distance(transform.position, collider.transform.position);
                }
            }
        }

        return target;
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
        isDead = true;
        StartCoroutine(CoDie());
    }

    IEnumerator CoDie()
    {
        animator.SetTrigger("OnDead");

        yield return new WaitForSeconds(3f);

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
