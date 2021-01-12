using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hj_MonsterMoveAiHandler : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    Animator animator;
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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
        
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
            animator.CrossFade("Walk_Front", 0.12f);
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

            // 애니메이션은 walk로 넣는다.
            animator.CrossFade("Walk_Front", 0.12f);
            // 속도 조절
            agent.speed = walkSpeed;
            Debug.Log(agent.speed);
        }
    }

    void Stop()
    {
        agent.isStopped = true;
        // 이렇게 하고 애니메이션은 idle을 넣으면 순찰을 안하는 효과
        animator.CrossFade("idle", 0.12f);
    }
}
