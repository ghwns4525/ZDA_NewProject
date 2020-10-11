using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerAttacker : MonoBehaviour
{

    Sc_AnimatorHandler animatorHandler;
    Sc_InputHandler inputHandler;
    Sc_PlayerManager playerManager;
    Animator animator;
    public string[] lightAttackList;
    public string[] heavyAttackList;

    public string lastAttack;
    /// <summary>
    /// 현재 공격한 카운트 
    /// </summary>
    public int attackCount;



    private void Awake()
    {
        animatorHandler = GetComponentInChildren<Sc_AnimatorHandler>();
        inputHandler = GetComponent<Sc_InputHandler>();
        playerManager = GetComponent<Sc_PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        
    }

    public void HandleWeaponCombo(Sc_WeaponItem weapon ,AttackType attackType)
    {
        if(inputHandler.comboFlag)
        {
            animatorHandler.animator.SetBool("canDoCombo", false);
            Sc_PlayerManager.ins.isAttack = true;
            if (attackType == AttackType.LightAttack)
            {
                if (attackCount == 0)
                {
                    animatorHandler.PlayTargetAnimation(weapon.Light_Attack_B, true , false);
                    attackCount++;// 예외처리 해야할 듯?
                    playerManager.SetStamina(playerManager.staminaValue_LightAttack);
                }
                else if (attackCount == 1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.Light_Attack_C, true , false);
                    attackCount++;// 예외처리 해야할 듯?
                    playerManager.SetStamina(playerManager.staminaValue_LightAttack);
                }
            }
            else if(attackType == AttackType.HeavyAttack )
            {
                if (attackCount == 0)
                {
                    animatorHandler.PlayTargetAnimation(weapon.Heavy_Attack_E, true ,false);
                    attackCount++;// 예외처리 해야할 듯?
                    playerManager.SetStamina(playerManager.staminaValue_HeavyAttack);
                }
                else if (attackCount == 1)
                {
                    //
                }
            }

            /*if (lightAttackList[1] == weapon.Light_Attack_B)
            {
                animatorHandler.PlayTargetAnimation(weapon.Light_Attack_B, true);
                attackCount++;// 예외처리 해야할 듯?
            }
            else if (lightAttackList[2] == weapon.Light_Attack_C)
            {
                animatorHandler.PlayTargetAnimation(weapon.Light_Attack_C, true);
                attackCount++;// 예외처리 해야할 듯?
            }*/
        }
        
    }
    public void HandleLightAttack(Sc_WeaponItem weapon)
    {
        /*if (inputHandler.rollFlag)
        {
            return;
        }
        if (playerManager.isInteracting)
        {
            return;
        }*/
        
        animatorHandler.PlayTargetAnimation(weapon.Light_Attack_A,true ,false);
        playerManager.SetStamina(playerManager.staminaValue_LightAttack);
        attackCount = 0;

        Sc_PlayerManager.ins.isAttack = true;
    }

    public void HandleHeavyAttack(Sc_WeaponItem weapon)
    {
        if (inputHandler.rollFlag)
        {
            return;
        }
        if (playerManager.isInteracting)
        {
            return;
        }
        
        animatorHandler.PlayTargetAnimation(weapon.Heavy_Attack_D, true ,false);
        playerManager.SetStamina(playerManager.staminaValue_HeavyAttack);
        attackCount = 0;
    }
}
