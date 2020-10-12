using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// Sprint 로직 :
/// 쉬프트를 누르면 Speed 값이 증가해서 뛰는 애니메이션이 되게끔 한다. 
namespace SG
{
    public enum AttackType
    {
        None,
        LightAttack,
        HeavyAttack
    }

    public class Sc_InputHandler : MonoBehaviour
    {
        // 이동할때 쓰는 입력값
        public float horizontal;
        public float vertical;

        // 행동시 쓰는 입력값
        public float horizontal2;
        public float vertical2;
        /// <summary>
        /// 입력키의 크기
        /// </summary>
        public float moveAmount;
        public float moveAmount2;
        public float mouseX;
        public float mouseY;

        

        /// <summary> 스페이스 Key :: 롤링 버튼 </summary>
        ///        
        public bool input_SpaceKey;

        /// <summary>
        ///  왼쪽 쉬프트키 :: 달리기
        /// </summary>   
        public bool input_leftShift;

        /// <summary>
        ///  마우스 왼쪽 키 :: 약 공격
        /// </summary>        
        public bool input_mouseLeft;

        /// <summary>
        /// 마우스 오른쪽 키 :: 강 공격
        /// </summary>
        public bool input_mouseRight;

        /// <summary>
        /// 마우스 휠키 버튼 :: 카메라 고정
        /// </summary>
        public bool input_mouseMid;

        /// <summary>
        /// 마우스 휠키 업다운 :: 카메라 고정 타겟 바꾸기
        /// </summary>
        public float input_mouseWheelUpDown;
        /// <summary>
        /// 키보드 R키  :: 재장전
        /// </summary>
        public bool input_R_Key;
    


        public bool rollFlag;
        public bool sprintFlag_shift;
        public float rollInputTimer;
        public bool sprintFlag;
        public bool comboFlag;
        public bool LockOnFlag;
        public bool reloadFlag;
        public bool mouseWheelUpDownFlag;
        
        AttackType attackType = AttackType.None;

        [SerializeField]
        private int fixedCameraTargetsIndexFlag;
       
        /// <summary>
        /// 인풋값들을 초기화( 후처리 해주는 함수 ) 
        /// </summary>
        void ResetInputValue()
        {
            input_SpaceKey = false;
            rollFlag = false;
            sprintFlag = false;
            input_mouseLeft = false;
            input_mouseRight = false;
            LockOnFlag = false;
            input_mouseMid = false;
            attackType = AttackType.None;
            input_R_Key = false;
            reloadFlag = false;
            mouseWheelUpDownFlag = false;
        }

        PlayerCtrl inputActions;
        Sc_PlayerAttacker playerAttacker;
        Sc_PlayerInventory playerInventory;
        Sc_PlayerManager playerManager;
        Sc_AnimatorHandler animatorHandler;
        Sc_PlayerLocomotionHandler playerLocomotionHandler;
        Sc_CameraHandler sc_CameraHandler;


        Vector2 movementInput;
        Vector2 cameraInput;

        


        private void Awake()
        {
            playerAttacker = GetComponent<Sc_PlayerAttacker>();
            playerInventory = GetComponent<Sc_PlayerInventory>();
            playerManager = GetComponent<Sc_PlayerManager>();
            animatorHandler = GetComponentInChildren<Sc_AnimatorHandler>();
            playerLocomotionHandler = GetComponent<Sc_PlayerLocomotionHandler>();
            
        }

