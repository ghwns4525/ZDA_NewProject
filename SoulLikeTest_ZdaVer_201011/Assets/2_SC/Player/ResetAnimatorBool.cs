using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{

    public string targetBool;
    public bool status;
    public bool test;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, status);
        Debug.Log("@@@@@ OnStateEnter  " + test);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool(targetBool, true);
        //test = animator.GetBool("isInteracting");
        //Debug.Log(stateInfo.normalizedTime+"__OnUp:: " + test );
    }
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Time + " + stateInfo.normalizedTime);
        //animator.SetBool(targetBool, status);
        //Debug.Log("@@ OnStateMove  " + status);
    }
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*animator.SetBool("isInteracting", false);
        test = animator.GetBool("isInteracting");
        Debug.Log("@@@@@ OnStateExit  " + test);*/
        //Sc_PlayerManager.ins.HandleUpdateClose_IsInteracting();
    }

    

    

}
