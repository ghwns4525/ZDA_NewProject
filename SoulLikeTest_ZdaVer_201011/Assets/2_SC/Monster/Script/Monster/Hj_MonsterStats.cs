using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hj_MonsterStats : MonoBehaviour
{
    [SerializeField]
    int maxHp;

    int currentHp;
    bool isDie;

    Hj_MonsterAnimationHandler hj_MonsterAnimationHandler;
    Hj_MonsterBaseMng hj_MonsterBaseMng;

    public bool IsDie
    {
        get
        {
            return isDie;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        hj_MonsterAnimationHandler = GetComponentInChildren<Hj_MonsterAnimationHandler>();
        hj_MonsterBaseMng = GetComponent<Hj_MonsterBaseMng>();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterDie();
        Attack();
    }

    public void TakeDamage(int Damage)
    {
        currentHp = currentHp - Damage;
        hj_MonsterAnimationHandler.PlayTargetActionAnimation(EAniName_Action.Act_Hit.ToString(), true);
        Debug.Log(currentHp);
    }

    void MonsterDie()
    {
        if (currentHp <= 0)
        {
            isDie = true;
            hj_MonsterAnimationHandler.PlayTargetActionAnimation("Act_Die", true);
        }
        else
        {
            isDie = false;
        }
    }

    void Attack()
    {
        if (hj_MonsterBaseMng.monsterStateCheck == MonsterState.attack)
        {
            hj_MonsterAnimationHandler.PlayTargetActionAnimation(EAniName_Action.Act_Attack.ToString(), true);
            Debug.Log("공격실행");
        }
    }
}
