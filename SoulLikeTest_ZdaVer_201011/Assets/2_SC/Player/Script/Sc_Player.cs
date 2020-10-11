using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class Sc_Player : MonoBehaviour
{
    Sc_PlayerManager playerManager;
    Transform cameraObject;
    Sc_InputHandler inputHandler;
    public Vector3 moveDirection;

    [HideInInspector]
    public Transform myTrasform;
    [HideInInspector]
    // public Sc_AnimatorHandler animatorHandler;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;
    [Header("Ground & Air Detection Status")]
    /*[SerializeField]
    float groundDetectionRayStartPoint = 0.5f;*/
    /*[SerializeField]
    float minDistanceNeededToBeginFall = 1f;*/
    /*[SerializeField]
    float groundDirectionRayDistance = 0.2f;*/

    LayerMask ignorForGroundCheck;
    public float inAirTimer;

    [Header("수정 가능")]
    [SerializeField] public float moveSpeed = 5;    // 플레이어 움직이는 속도
    [SerializeField] float roatationSpeed = 10;     // 플레이어 회전 속도
    //[SerializeField] private float sprintSpeed = 7;
    /*[SerializeField]
    float fallingSpeed = 45;*/

    // 여기부터는 UI 테스트용
    public int t_PlayerHp = 5;
    public float t_Power = 10;
    public float t_Defense = 10;
    public int t_Junk = 0;
    public float t_overHeating = 0;
    public float t_maxOverHeating = 10f;
    public List<int> t_item;
    public bool t_Passive1 = true;
    public bool t_Passive2 = true;
    public bool t_Passive3 = true;
    public bool t_Passive4 = true;


    void Start()
    {
        playerManager = GetComponent<Sc_PlayerManager>();
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<Sc_InputHandler>();
        // animatorHandler = GetComponentInChildren<Sc_AnimatorHandler>();
        cameraObject = Camera.main.transform;
        myTrasform = transform;
        // animatorHandler.Initialized();

        playerManager.isGround = true;
        ignorForGroundCheck = ~(1 << 8 | 1 << 11);
    }

    void Update()
    {
        float delta = Time.deltaTime;
        Debug.Log("moveSpeed : " + moveSpeed);
            
    }
    #region Movement
    Vector3 normalVector;
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

        if (inputHandler.rollFlag)
        {
            return;
        }
        if(playerManager.isInteracting)
        {
            return;
        }
        // 카메라에 따라서 내가 가고 싶은 방향이 정해진다. 
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();

        // 근데 이제 피벗때문에 카메라의 위치가 애매해짐. 
        // 따라서 
        moveDirection.y = 0;

        float speed = moveSpeed;

        if(inputHandler.sprintFlag && inputHandler.moveAmount > 0.5f)   // 달리는 경우
        {
            //speed = sprintSpeed;
            playerManager.isSprinting = true;
            moveDirection *= speed;
        }
        else    // 걷는 경우
        {
            if(inputHandler.moveAmount < 0.5) 
            {
                moveDirection *= moveSpeed;
                playerManager.isSprinting = false;
            }
            else
            {
                moveDirection *= speed;
                playerManager.isSprinting = false;
            }
        }

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;
        //Debug.Log("velocity" + rigidbody.velocity);
        // animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0 , playerManager.isSprinting);
            
        HandleRoatation(delta); // 캐릭터 회전
           
    }

    public void Restart()
    {
        transform.position = new Vector3(0, 1, 0);
    }


    /*
    public void HandleRollingAndSprinting(float delta)  // 구르기, 달리기
    {
        if (animatorHandler.anim.GetBool("isInteracting"))
            return;

        if (inputHandler.rollFlag)  // 돌고있다?
        {
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;

            // 어디든 일단 움직이고 있음 
            if (inputHandler.moveAmount >= 0)
            {
                animatorHandler.PlayTargetAnimation("Rolling", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTrasform.rotation = rollRotation;
            }

            // 만약 멈췄을때 처리를 하고싶다면 이렇게 하면 됨 
            if (inputHandler.moveAmount > 0)
            {
                animatorHandler.PlayTargetAnimation("Rolling", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTrasform.rotation = rollRotation;
            }
            else // 멈추고 있음 
            {
                animatorHandler.PlayTargetAnimation("Backstep", true);
            }


        }
    }*/

    /*public void HandleFalling(float delta, Vector3 moveDirection)   // 떨어지는 중
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
            rigidbody.AddForce(moveDirection * fallingSpeed / 5f);
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
                    animatorHandler.PlayTargetAnimation("Land", true);
                    inAirTimer = 0;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Empty", false);
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
                    animatorHandler.PlayTargetAnimation("Falling", true);
                }

                Vector3 vel = rigidbody.velocity;
                vel.Normalize();
                rigidbody.velocity = vel * (moveSpeed / 2);
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
    }*/
    #endregion



}


