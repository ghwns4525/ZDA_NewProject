using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hj_MonsterMoveAiHandler : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    bool isPatrol;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float traceSpeed;
    [SerializeField]
    Vector3 startingPosition;
    [SerializeField]
    bool isStarting;
    [SerializeField]
    float chaseRange;
    [SerializeField]
    Vector3 chaseLimit;
    [SerializeField]
    Vector3 chaseLimitC;

    bool isTrace;
    

    GameObject player;
    private Vector3 playerPoint;

    Hj_MonsterAnimationHandler hj_animationHandler;
    Hj_MonsterBaseMng hj_MonsterBaseMng;

    public GameObject TargetPlayer
    {
        get
        {
            return player;
        }
    }

    public bool IsPatrol
    {
        get 
        {
            return isPatrol;
        }
    }


    public Vector3 ChaseLimit
    {
        get
        {
            return chaseLimit;
        }
    }
    public Vector3 ChaseLimitC
    {
        get
        {
            return chaseLimitC;
        }
    }

    void Awake()
    {
        startingPosition = this.transform.position;
        chaseLimit.x = startingPosition.x + chaseRange;
        chaseLimit.z = startingPosition.z + chaseRange;
        chaseLimitC.x = startingPosition.x - chaseRange;
        chaseLimitC.z = startingPosition.z - chaseRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        hj_animationHandler = GetComponentInChildren<Hj_MonsterAnimationHandler>();
        hj_MonsterBaseMng = GetComponent<Hj_MonsterBaseMng>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPoint = player.transform.position;

        Patrol(playerPoint);
        Stop();
        Chase(playerPoint);
        Comeback(startingPosition);

        if (this.transform.position == startingPosition)
        {
            isStarting = true;
        }
        else
        {
            isStarting = false;
        }
    }

    void Chase(Vector3 target)
    {
        if(hj_MonsterBaseMng.monsterStateCheck == MonsterState.chase)
        {
            agent.isStopped = false;
            agent.destination = target;
            // 애니메이션을 walk로 넣어야할듯 뛰는게 안보임
            hj_animationHandler.animator.SetFloat("PatrolY", 0.1f);
            // 속도 조절
            agent.speed = traceSpeed;
            Debug.Log("추격");
        }        
    }

    void Patrol(Vector3 target)
    {
        if(hj_MonsterBaseMng.monsterStateCheck == MonsterState.boundary)
        {
            agent.isStopped = false;
            // 목적지를 설정한다.(플레이어 혹은 일정한 지역이 될수도 있음)
            agent.destination = target;

            // 애니메이션은 blend값을 조절해서 멈추고 걷고 뛰게 조절
            hj_animationHandler.animator.SetFloat("PatrolY", 0.1f);

            // 속도 조절
            agent.speed = walkSpeed;
            Debug.Log("경계");
        }        
    }

    void Stop()
    {
        if (hj_MonsterBaseMng.monsterStateCheck == MonsterState.idle)
        {
            Debug.Log("정지");
            if (isStarting)
            {
                agent.isStopped = true;
                // 이렇게 하고 애니메이션은 idle을 넣으면 순찰을 안하는 효과
                //hj_animationHandler.PlayTargetActionAnimation("idle");
                hj_animationHandler.animator.SetFloat("PatrolY", 0);
            }
        }          
    }

    void Comeback(Vector3 target)
    {
        if (hj_MonsterBaseMng.monsterStateCheck == MonsterState.comeback)
        {
            if (!isStarting)
            {
                //isPatrol = false;
                // 몬스터가 처음 있던 자리로 복귀
                agent.destination = target;
            }
        }
    }

}
