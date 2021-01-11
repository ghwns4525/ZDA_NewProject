using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using SG;

public enum BossState
{
    idle,   // 대기
    chase,  // 추적 
    attack, // 공격
    stun,   // 스턴
    die,    // 죽음 
    lose,   // 플레이어 죽음
}
public class Sc_BaseBossMng : MonoBehaviour
{

    public BossState bossStateChecker = BossState.idle;

    #region == 컴포넌트 == 

    public Sc_BossAnimationHandler sc_BossAnimationHandler;
    public Sc_BossAttackHandler sc_BossAttackHandler;
    public Sc_BossMoveAiHandler sc_BossMoveAiHandler;
    public Sc_BossLocomotionHandler sc_BossLocomotionHandler;
    public Sc_BossStats sc_BossStats;
    public GameObject sc_PlayerStats;
    public Rigidbody rigidbody;
    public Animator animator; // 애니메이션

    #endregion

    public bool flagTest;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    void Update()
    {
        float delta = Time.deltaTime;
        // 상태를 체크하고 변화
        StateCheckAnimationPlayHandler(delta);
        // 죽음
        // 스턴
        // 추적
        sc_BossMoveAiHandler.TraceTargetHandler(bossStateChecker, delta);
        // 공격
        sc_BossAttackHandler.AttackHandler(bossStateChecker, delta);
  
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        // 애니메이션, 애니메이션을 통한 짧은 이동
        sc_BossLocomotionHandler.AnimatorRootMotionMoveHandler(delta);
    }



    void Initialize()
    {
        sc_BossAnimationHandler = GetComponentInChildren<Sc_BossAnimationHandler>();
        sc_BossAttackHandler = GetComponent<Sc_BossAttackHandler>();
        sc_BossMoveAiHandler = GetComponent<Sc_BossMoveAiHandler>();
        sc_BossLocomotionHandler = GetComponent<Sc_BossLocomotionHandler>();
        sc_BossStats = GetComponent<Sc_BossStats>();
        sc_PlayerStats = GameObject.FindWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void StateCheckAnimationPlayHandler(float delta)
    {
        // == anitime Start(어떤 상태든 체크를 진행한다.) == 

        // == anitime End == 
        if (bossStateChecker == BossState.stun)
        {
            return;
        }
        if (bossStateChecker == BossState.die)
        {
            return;
        }
        if(bossStateChecker == BossState.lose)
        {
            return;
        }
        // 죽음 상태로 진입 체크
        if (sc_BossStats.IsBossDie())
        {
            bossStateChecker = BossState.die;
            sc_BossStats.DieHandler(bossStateChecker);
            return;
        }

        // 플레이어 사망 체크
        if(sc_PlayerStats.GetComponent<Sc_PlayerStats>().IsPlayerDie())
        {
            bossStateChecker = BossState.lose;
            sc_BossStats.LoseHandler(bossStateChecker);
            Debug.Log("플레이어 사망");
            return;
        }

        // 스턴은 게이지가 다 찬 상태에서 사용해야 함 이건 테스트 용
        if (sc_BossStats.StunHandler())
        {
            bossStateChecker = BossState.stun;
            sc_BossStats.StunHandler(bossStateChecker);
            return;
        }
        if (sc_BossAnimationHandler.isInteracting) // 예외처리 : 애니메이션 실행중에는 체크하지 않는다. 
        {
            return;
        }

       
        // bossStateChecker 가 바뀌고 아래에서 또 바뀌어서 그럼. 
        // 

        // 히트 되었을때  // 히트 된 부분은 따로 설정 해야함.
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
