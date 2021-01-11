using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sc_BossAttackHandler : MonoBehaviour
{
    
    Sc_BossAnimationHandler sc_BossAnimationHandler;
    Sc_BossHandler sc_BossHandler;
    Sc_BossAttackHandler sc_BossAttackHandler;


    [Header("== Status ==")]

    [SerializeField] float attackRoatationSpeed = 10;

    [SerializeField] float attackDis = 2;

    [SerializeField] GameObject target;


    [Header("== Animation Data == ")]

    [SerializeField] List<Sc_AttackData> sc_AttackDataList;
    [SerializeField] List<string> randomAttackPatternList;


    string aniNameTemp;

    #region == Properties ==

    public GameObject Target
    {
        get
        {
            return target;
        }
    }
    public float AttackDis
    {
        get
        {
            return attackDis;
        }
    }

    #endregion


    void Start()
    {
        sc_BossAnimationHandler = GetComponentInChildren<Sc_BossAnimationHandler>();
        sc_BossHandler = GetComponent<Sc_BossHandler>();
        sc_BossAttackHandler = GetComponent<Sc_BossAttackHandler>();
        target = GameObject.FindGameObjectWithTag("Player");

        RandomAttackPatternConstructor();
    }

    public void AttackHandler(BossState bossState, float delta)
    {
        if (sc_BossAnimationHandler.isInteracting)
        {
            return;
        }

        if (bossState == BossState.attack)
        {
            // 회전 해야함.
            Vector3 targetDir = sc_BossAttackHandler.Target.transform.position - transform.position;
            targetDir.Normalize();

            float rs = attackRoatationSpeed; // 회전속도 
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * delta);
            transform.rotation = targetRotation;

            ResetAniNameTemp_RandAttackPatternList_0th();
            // 공격 모션
/*            if(aniNameTemp == "Atk_Dash")
            {
                sc_BossAnimationHandler.PlayTargetAttackAnimation(aniNameTemp, true, true);
            }*/
                sc_BossAnimationHandler.PlayTargetAttackAnimation(aniNameTemp, true, false);
            
        }
    }

    void RandomAttackPatternConstructor()
    {
        // Rand일때 boss_Attck_Pattern_Temp 초기화한다.
        for (int i = 0; i < 100; i++)
        {
            int rndTemp = Random.Range(0, sc_AttackDataList.Count);
            // 해쉬테이블로 바꿔서 캐스팅 해주던가 바꿔줘야함.
            randomAttackPatternList.Add((sc_AttackDataList[rndTemp].aniName));
        }
        for (int i = 0; i < 100; i++)
        {
            //Debug.Log("생성된 패턴 : [" + i + "]  : " + randomPatternList[i].ToString());
        }
    }

    public void ResetAniNameTemp_RandAttackPatternList_0th()
    {
        aniNameTemp = randomAttackPatternList[0].ToString();
        randomAttackPatternList.RemoveAt(0);
    }
    

}
