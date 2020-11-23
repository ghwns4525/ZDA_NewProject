using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ActionState
{
    InplaceStartMove,
    InplaceStopMove,
    RootMotionStartMove,
    RootMotionStopMove,
    None
}
public class Sc_PlayerManager : MonoBehaviour
{

    // 싱글턴
    public static Sc_PlayerManager ins;

    public Collider playerCollider;

    Sc_InputHandler inputHandler;
    [HideInInspector]
    public Animator anim;


    /// <summary>
    /// ResetAnimatorBool에서 애니메이션이 끝날때마다 자동으로 false 처리 해준다. 
    /// </summary>
    [Header("Player_Flag")]
    public bool isInteracting;
    public bool InteractingCutOffFlow;
    public bool isSprinting;
    public bool isInAir;
    public bool isGround;
    public bool canDoCombo;
    public bool canDoRoll;
    public bool isAttack;
    public bool isAttackMove; // 공격하면 일정 시간만큼 움직여야 하는데 그것에 대한 값
    public bool attackFlag;

    [Header("Stamina Status")]
        

    [SerializeField]
    float stamina; // 나중에 SerializeField 지워야 함 
    public float Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
            if(staminaMax < stamina)
            {
                stamina = staminaMax;
                Debug.Log("스태미나 맥스치");
            }
            else if(stamina < 0)
            {
                stamina = 0;
                //Debug.Log("스태미나 최소값");
                StopCoroutine("HandleResetStamina");
            }
        }
    }
    [SerializeField]
    float staminaMax;
    [SerializeField]
    [Range(0.001f,0.7f)]
    float staminaValue_CoolDownSpeed;
    [SerializeField]
    [Range(1f, 5f)]
    float staminaValue_ReloadCoolDownSpeed;
    [SerializeField]
    float staminaAdder;
    [SerializeField]
    bool staminaFlag;

    /// 달리기 초당 10 
    /// 대쉬 20
    /// 구르기 10 
    /// 일반공격 20
    /// 강공격 35
    /// 차지공격 35
    [Header("StaminaValue Status")]
    public float staminaValue_Sprint;
    public int staminaValue_Dash;
    public int staminaValue_Roll;
    public int staminaValue_LightAttack;
    public int staminaValue_HeavyAttack;
    public int staminaValue_ChargingAttack;


    Sc_CameraHandler cameraHandler;
    Sc_PlayerLocomotionHandler sc_PlayerLocomotion;
    Sc_AnimatorHandler sc_AnimatorHandler;
    private void Awake()
    {
        if(ins == null)
        {
            ins = this;
        }
            
        cameraHandler = FindObjectOfType<Sc_CameraHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<Sc_InputHandler>();
        anim = GetComponentInChildren<Animator>();
        sc_AnimatorHandler = GetComponentInChildren<Sc_AnimatorHandler>();
        sc_PlayerLocomotion = GetComponent<Sc_PlayerLocomotionHandler>();
        playerCollider = GetComponent<Collider>();


        StartCoroutine(HandleStamina());
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        canDoRoll = anim.GetBool("canDoRoll");

        inputHandler.TickInput(delta);
        sc_PlayerLocomotion.HandleRollingAndSprinting(delta);
        sc_PlayerLocomotion.HandleFalling(delta, sc_PlayerLocomotion.moveDirection);
            
        sc_PlayerLocomotion.LockOnTrigger(delta);
            
        if(cameraHandler.isLockOnMode == false)
        {
            // 일반 모드


            sc_PlayerLocomotion.HandleMovement(delta);

        }
        else if (cameraHandler.isLockOnMode == true)
        {
            // 타겟 모드
            sc_PlayerLocomotion.HandleLockOnMovement(delta);
            sc_PlayerLocomotion.LockOnModeHandler(delta);
        }
            

        //HandleOpenIsInteracting();
    }
        
    void testTemp()
    {
           
    }

    private void LateUpdate()
    {
        if (isInAir)
        {
            sc_PlayerLocomotion.inAirTimer = sc_PlayerLocomotion.inAirTimer + Time.deltaTime;
        }
            
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        sc_PlayerLocomotion.AnimatorRootMotionMoveHandler(delta);
        if (cameraHandler != null)
        {
            if(cameraHandler.isLockOnMode == false)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
            else
            {
                cameraHandler.HandleLockOnCameraRotation(delta);
            }
                
        }
            
        //sc_Player.Set_AniAddForceMovement(delta, 70f);
    }

    #region ### 스태미나 ### 
      
    IEnumerator HandleStamina()
    {
        while(true)
        {
            if(staminaFlag)
            {
                Stamina += staminaAdder;
                //Debug.Log("staminaAdderUpdate : " + staminaAdder);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                Stamina -= staminaValue_CoolDownSpeed;
                yield return new WaitForSeconds(0.1f);
                staminaAdder = 0;
            }
                
                
        }
    }

    /// <summary>
    /// 스태미너 추가 함수
    /// </summary>
    /// <param name="value"></param>
    public void SetStamina(int value)
    {
        staminaAdder = (float)value;
        Stamina += staminaAdder;
        //Debug.Log("staminaAdder0 : "+ staminaAdder);
    }
    public void UpdateOpenStaminaFlag(float value)
    {
        staminaFlag = true;
        staminaAdder = value;
        //Debug.Log("staminaAdderUpdate : " + staminaAdder);
    }

    public void UpdateCloseStaminaFlag()
    {
        staminaFlag = false;
        staminaAdder = 0;
    }
        
    public void ResetStamina()
    {
        StartCoroutine("HandleResetStamina");
    }

    IEnumerator HandleResetStamina()
    {
        while (0 <= Stamina)
        {
            Stamina -= staminaValue_ReloadCoolDownSpeed;
            yield return 0;
        }
    }
    #endregion

       
    public void DisablePlayerCollider()
    {
        Debug.Log("###########################DisablePlayerCollider()");
        Sc_PlayerManager.ins.playerCollider.enabled = false;
    }

       
}


