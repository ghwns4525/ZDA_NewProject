using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class Sc_BossStats : MonoBehaviour
{
    // Boss HP
    [SerializeField]
    int healthLevel = 10;
    [SerializeField]
    int maxHp;
    int currentHp;
    public int CurrentHp
    {
        get
        {
            return currentHp;
        }
    }

    bool isDie;
    /// <summary>
    /// 죽었을 때 상태
    /// </summary>
    public bool IsDie
    {
        get
        {
            return isDie;
        }
    }

    bool isStun;
    /// <summary>
    ///  스턴 당했을 때 상태
    /// </summary>
    public bool IsStun
    {
        get
        {
            return isStun;
        }
    }



    // Boss 스턴 스텟

    [SerializeField]
    int stunGaugeLevel = 10;
    [SerializeField]
    int stunGaugeMax;
    public int stunGaugeCurrent;

   // 행동 불가

    public Sc_HealthBar healthBar;
    public Sc_HealthBar stunGaugeBar;

    Sc_BossAnimationHandler animatorHandler;

    private void Awake()
    {
        animatorHandler = GetComponentInChildren<Sc_BossAnimationHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHp = SetMaxHealthFromHealthLevel();
        stunGaugeMax = SetMaxStunFromHealthLevel();
        currentHp = maxHp;
        stunGaugeCurrent = stunGaugeMax;
        healthBar.SetMaxHealth(maxHp);
        stunGaugeBar.SetMaxHealth(stunGaugeMax);
    }
    #region == Hp ==

    private int SetMaxHealthFromHealthLevel()
    {
        maxHp = healthLevel * 10;
        return maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp = currentHp - damage;
        TakeStunGauge(damage/2);

        healthBar.SetCurrentHealth(currentHp);
        animatorHandler.PlayTargetActionAnimation(animatorHandler.GetAniName(EAniName_Action.Hit), true , false);
        if (currentHp <= 0)
        {
            currentHp = 0;
            animatorHandler.PlayTargetActionAnimation(animatorHandler.GetAniName(EAniName_Action.Die), true, false);
        }
    }

    public bool IsBossDie()
    {
        if (currentHp <= 0)
        {
            isDie = true;
            return true;
        }
        else
        {
            isDie = false;
            return false;
        }
    }

    public void DieHandler(BossState bossState)
    {
        if(bossState == BossState.die)
        {
            animatorHandler.PlayTargetActionAnimation(EAniName_Action.Die.ToString(), true, false);
        }
    }

    #endregion


    #region == Stun == 
    private int SetMaxStunFromHealthLevel()
    {
        stunGaugeMax = stunGaugeLevel * 10;
        return stunGaugeMax;
    }
    public void TakeStunGauge(int damage)
    {
        stunGaugeCurrent = stunGaugeCurrent - damage;
        stunGaugeBar.SetCurrentHealth(stunGaugeCurrent);
        if (currentHp <= 0)
        {
            stunGaugeCurrent = 0;
            animatorHandler.PlayTargetActionAnimation(animatorHandler.GetAniName(EAniName_Action.StunStart), true, false);
        }
    }

    public bool StunHandler()
    {
        if(stunGaugeCurrent <= 0)
        {
            isStun = true;
            animatorHandler.animator.SetBool("IsStun",true);
            return true;
        }
        else
        {
            isStun = false;
            return false;
        }
    }

    public void StunHandler(BossState bossState)
    {
        if (bossState == BossState.stun)
        {
            // 버그 : 계속 호출해서 계속 애니메이션이 호출됨.
            // 해결 : 한번만 호출되게 하자

            animatorHandler.PlayTargetActionAnimation(EAniName_Action.StunStart.ToString(), true, false);
        }
    }



    #endregion
}

