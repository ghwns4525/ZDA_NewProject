using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sc_BossMoveAiHandler : MonoBehaviour
{
    public List<Transform> wayPoints;      // 순찰 지점들을 저장하기 위한 List타입의 변수
    public int nextWayIdx;                 // 다음 순찰 지점 배열의 인덱스



    public float patrolSpeed = 1.5f;
    public float traceSpeed;
    public float moveAmount;

   
    private float damping = 1.0f;            //회전할 때의 속도를 조절하는 계수

    
    public NavMeshAgent agent;              //NavMeshAgent 컴포넌트를 저장할 변수
    private Transform enemyTr;              //적 캐릭터의 Transform 컴포넌트를 저장할 변수

    //순찰 여부를 판단하는 변수
    private bool is_patrolling;
    public bool test;
    private GameObject target;

    Vector3 traceTarget;

    Sc_BossAnimationHandler sc_BossAnimationHandler;
    Sc_BossHandler sc_BossHandler;

    void Start()
    {
        Starter();
    }

    void Starter()
    {
        // 적 캐릭터의 Trasform 컴포넌트 추출 후 변수에 저장한다.
        enemyTr = GetComponent<Transform>();
        //NavMeshAgent 컴포넌트를 추출한 후 변수에 저장
        agent = GetComponent<NavMeshAgent>();
        //목적지에 가까워질수록 속도를 줄이는 옵션을 비활성화
        agent.autoBraking = false;
        //자동으로 회전하는 기능을 비활성화
        agent.updateRotation = false;

        agent.speed = patrolSpeed;
        sc_BossAnimationHandler = GetComponentInChildren<Sc_BossAnimationHandler>();
        sc_BossHandler = GetComponent<Sc_BossHandler>();
        target = GameObject.FindGameObjectWithTag("Player");

        var group = GameObject.Find("PatrolAreas");
        if (group != null)
        {
            //WayPointGroup 하위에 있는 모든 Transform컴포넌트를 추출한 후 
            // List  타입의 wayPoints 배열에 추가 
            // 단 이렇게 하면 부모가 배열 0번째로 들어가게 된다. 
            group.GetComponentsInChildren<Transform>(wayPoints);
            // 그래서 지워주는 것임 
            wayPoints.RemoveAt(0);

            //첫 번째로 이동할 위치를 불규칙하게 추출
            nextWayIdx = UnityEngine.Random.Range(0, wayPoints.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 nomalizedVelocity = agent.desiredVelocity;
        nomalizedVelocity.Normalize();
        float x = nomalizedVelocity.x;
        float z = nomalizedVelocity.z;
        moveAmount = Mathf.Clamp01(x) + Mathf.Abs(z);
        test = agent.isStopped;

        //agent.destination = GetComponent<Sc_Boss>().target.transform.position;
        // 캐릭터가 이동중일때만 회전을 한다.
        if (agent.isStopped == false)
        {
            //NavMeshAgent가 가야 할 방향 벡터를 쿼터니언 타입의 각도로 변환
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            //보간 함수를 사용해 점진적으로 회전시킴
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }

        // 순찰 모드가 아닐 경우에는 이후 로직을 수행하지 않음
        if (!is_patrolling)
        {
            return;
        }

        // NavMeshAgent가 이동하고 있는지 ( 속도가 0.02면 거의 멈췄다고 판단 한거임 
        // 목적지에 도착했는지 여부를 계산 
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f &&
           agent.remainingDistance <= 0.5f)
        {
            //다음 목적지의 배열 첨자를 계산
            //nextIdx = ++nextIdx % wayPoints.Count;
            nextWayIdx = UnityEngine.Random.Range(0, wayPoints.Count);
        }
    }

    public bool flag_Trace;

    public void TraceTargetHandler(BossState bossState, float delta)
    {
        if (sc_BossAnimationHandler.isInteracting)
        {
            Stop();
            sc_BossAnimationHandler.UpdateAnimatorValues(0, 0, false);
            return;
        }
        if (bossState == BossState.chase)
        {
            sc_BossAnimationHandler.UpdateAnimatorValues(moveAmount, 0, false);
            damping = 3.0f;
            traceTarget = target.transform.position;
            Trace(traceTarget);
            Debug.DrawRay(agent.destination, Vector3.up * 2f, Color.red, 2f);
            
            //Debug.Log("moveAmount : "+ moveAmount);
        }
        else
        {
            Stop();
            sc_BossAnimationHandler.UpdateAnimatorValues(0, 0, false);
        }
    }
    void Trace(Vector3 pos)
    {
        // 최단거리 경로가 아직 끝나지 않았을때는 리턴시킨다.
        if (agent.isPathStale)
            return;
        // 목표위치를 설정한다. 
        agent.destination = pos;
        // 내비게이션 기능을 활성화 시킨다.
        agent.isStopped = false;
    }
    public void Stop()
    {
        agent.isStopped = true;
        //바로 정지하기 위해 속도를 0으로 설정
        agent.velocity = Vector3.zero;
        is_patrolling = false;
    }
}
