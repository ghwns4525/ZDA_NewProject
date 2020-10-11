using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 구르고 나서 Interacting때문에 구르고 나서 
//움직임을 담당하는 스크립트.
//movement라고 봐도 됨.
namespace SG
{
    public class Sc_PlayerLocomotionHandler : MonoBehaviour
    {
        public bool isRootAni;
        Sc_PlayerManager playerManager;
        Transform cameraObject;
        Sc_InputHandler inputHandler;
        public Vector3 moveDirection;
        public Vector3 moveDirectionNomallized;

        [HideInInspector]
        public Transform myTrasform;
        [HideInInspector]
        public Sc_AnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;
        [Header("Ground & Air Detection Status")]
        [SerializeField]
        float groundDetectionRayStartPoint = 0.5f;
        [SerializeField]
        float minDistanceNeededToBeginFall = 1f;
        [SerializeField]
        float groundDirectionRayDistance = 0.2f;

        LayerMask ignorForGroundCheck;
        public float inAirTimer;

        [Header("Stats")]
        [SerializeField]
        float moveSpeed = 5;
        [SerializeField]
        private float sprintSpeed = 7;
        [SerializeField]
        float roatationSpeed = 10;
        [SerializeField]
        float fallingSpeed = 45;

        [Header("LockOn Mode")]
        public GameObject[] LockOnTargetObjs;
        public GameObject LockOnTargetObj;
        [SerializeField]
        private float LockOnCameraRoatationSpeed = 10;

        int lockOnTargetIndex = 0;
        public int LockOnTargetIndex
        {
          
            get
            {
                return lockOnTargetIndex;
            }
            set
            {
                // 8개 있으면 0~7

                Debug.Log("TargetsIndex : " + lockOnTargetIndex);
                // 7개 보다 크면 0으로

                // LockOnTargetObjs 예외 처리

                if (LockOnTargetObjs != null)
                {
                    if (LockOnTargetObjs.Length - 1 < lockOnTargetIndex)
                    {
                        lockOnTargetIndex = 0;
                    }
                    // 0보다 작으면 ( 음수가 되면 ) 최대값으로 
                    else if (lockOnTargetIndex < 0)
                    {
                        lockOnTargetIndex = LockOnTargetObjs.Length - 1;
                    }
                }

                
            }
        }

        void Start()
        {
            playerManager = GetComponent<Sc_PlayerManager>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<Sc_InputHandler>();
            animatorHandler = GetComponentInChildren<Sc_AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTrasform = transform;
            animatorHandler.Initialized();

            playerManager.isGround = true;
            ignorForGroundCheck = ~(1 << 8 | 1 << 11);
        }

        void Update()
        {
            float delta = Time.deltaTime;
            isRootAni = animatorHandler.animator.applyRootMotion;


        }
        #region # Movement

        public Vector3 normalVector;
        Vector3 targetPossition;

        private void HandleRoatation (float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0; 

            if(targetDir == Vector3.zero)
            {
                targetDir = myTrasform.forward;

            }
            float rs = roatationSpeed; // 회전속도 
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTrasform.rotation, tr, rs * delta);
            myTrasform.rotation = targetRotation;

        }

        public void HandleMovement(float delta)
        {

            // 행동중 예외 처리
            if (inputHandler.rollFlag)
            {
                return;
            }
           
            if (playerManager.isInteracting)
            {
                /// 예외처리 이유
                /// 공격이나 롤링같은 행동중에 애니메이션 파라미터 값은 계속 올라가서
                /// 해당 코드로 강제로 파라미터를 0으로 만든다.
                animatorHandler.UpdateAnimatorValues(0, 0 , false);
                return;
            }
           
            //animatorHandler.animator.applyRootMotion = false;

            // 카메라에 따라서 내가 가고 싶은 방향이 정해진다. 
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            
            // 근데 이제 피벗때문에 카메라의 위치가 애매해짐. 
            // 따라서 Y축은 0으로! @@@=> 아마 
            moveDirection.y = 0;
            float speed = moveSpeed;
            if(playerManager.isSprinting && inputHandler.moveAmount > 0.5f)
            {
                //Debug.Log("aa");
                speed = sprintSpeed;
                //playerManager.isSprinting = true;
                moveDirection *= speed;
                playerManager.UpdateOpenStaminaFlag(playerManager.staminaValue_Sprint);
            }
            else
            {
                playerManager.UpdateCloseStaminaFlag();
                if (inputHandler.moveAmount < 0.5)
                {
                    moveDirection *= moveSpeed;
                }
                else
                {
                    moveDirection *= speed;
                    //@@@
                    
                }
            }
            //Debug.DrawRay(transform.position, moveDirection * 4f, Color.green , 3f);
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector); // @@@? 
            
