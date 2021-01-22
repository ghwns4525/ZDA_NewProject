using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    idle,       // 기본값
    waiting,    // 대기
    boundary,   // 경계
    chase,      // 추격
    attack,     // 공격
    comeback,   // 복귀
    die,        // 죽음
}

public class Hj_MonsterBaseMng : MonoBehaviour
{
    [Header("상태를 변경할 거리")]
    [SerializeField]
    float AttackDistance;    
    [SerializeField]
    float ChaseDistance;

    
    public MonsterState monsterStateCheck = MonsterState.idle;
    
    Hj_MonsterAnimationHandler hj_MonsterAnimationHandler;
    Hj_MonsterMoveAiHandler hj_MonsterMoveAiHandler;

    // Start is called before the first frame update
    void Start()
    {
        hj_MonsterMoveAiHandler = GetComponent<Hj_MonsterMoveAiHandler>();
        hj_MonsterAnimationHandler = GetComponentInChildren<Hj_MonsterAnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterStateChacker();
    }

    void MonsterStateChacker()
    {
        float dist = Vector3.Distance(hj_MonsterMoveAiHandler.TargetPlayer.transform.position, this.transform.position);

        if (dist < AttackDistance)
        {
            // 몬스터의 상태를 공격으로 바꿈
            monsterStateCheck = MonsterState.attack;
        }
        else if (hj_MonsterMoveAiHandler.IsComeback)
        {
            // 몬스터의 상태를 복귀로 바꿈
            monsterStateCheck = MonsterState.comeback;
        }
        else if (dist > AttackDistance && dist < ChaseDistance && !hj_MonsterMoveAiHandler.IsComeback && !hj_MonsterAnimationHandler.isInteracting)
        {
            // 몬스터의 상태를 추격으로 바꿈
            monsterStateCheck = MonsterState.chase;
        }        
        else if (hj_MonsterMoveAiHandler.IsPatrol && !hj_MonsterMoveAiHandler.IsComeback && !hj_MonsterAnimationHandler.isInteracting)
        {
            // 몬스터의 상태를 경계로 바꿈
            monsterStateCheck = MonsterState.boundary;
        }
        else if(!hj_MonsterMoveAiHandler.IsPatrol)
        {
            // 몬스터의 상태를 대기로 바꿈
            monsterStateCheck = MonsterState.waiting;
        }
        else if (hj_MonsterAnimationHandler.isInteracting)
        {
            monsterStateCheck = MonsterState.idle;
        }
        Debug.Log(monsterStateCheck);
    }
}
