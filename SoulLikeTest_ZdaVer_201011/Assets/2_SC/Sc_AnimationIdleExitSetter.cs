using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_AnimationIdleExitSetter : StateMachineBehaviour
{
    public Sc_BaseBossMng boss;
    public Hj_MonsterBaseMng monster;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(boss == null)
        {
            boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Sc_BaseBossMng>();
            
        }
        if(monster == null)
        {
            monster = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Hj_MonsterBaseMng>();
        }
         
        boss.GetComponent<Sc_BaseBossMng>().bossStateChecker = BossState.idle;
        monster.GetComponent<Hj_MonsterBaseMng>().monsterStateCheck = MonsterState.idle;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