            //Debug.DrawRay(transform.position, projectedVelocity * 2f, Color.blue ,3f);

            rigidbody.velocity = projectedVelocity;
            //Debug.Log("velocity" + rigidbody.velocity);
            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0 , playerManager.isSprinting);

            if (animatorHandler.canRatate)
            {
                HandleRoatation(delta);
            }
        }
        public void VelocityReset()
        {
            rigidbody.velocity = Vector3.zero;
            animatorHandler.AnimatiorMoveValuesReset();
        }
        public void HandleRollingAndSprinting(float delta)
        {
            // 공격중에 공격 애니메이션 후반  
            // canDoCombo가 켜져있다면 아무튼 
            if (Sc_PlayerManager.ins.canDoRoll == false)
            {
                //Debug.Log("HandleRollingAndSprinting 실행함");
                return;
            }

            if (inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                /*   
                 *   // 어디든 일단 움직이고 있음 
                if (inputHandler.moveAmount >= 0)
                {
                    animatorHandler.PlayTargetAnimation("Rolling", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTrasform.rotation = rollRotation;
                }
                */

                // 만약 멈췄을때 처리를 하고싶다면 이렇게 하면 됨 

                // 움직이는 중 
                if (inputHandler.moveAmount > 0)
                {
                    animatorHandler.PlayTargetAnimation("Rolling", true , true);
                    playerManager.SetStamina(playerManager.staminaValue_Roll); // 롤링 자체가 한번만 실행하는거라 2번 호출되는듯
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTrasform.rotation = rollRotation;

                    // 이러면 뒤에 DisableCome 애니메이션 이벤트 함수가 씹히기 때문에
                    // 이쪽에서 
                    Sc_PlayerManager.ins.anim.SetBool("canDoCombo", false);
                    Sc_PlayerManager.ins.isAttack = false;
                    // 처리해준다. 

                }
                /*  Debug.Log("호출 얼마나 됨?");
                  inputHandler.rollFlag = false;*/
            }
        }

        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            playerManager.isGround = false;
            RaycastHit hit;
            Vector3 origin = myTrasform.position;
            origin.y += groundDetectionRayStartPoint;

            if(Physics.Raycast(origin , myTrasform.forward , out hit , 0.4f))
            {
                moveDirection = Vector3.zero;
            }

            if(playerManager.isInAir)
            {
                rigidbody.AddForce(-Vector3.up * fallingSpeed);
                rigidbody.AddForce(moveDirection * fallingSpeed / 10f);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDirectionRayDistance;

            targetPossition = myTrasform.position;

            Debug.DrawRay(origin, -Vector3.up * minDistanceNeededToBeginFall, Color.red, 0.1f, false);
            if(Physics.Raycast(origin , -Vector3.up , out hit , minDistanceNeededToBeginFall , ignorForGroundCheck))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGround = true;
                targetPossition.y = tp.y;

                if(playerManager.isInAir)
                {
                    if(inAirTimer > 0.5f)
                    {
                        Debug.Log("지금 공중이야 : " + inAirTimer);
                        animatorHandler.PlayTargetAnimation("Land", true , false);
                        inAirTimer = 0;
                    }
                    else
                    {
                        animatorHandler.PlayTargetAnimation("Empty", false , false);
                        inAirTimer = 0; 
                    }

                    playerManager.isInAir = false;
                }
            }
            else
            {
                if(playerManager.isGround)
                {
                    playerManager.isGround = false;
                }

                if(playerManager.isInAir == false)
                {
                    if(playerManager.isInteracting == false)
                    {
                        animatorHandler.PlayTargetAnimation("Falling", true ,false);
                    }

                    // 떨어지기 전에 튕겨나오는 크기
                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (moveSpeed / 4);
                    playerManager.isInAir = true;

                }
            }

            if (playerManager.isGround)
            {
                if(playerManager.isInteracting || inputHandler.moveAmount > 0 )
                {
                    myTrasform.position = Vector3.Lerp(myTrasform.position, targetPossition, Time.deltaTime / 0.1f);
                }
                else
                {
                    myTrasform.position = targetPossition;
                }
            }
        }

        public void HandleSprinting(float delta, float speed)
        {
            if(inputHandler.sprintFlag_shift)
            {

            }
        }
        #endregion

        #region == 카메라 고정 로직 == 
        /// # HandleTargeting() :: Update 
        /// 버튼을 누르면 고정모드 ture
        /// 고정모드 true 일때 
        bool isfixedCameraStayFlag=false;
        public void LockOnTrigger(float delta)
        {

            if(inputHandler.LockOnFlag)
            {
                // isLockOnMode의 컨트롤 
                if (Sc_CameraHandler.singleton.isLockOnMode == false)
                {
                    // 고정모드 True가 됨
                    Sc_CameraHandler.singleton.isLockOnMode = true;
                    VelocityReset();
                }
                
                else if (Sc_CameraHandler.singleton.isLockOnMode == true)
                {
                    // isFixedCameraTargetMode 취소
                    Sc_CameraHandler.singleton.isLockOnMode = false;
                    VelocityReset();
                }
            }
           
        }
        public void LockOnModeHandler(float delta)
        {
    
            // Flag : 미드 키를 누르면 True 
            if(inputHandler.LockOnFlag)
            {
                // 타겟 모드중이면
                if (Sc_CameraHandler.singleton.isLockOnMode == true)
                {
                    // 보스 Null 이면 보스 없다는것
                    if (GameObject.FindGameObjectWithTag("Boss") == null)
                    {
                        // 일반 몬스터중 제일 가까운 녀석들을 리스트에 담아서 소트함.
                        EnemysTargeting();
                    }

                    else
                    {
                        // 보스 락온 타겟오브젝트로 한다.
                        LockOnTargetObj = GameObject.FindGameObjectWithTag("Boss");
                    }

                    Debug.Log(LockOnTargetObj.name);
                }
                else if (Sc_CameraHandler.singleton.isLockOnMode == false)
                {
                    LockOnTargetIndex = 0;
                    LockOnTargetObj = null;
                    LockOnTargetObjs = null;
                }
            }

            // Flag : 마우스 휠을 업다운 했을때 True가 됨.
            if(inputHandler.mouseWheelUpDownFlag)
            {
                // 예외처리 : 보스를 타겟 했을때는  LockOnTargetObjs는 NUll 이다 이것을 예외처리 해줌.
                if (LockOnTargetObjs != null)
                {
                    // 마우스 휠을 돌리면 타겟이 달라짐 
                    LockOnTargetObj = LockOnTargetObjs[LockOnTargetIndex];
                    Debug.Log("Enemy 수 : " + LockOnTargetObjs.Length + " 현재 선택한 인덱스 값 : " + LockOnTargetIndex);
                }
            }
            
        }

        public void HandleLockOnMovement(float delta)
        {
            // 행동중 예외 처리
            if (inputHandler.rollFlag)
            {
                return;
            }

            if (playerManager.isInteracting)
            {
                /// 예외처리 이유
                /// 공격이나 롤링같은 행동중에 애니메이션 파라미터 값은 계속 올라가서
                /// 해당 코드로 강제로 파라미터를 0으로 만든다.
                animatorHandler.UpdateAnimatorValues(0, 0, false);
                return;
            }

            //animatorHandler.animator.applyRootMotion = false;

            // 캐릭터를 기준으로 이동한다.
            moveDirection = transform.forward * inputHandler.vertical;
            moveDirection += transform.right * inputHandler.horizontal;
            moveDirection.Normalize();

            // 근데 이제 피벗때문에 카메라의 위치가 애매해짐. 
            // 따라서 Y축은 0으로! @@@=> 아마 
            moveDirection.y = 0;
            float speed = moveSpeed;
            if (playerManager.isSprinting && inputHandler.moveAmount > 0.5f)
            {
                //Debug.Log("aa");
                speed = sprintSpeed;
                //playerManager.isSprinting = true;
                moveDirection *= speed;
                playerManager.UpdateOpenStaminaFlag(playerManager.staminaValue_Sprint);
            }
            else
            {
                playerManager.UpdateCloseStaminaFlag();
                if (inputHandler.moveAmount < 0.5)
                {
                    moveDirection *= moveSpeed;
                }
                else
                {
                    moveDirection *= speed;
                    //@@@

                }
            }
            //Debug.DrawRay(transform.position, moveDirection * 4f, Color.green , 3f);
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector); // @@@? 

            //Debug.DrawRay(transform.position, projectedVelocity * 2f, Color.blue ,3f);

            rigidbody.velocity = projectedVelocity;
            //Debug.Log("velocity" + rigidbody.velocity);
            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

            if (animatorHandler.canRatate)
            {
                Vector3 targetDir = LockOnTargetObj.transform.position - transform.position;
                targetDir.Normalize();
                float rs = roatationSpeed; // 회전속도 
                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(myTrasform.rotation, tr, rs * delta);
                myTrasform.rotation = targetRotation;
            }
        }
       

        void EnemysTargeting()
        {
            //타겟팅될 오브젝트를 모두 TargetObj에 집어넣는다.
            LockOnTargetObjs = GameObject.FindGameObjectsWithTag("Enemy");        //타겟이 가능한 오브젝트 집어넣기        
                                                                                       //거리순으로 정렬한다.        
            Vector3 playerPos = transform.position;

            Debug.Log("현재 캐릭터의 좌표는 " + transform.position);
            Array.Sort<GameObject>(LockOnTargetObjs, (x, y) => (playerPos - x.transform.position).sqrMagnitude.CompareTo((playerPos - y.transform.position).sqrMagnitude));
            //TargetObjs.Sort((x,y) => (ch_pos - x.transform.position).sqrMagnitude.CompareTo((ch_pos -y.transform.position).sqrMagnitude));  //소트가 제대로 되지 않음 (거리기준으로 되야 됨)
            for (int i = 0; i < LockOnTargetObjs.Length; i++)
            {
                Debug.Log(i + "번째 오브젝트(" + LockOnTargetObjs[i].name + ")의 거리 = " + (playerPos - LockOnTargetObjs[i].transform.position).sqrMagnitude);
            }
            //가장 가까운 오브젝트를 반환한다.

            LockOnTargetObj = LockOnTargetObjs[0];
            Debug.Log("타게팅 된 오브제 :" + LockOnTargetObj.name);
        }

        #endregion

        #region == Root Motion Locomotion == 

        public void AnimatorRootMotionMoveHandler(float delta)
        {
            animatorHandler.animator.SetBool("isRootMotion", animatorHandler.animator.applyRootMotion);
            if(animatorHandler.animator.applyRootMotion)
            {
                rigidbody.drag = 0; //?
                Vector3 deltaPosition = animatorHandler.animator.deltaPosition;
                deltaPosition.y = 0;
                Vector3 velocity = deltaPosition / delta;
                rigidbody.velocity = velocity * 2.5f;
            }
            
        }
        #endregion

        #region == Inplace Motion Locomotion == 

        Coroutine Co_MoveRigidbodyLerpMotion;
        /// <summary>
        /// Inplace 애니메이션을 이동시키는 함수이다.
        /// </summary>
        /// <param name="speedInitialValue">속도 초기 값</param>
        /// <param name="speedIncreaseValue">속도 증가율 만약 증가율이 없다면 1로</param>
        /// <param name="duration">이동 지속 시간</param>
        public void InplaceMoveMotionStarter(float speedInitialValue, float speedIncreaseValue, float duration)
        {
            Co_MoveRigidbodyLerpMotion = StartCoroutine(InplaceMoveMotion(speedInitialValue, speedIncreaseValue, duration));
        }
        Vector3 vector = Vector3.zero;
        /// <summary>
        /// Inplace 애니메이션을 이동시키는 함수이다.
        /// </summary>
        /// <param name="speedInitialValue">속도 초기 값</param>
        /// <param name="speedIncreaseValue">속도 증가율 만약 증가율이 없다면 1로</param>
        /// <param name="duration">이동 지속 시간</param>
        /// <returns></returns>
        IEnumerator InplaceMoveMotion(float speedInitialValue, float speedIncreaseValue, float duration)
        {
            //초기값
            rigidbody.velocity = Vector3.zero;
            float currentDelta = 0;
            float accelerateAmount = speedInitialValue;
            animatorHandler.animator.SetBool("isRootMotion", false);
            animatorHandler.animator.SetInteger("Vertical", 0);
            animatorHandler.animator.SetInteger("Horizontal", 0);

            Vector3 moveMotionDir = Vector3.zero;

            // 락온 모드인지 아닌지 체크 
            if (Sc_CameraHandler.singleton.isLockOnMode)
            {
                // 락온이면  
                //타겟의 방향으로
                
                moveMotionDir = LockOnTargetObj.transform.position - transform.position ;
                Debug.Log("moveMotionDir : "+ moveMotionDir +  "1 :" + LockOnTargetObj.transform.position + " 2 : "+transform.position);
                moveMotionDir.Normalize();
            }
            else
            {
                // 락온이 아니라면 
                // 플레이어 오브젝트의 앞으로
                moveMotionDir = transform.forward;
            }

            while (currentDelta <= duration)
            {
                currentDelta += Time.fixedDeltaTime;
                Debug.Log("moveDirection : " + moveMotionDir);
                rigidbody.velocity = moveMotionDir * accelerateAmount;
                accelerateAmount += speedIncreaseValue;
                yield return new WaitForFixedUpdate();
            }
            Debug.Log("ss");
            animatorHandler.animator.SetBool("isRootMotion", true);
            rigidbody.velocity = Vector3.zero;
            StopCoroutine(Co_MoveRigidbodyLerpMotion);

        }
        public void InplaceMoveMotionStoper()
        {
            if(Co_MoveRigidbodyLerpMotion != null)
            {
                StopCoroutine(Co_MoveRigidbodyLerpMotion);
            }
        }
        #endregion

        public Vector3 Get_Z_AxisPos(Transform target, float Length)
        {
            Vector3 vec = target.forward * Length;
            return vec;
        }
        public Vector3 GetTargetBteenPos(Transform start, Vector3 target)
        {
            Vector3 vec = target - start.position;
            return vec;
        }
        public Vector3 GetTargetBteenPosNomalize(Transform start, Transform target)
        {
            Vector3 vec = target.position - start.position;
            vec.Normalize();
            return vec;
        }
        public Vector3 Get_X_AxisPos(Transform target, float Length)
        {
            Vector3 vec = target.right * Length;
            return vec;
        }
        public Vector3 Get_Y_AxisPos(Transform target, float Length)
        {
            Vector3 vec = target.up * Length;
            return vec;
        }
        public Vector3 Get_Z_ProjectOnPlaneAxisPos(Transform target, float Length)
        {
            Vector3 vec = target.forward * Length + target.position;
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(vec, normalVector);

            return projectedVelocity;
        }
    }
}

