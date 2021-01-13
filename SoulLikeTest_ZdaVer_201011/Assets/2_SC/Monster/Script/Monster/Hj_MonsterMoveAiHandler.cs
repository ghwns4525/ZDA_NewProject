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
    bool isTrace;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float traceSpeed;

    private GameObject player;
    private Vector3 playerPoint;

    Hj_MonsterAnimationHandler hj_animationHandler;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        hj_animationHandler = GetComponentInChildren<Hj_MonsterAnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPoint = player.transform.position;


        if (isPatrol)
        {
            Patrol(playerPoint);
        }
        if (!isPatrol)
        {
            Stop();
        }
        if (isTrace)
        {
            Trace(playerPoint);
        }
    }

    void Trace(Vector3 target)
    {
        agent.isStopped = false;
        agent.destination = target;
        if(!isPatrol)
        {
            // 애니메이션을 walk로 넣어야할듯 뛰는게 안보임
            hj_animationHandler.animator.SetFloat("Patrol", 0.1f);
        }
        // 속도 조절
        agent.speed = traceSpeed;
        Debug.Log(agent.speed);
    }

    void Patrol(Vector3 target)
    {
        if (!isTrace)
        {
            agent.isStopped = false;
            // 목적지를 설정한다.(플레이어 혹은 일정한 지역이 될수도 있음)
            agent.destination = target;

            // 애니메이션은 blend값을 조절해서 멈추고 걷고 뛰게 조절
            hj_animationHandler.animator.SetFloat("Patrol", 0.1f);

            // 속도 조절
            agent.speed = walkSpeed;
            Debug.Log(agent.speed);
        }
    }

    void Stop()
    {
        if(!isTrace)
        {
            agent.isStopped = true;
            // 이렇게 하고 애니메이션은 idle을 넣으면 순찰을 안하는 효과
            //hj_animationHandler.PlayTargetActionAnimation("idle");
            hj_animationHandler.animator.SetFloat("Patrol", 0);
        }

    }
}
