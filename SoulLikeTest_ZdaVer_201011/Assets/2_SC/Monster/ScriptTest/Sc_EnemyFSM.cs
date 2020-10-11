using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_EnemyFSM : Sc_EnemyBase
{
    public enum State
    {
        Idle,
        Move,
        Attack,
    };
    public State currentState = State.Idle;

    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);
    WaitForSeconds Delay250 = new WaitForSeconds(0.25f);

    void Start()
    {
        base.Start();
        Debug.Log("Start - State :" + currentState.ToString());
        StartCoroutine(FSM());
    }


    protected virtual IEnumerator FSM()
    {
        yield return null;

        while (true)
        {
            // currentState의 이름과 함수이름을 같게 만들어서 바로 처리하게 할 수 있음
            // 이게 곧 스위치 문이 되는것이지!
            yield return StartCoroutine(currentState.ToString());
        }
    }

    protected virtual IEnumerator Idle()
    {
        yield return null;
        // !중요 
        // 애니메이션 중복 실행을 막기 위해서 이런 처리를 한다. 
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            Anim.SetTrigger("Idle");
        }

        // Idle에서 상태전이를 한다. 
        if (CanAtkStateFunDistance()) // 공격 거리가 되는지 먼저 체크한다. 
        {
            if (canAtk)
            {
                currentState = State.Attack;
            }
            else
            {
                currentState = State.Idle;
                transform.LookAt(Player.transform.position);
            }
        }
        else
        {
            currentState = State.Move;
        }
    }

    protected virtual void AtkEffect() { }

    protected virtual IEnumerator Attack()
    {
        yield return null;
        //Atk

        nvAgent.stoppingDistance = 0f;
        nvAgent.isStopped = true;
        nvAgent.SetDestination(Player.transform.position);
        yield return Delay500;

        nvAgent.isStopped = false;
        nvAgent.speed = 30f;
        canAtk = false;

        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("stun"))
        {
            Anim.SetTrigger("Attack");
        }
        AtkEffect();
        yield return Delay500;

        nvAgent.speed = moveSpeed;
        nvAgent.stoppingDistance = attackRange;
        currentState = State.Idle;
    }

    protected virtual IEnumerator Move()
    {
        yield return null;
        //Move
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            Anim.SetTrigger("Walk");
        }
        if (CanAtkStateFunDistance() && canAtk)
        {
            currentState = State.Attack;
        }
        else if (distance > playerRealizeRange)
        {
            nvAgent.SetDestination(transform.parent.position - Vector3.forward * 5f);
        }
        else
        {
            nvAgent.SetDestination(Player.transform.position);
        }
    }
    
}
