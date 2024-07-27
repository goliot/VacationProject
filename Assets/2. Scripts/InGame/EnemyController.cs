using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.SceneView;

public class EnemyController : MonoBehaviour
{
    public Stats stats;

    private Animator animator;
    // NavMeshAgent를 위한 변수
    private NavMeshAgent navAgent;

    // 플레이어의 위치를 추적하기 위한 변수
    private Transform player;

    // 몬스터가 플레이어를 추적할 거리
    public float chaseDistance = 10f;

    // 몬스터와 플레이어의 거리
    private float distanceToPlayer;

    // 몬스터의 초기 위치를 저장할 변수
    private Vector3 startPosition;

    // 몬스터가 플레이어를 추적할 최소 거리
    public float stopDistance = 3f;

    // 몬스터가 정찰할 위치 배열
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Player 태그를 가진 오브젝트를 찾아서 플레이어로 설정
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // 몬스터의 초기 위치 저장
        startPosition = transform.position;
    }

    /*private void Update()
    {
        // 플레이어와의 거리를 계산
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 플레이어가 추적 거리 안에 있을 경우
        if (distanceToPlayer <= chaseDistance)
        {
            // 플레이어를 추적
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            // 정찰 지점으로 이동
            //Patrol();
        }
    }*/

    // 플레이어를 추적하는 함수
    void ChasePlayer(float distanceToPlayer)
    {
        // 플레이어가 너무 가까이 있을 경우
        if (distanceToPlayer <= stopDistance)
        {
            // 멈춘다
            navAgent.isStopped = true;
        }
        else
        {
            // 플레이어를 따라간다
            navAgent.isStopped = false;
            navAgent.SetDestination(player.position);
        }

        // 항상 플레이어를 바라보도록 설정
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // 정찰하는 함수
    void Patrol()
    {
        // 현재 정찰 지점에 도달했는지 확인
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            // 다음 정찰 지점으로 이동
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