        private void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerCtrl();
                inputActions.PlayerMovement.Movement.performed += (inputActions) =>
                movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += (i) => cameraInput = i.ReadValue<Vector2>();
                inputActions.PlayerActions.FixedCameraWheelUpDown.performed += i => input_mouseWheelUpDown = i.ReadValue<float>();
            }
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollingInput(delta);
            HandleAttackInput(delta);
            HandleSprintInput(delta);
            HandleLockOn();
            inputTest(delta);
            HandleReload(delta);
        }
        public void inputTest(float delta)
        {
            #region ### 주석 쓰레기통 ### 

            // phase 테스트

            /*  if (inputActions.Test.Test.phase == UnityEngine.InputSystem.InputActionPhase.Started)
              {
                  Debug.Log("phase Started");
              }
              if (inputActions.Test.Test.phase == UnityEngine.InputSystem.InputActionPhase.Waiting)
              {
                  //Debug.Log("phase Waiting");
              }
              if (inputActions.Test.Test.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
              {
                  Debug.Log("phase Performed");
              }
              if (inputActions.Test.Test.phase == UnityEngine.InputSystem.InputActionPhase.Disabled)
              {
                  Debug.Log("phase Disabled");
              }
              if (inputActions.Test.Test.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
              {
                  Debug.Log("phase Canceled");
              }*/



            // 키 설정 테스트 

            if (LockOnFlag)
            {
                //Debug.Log("FixedCameraFlag" + FixedCameraFlag);
            }

            //Debug.Log(" mouseWheelUpDown : " + input_mouseWheelUpDown);
       

            #endregion

        }
        public void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;

            horizontal2 = movementInput.x;
            vertical2 = movementInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            moveAmount2 = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        

        public void HandleRollingInput(float delta)
        {
            inputActions.PlayerActions.Roll.performed += i => input_SpaceKey = true; // ?
            if (input_SpaceKey)
            {
                rollFlag = true;
                // 키 누르자마자 무적으로 하자 => 그럼 계속 쉬프트 누르면 되는부분이라 안됨 ㅠㅠ 
                //Sc_PlayerManager.ins.DisablePlayerCollider();
                Debug.Log("rollFlag + " + rollFlag);
            }
            
        }

       

        public void HandleSprintInput(float delta)
        {
            // isInteracting으로 입력 자체를 막아버리자.
            input_leftShift = inputActions.PlayerMovement.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            playerManager.isSprinting = input_leftShift;


           /* if (input_leftShift)
            {
                
                Debug.Log("sprintFlag_shift" + input_leftShift);
            }*/
        }

        /// <summary>
        /// 공격 하는 함수 애니메이션 // 이벤트 함수 설정 잊지 말고 해야함.
        /// </summary>
        /// <param name="delta"></param>
        private void HandleAttackInput(float delta)
        {

            inputActions.PlayerActions.RB.performed += i => input_mouseLeft = true;
            inputActions.PlayerActions.RT.performed += i => input_mouseRight = true;
            

            if (input_mouseLeft)
            {
                attackType = AttackType.LightAttack;
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon , attackType);
                    comboFlag = false;
                }
                else
                {
                    if(playerManager.isInteracting)
                        return;

                    if (playerManager.canDoCombo)
                        return;
                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                }
                
            }

            if (input_mouseRight)
            {
                attackType = AttackType.HeavyAttack;
                if (playerManager.canDoCombo)
                {

                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon , attackType);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;

                    if (playerManager.canDoCombo)
                        return;
                    playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
                }
            }
        }

        public void HandleLockOn()
        {

            inputActions.PlayerActions.FixedCamera.performed += i => input_mouseMid = true;
            if(input_mouseMid)
            {
                LockOnFlag = input_mouseMid;
            }

            if(Sc_CameraHandler.singleton.isLockOnMode)
            {
                // 마우스휠로 값 올려주는 작업
                if (input_mouseWheelUpDown < 0)
                {
                    Sc_GameMng.ins.LockOnTargetIndex -= 1;
                    mouseWheelUpDownFlag = true;
                }
                else if (input_mouseWheelUpDown > 0)
                {
                    Sc_GameMng.ins.LockOnTargetIndex += 1;
                    mouseWheelUpDownFlag = true;
                }
            }
        }

        public void HandleReload(float delta)
        {
            inputActions.PlayerActions.Reload.performed += i => input_R_Key = true;
            if(input_R_Key)
            {
                if (animatorHandler.animator.GetBool("isInteracting"))
                {
                    return;
                }
                reloadFlag = input_R_Key;
                playerManager.ResetStamina();
                animatorHandler.PlayTargetAnimation("Reload", playerManager.isInteracting ,false);
            }
        }

        private void LateUpdate()
        {
            ResetInputValue();
        }

      
    }
}

