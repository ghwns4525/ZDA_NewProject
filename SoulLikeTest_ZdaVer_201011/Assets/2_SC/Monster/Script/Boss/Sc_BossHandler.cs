using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

// 스태미나 총관리
// Enemy 컴포넌트 관리

public class Sc_BossHandler : MonoBehaviour
{
    public BossState bossStateChecker = BossState.idle;

    /// <summary>
    /// 특수 조건에 의한 특수 공격
    /// </summary>
    public bool flag_BossSkill;


    #region == 컴포넌트 == 

    public Sc_BossAnimationHandler sc_BossAnimationHandler;
    public Sc_BossAttackHandler sc_BossAttackHandler;
    public Sc_BossMoveAiHandler sc_BossMoveAiHandler;
    public Sc_BossLocomotionHandler sc_BossLocomotionHandler;
    public Sc_BossStats sc_BossStats;
    public Rigidbody rigidbody;
    public Animator animator; // 애니메이션

    #endregion

    public bool flagTest;
    private void Awake()
    {
     
    }
    void Start()
    {
        SetStart();
        ResetValue();
    }
    void SetStart()
    {
        sc_BossAnimationHandler = GetComponentInChildren<Sc_BossAnimationHandler>();
        sc_BossAttackHandler = GetComponent<Sc_BossAttackHandler>();
        sc_BossMoveAiHandler = GetComponent<Sc_BossMoveAiHandler>();
        sc_BossLocomotionHandler = GetComponent<Sc_BossLocomotionHandler>();
        sc_BossStats = GetComponent<Sc_BossStats>();
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void ResetValue()
    {

    }


    void Update()
    {
        float delta = Time.deltaTime;
        StateCheckAnimationPlayHandler(delta);
        sc_BossMoveAiHandler.TraceTargetHandler(bossStateChecker, delta);
        sc_BossAttackHandler.AttackHandler(bossStateChecker, delta);
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        sc_BossLocomotionHandler.AnimatorRootMotionMoveHandler(delta);
    }

    void StateCheckAnimationPlayHandler(float delta)
    {
        if (sc_BossAnimationHandler.isInteracting) // Exception : 애니메이션 실행중에는 체크하지 않는다. 
        {
            return;
        }
        // == anitime Start == 


        // 죽음 상태로 진입 체크
        if (sc_BossStats.CurrentHp <= 0)
        {
            bossStateChecker = BossState.die;
        }

        if(flag_BossSkill)
        {
            
        }


        // 히트 되었을때  // 히트 된 부분은 따로 설정 해야함.

        // == anitime End == 
        float dist = Vector3.Distance(sc_BossAttackHandler.Target.transform.position, transform.position);
        //Debug.Log("거리 : " + dist);
       
       
        // == 공격 == 
        if ((dist < sc_BossAttackHandler.AttackDis))
        {
            bossStateChecker = BossState.attack;
        }
        // == 추적 ==
        else if (sc_BossAttackHandler.AttackDis < dist)
        {
            bossStateChecker = BossState.chase;
        }
    }
}
