using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace SG
{
    public enum AnimationName_Attack
    {
        None,
        Heavy_Attack_Temp_1,
        Heavy_Attack1,
        Heavy_Attack2,
        Light_Attack1,
        Light_Attack2
    }

    public class Sc_AnimatorHandler : MonoBehaviour
    {
        Sc_PlayerManager playerManager;
        public Animator animator;
        Sc_InputHandler inputHandler;
        Sc_PlayerLocomotionHandler sc_PlayerLocomotionHandler;
        Sc_PlayerAttacker sc_PlayerAttacker;
        Rigidbody rigidbody;

        int vertical;
        int horizontal;

        public bool canRatate;

        [Header("AnimationName _ Attack")]

        public string aniName_OverrideIdie;
        public string aniName_Light_AttackA;
        public string aniName_Light_AttackB;
        public string aniName_Light_AttackC;
        public string aniName_Heavy_AttackD;
        public string aniName_Heavy_AttackE;

        [Header("AnimationName _ Action")]

        public string aniName_Reload;
        public string aniName_Rolling;
        public string aniName_Falling;
        public string aniName_Land;
        public string aniName_Damage;
        public string aniName_Dead;

        /// <summary>
        /// 현재 상태를 담는 변수
        /// </summary>
        public string CurrentAni = "";
        /// <summary>
        /// 현재 상태가 바뀌는 변수
        /// </summary>
        public string CurrentAniTemp = "";

        public bool applyRootMotion;
        public void Initialized()
        {
            playerManager = GetComponentInParent<Sc_PlayerManager>();
            animator = GetComponent<Animator>();
            sc_PlayerLocomotionHandler = GetComponentInParent<Sc_PlayerLocomotionHandler>();
            inputHandler = GetComponentInParent<Sc_InputHandler>();
            sc_PlayerAttacker = GetComponentInParent<Sc_PlayerAttacker>();
            rigidbody = GetComponentInParent<Rigidbody>();


            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
            CurrentAni = aniName_OverrideIdie;
            CurrentAniTemp = aniName_OverrideIdie;
        }
        private void Update()
        {
            //animator.SetFloat(vertical, Mathf.Clamp(animator.GetFloat(vertical), -1, 1));
            //animator.SetFloat(horizontal, Mathf.Clamp(animator.GetFloat(horizontal), -1, 1)); // dampTIme 은 또ㅓ 뭐임? 
        }
        public void UpdateAnimatorLocomotionValues (float verticalMovement , float horizontalMovement, bool isSprinting)
        {
            #region Vertical

            float v = 0; 
            if(verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if(verticalMovement >= 0.55f)
            {
                v = 1; 
            }
            else if(verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if(verticalMovement <= -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement >= 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement <= -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion
            if(isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }
            

            animator.SetFloat(vertical , v , 0.1f , Time.deltaTime);
            animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime); // dampTIme 은 또ㅓ 뭐임? 

            // 롤링시에 값을 유지하기 위해서 들어오지 못하게 한다. 
            if(!animator.GetBool("IsRollinjg"))
            {
                UpdateAnimatorValues2(v,h);
            }
        }

        public void UpdateAnimatorValues2(float verticalMovement, float horizontalMovement)
        {
            animator.SetFloat(Animator.StringToHash("DodgeVert"), verticalMovement, 0.1f, Time.deltaTime);
            animator.SetFloat(Animator.StringToHash("DodgeHori"), horizontalMovement, 0.1f, Time.deltaTime);
        }

        public void AnimatiorMoveValuesReset()
        {
            animator.SetFloat(vertical, 0);
            animator.SetFloat(horizontal, 0); 
        }



        public void CanRotate()
        {
            canRatate = true;
        }

        public void StopRatation()
        {
            canRatate = false;
        }

        
        public void PlayTargetAnimation(string targetAnim,  bool isInteracting , bool isRootMotion )
        {
            animator.applyRootMotion = isRootMotion; // 이걸로 루트모션을 쓸껀지 정한다. 
            animator.SetBool("isInteracting", isInteracting);
            animator.SetBool("isAniPlay", true);
            animator.CrossFade(targetAnim, 0.07f); // Defalt 0.12
            Debug.Log("############isInteracting : " + animator.GetBool("isInteracting"));
        }

        #region  == 애니메이션 이벤트 함수 모음 == 
        public void Open_InteractingCutOffFlow()
        {
            Sc_PlayerManager.ins.InteractingCutOffFlow = true;
        }

        public void EnableRoll()
        {
            animator.SetBool("canDoRoll", true);
            
        }
        public void DisableRoll()
        {
            animator.SetBool("canDoRoll", false);
        }
        public void EnableCombo()
        {
            animator.SetBool("canDoCombo" , true);
        }



        public void DisableCombo()
        {
            animator.SetBool("canDoCombo", false);
            Sc_PlayerManager.ins.isAttack = false;
        }

        public void test()
        {
            
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                Debug.Log("asd");
            }
        }
        public void ResetVelocity()
        {
            rigidbody.velocity = Vector3.zero;
        }
        public void EnableVelocity()
        {
            Debug.Log("EnableVelocity실행함");
            sc_PlayerLocomotionHandler.InplaceMoveMotionStarter(speedInitialValue: 7, speedIncreaseValue: 1, duration: 0.15f);
        }
        public void DisableVelocity()
        {
            Debug.Log("###########################DisableVelocity()");
            sc_PlayerLocomotionHandler.InplaceMoveMotionStoper();
        }

        public void EnableVelocityRootMotion()
        {
        }




        // 무적을 만들때 콜라이더를 끄면 운동할때 잡 버그가 너무 많다.
        public void DisablePlayerCollider()
        {
            Debug.Log("###########################DisablePlayerCollider()");
            //Sc_PlayerManager.ins.playerCollider.enabled = false;
        }
        public void EnablePlayerCollider()
        {
            Debug.Log("###########################EnablePlayerCollider()");
            //Sc_PlayerManager.ins.playerCollider.enabled = true;
        }



        #endregion

        // 지우면 RootAimation 적용 안됨.
        private void OnAnimatorMove()
        {

        }

    }
}

