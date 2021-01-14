using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    idle,       // 대기
    boundary,   // 경계
    chase,      // 추격
    attack,     // 공격
    comeback,   // 복귀
    die,        // 죽음
}

public class Hj_MonsterBaseMng : MonoBehaviour
{
    [SerializeField]
    float AttackDistance;
    [SerializeField]
    float ChaseDistance;
    
    public MonsterState monsterStateCheck = MonsterState.idle;

    Hj_MonsterMoveAiHandler hj_MonsterMoveAiHandler;

    // Start is called before the first frame update
    void Start()
    {
        hj_MonsterMoveAiHandler = GetComponent<Hj_MonsterMoveAiHandler>();
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
        else if (dist > AttackDistance && dist < ChaseDistance)
        {
            // 몬스터의 상태를 추격으로 바꿈
            monsterStateCheck = MonsterState.chase;
        }
        else if (this.transform.position.x > hj_MonsterMoveAiHandler.ChaseLimit.x ||
            this.transform.position.z > hj_MonsterMoveAiHandler.ChaseLimit.z ||
            this.transform.position.x < hj_MonsterMoveAiHandler.ChaseLimitC.x ||
            this.transform.position.z < hj_MonsterMoveAiHandler.ChaseLimitC.z)
        {
            // 몬스터의 상태를 복귀로 바꿈
            monsterStateCheck = MonsterState.comeback;
        }
        else if (hj_MonsterMoveAiHandler.IsPatrol && 
            (this.transform.position.x < hj_MonsterMoveAiHandler.ChaseLimit.x ||
            this.transform.position.z < hj_MonsterMoveAiHandler.ChaseLimit.z ||
            this.transform.position.x > hj_MonsterMoveAiHandler.ChaseLimitC.x ||
            this.transform.position.z > hj_MonsterMoveAiHandler.ChaseLimitC.z))
        {
            // 몬스터의 상태를 경계로 바꿈
            monsterStateCheck = MonsterState.boundary;
        }
        else if(!hj_MonsterMoveAiHandler.IsPatrol)
        {
            // 몬스터의 상태를 대기로 바꿈
            monsterStateCheck = MonsterState.idle;
        }
        
        Debug.Log(monsterStateCheck);
    }
}
