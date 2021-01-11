using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sc_BossAnimationHandler : MonoBehaviour
{
    #region == 컴포넌트 ==

    Sc_BossAttackHandler sc_EnemyAttack;
    public Rigidbody rigidbody;
    public Animator animator;
    Sc_BossHandler sc_BossHandler;

    #endregion


    [Header("== Ani Params ==")]
    int aniParam_Vertical;
    int aniParam_Horizontal;

    public bool canRatate;
    public bool isInteracting;

    public readonly int hash_attackIndex = Animator.StringToHash("Index_attack");
    public readonly int hash_isInteracting = Animator.StringToHash("isInteracting");



    [Header("== Status ==")]
    /// <summary>
    /// 현재 상태를 담는 변수
    /// </summary>
    public string CurrentAni = "";

    /// <summary>
    /// 현재 상태가 바뀌는 변수
    /// </summary>
    public string CurrentAniTemp = "";

    [SerializeField] List<Sc_ActionData> ActtionDataList;
    

    [SerializeField]
    private Collider[] damageColliders;

    [SerializeField]
    private List<Collider> damageColliders_RightHand;

    [SerializeField]
    private List<Collider> damageColliders_LeftHand;

    [SerializeField]
    private List<Collider> damageColliders_LeftLeg;

    [SerializeField]
    private List<Collider> damageColliders_RightLeg;

    [SerializeField]
    private List<Collider> damageColliders_LeftGround;

    [SerializeField]
    private List<Collider> damageColliders_RightGround;

    void Start()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        isInteracting = animator.GetBool(hash_isInteracting);
    }
    public void Initialized()
    {
        animator = GetComponent<Animator>();
        sc_EnemyAttack = GetComponentInParent<Sc_BossAttackHandler>();

        aniParam_Vertical = Animator.StringToHash("Vertical");
        aniParam_Horizontal = Animator.StringToHash("Horizontal");

        

        damageColliders = GetComponentsInChildren<Collider>();
        sc_BossHandler = GetComponent<Sc_BossHandler>();


        rigidbody = GetComponentInParent<Rigidbody>();

        /// ==  데미지 콜라이더 왼손 오른손 정리 ==
        foreach (var item in damageColliders)
        {
            if(item.name.Contains("RightHand"))
            {
                damageColliders_RightHand.Add(item);
            }
            else if(item.name.Contains("LeftHand"))
            {
                damageColliders_LeftHand.Add(item);
            }
            else if(item.name.Contains("LeftLeg"))
            {
                damageColliders_LeftLeg.Add(item);
            }
            else if(item.name.Contains("RightLeg"))
            {
                damageColliders_RightLeg.Add(item);
            }
            else if(item.name.Contains("LeftGround"))
            {
                damageColliders_LeftGround.Add(item);
            }
            else if(item.name.Contains("RightGround"))
            {
                damageColliders_RightGround.Add(item);
            }
        }


    }

    /// <summary>
    /// 일반 모션( 공격을 제외한 행동들 )을 할 때 쓰는 애니메이션 호출 함수
    /// </summary>
    /// <param name="targetAnim"></param>
    /// <param name="isInteracting"></param>
    /// <param name="isRootAnimation"></param>
    public void PlayTargetActionAnimation(string targetAnim, bool isInteracting , bool isRootAnimation)
    {
        animator.SetBool("IsRootAnimation" , isRootAnimation);
        animator.applyRootMotion = isRootAnimation; // 이걸로 루트모션을 쓸껀지 정한다. 
        animator.SetBool("isInteracting", isInteracting);
        
        animator.CrossFade(targetAnim, 0.12f);
        Debug.Log("############ isInteracting : " + animator.GetBool("isInteracting"));
    }
    /// <summary>
    /// 공격을 할 때 쓰는 애니메이션 호출 함수
    /// </summary>
    /// <param name="isInteracting"></param>
    /// <param name="isRootAnimation"></param>
    public void PlayTargetAttackAnimation(string aniName, bool isInteracting , bool isRootAnimation)
    {
        animator.SetBool("IsRootAnimation", isRootAnimation);
        animator.applyRootMotion = isRootAnimation; // 이걸로 루트모션을 쓸껀지 정한다.
        animator.SetBool("isInteracting", isInteracting);
       
        animator.CrossFade(aniName, 0.12f);
        Debug.Log("PlayTargetAttackAnimation : " + animator.GetBool("isInteracting"));
    }


    #region == 애니메이션 이벤트 함수 ==
    public void EnableDamageCollider()
    {
        foreach (var item in damageColliders)
        {
            item.enabled = true;
        }
        
    }
    public void DisableDamageCollider()
    {
        foreach (var item in damageColliders)
        {
            item.enabled = false;
        }
    }

    public void EnableRightDamageCollider()
    {
        foreach (var item in damageColliders_RightHand)
        {
            item.enabled = true;
        }

    }
    public void DisableRightDamageCollider()
    {
        foreach (var item in damageColliders_RightHand)
        {
            item.enabled = false;
        }
    }

    public void EnableLeftDamageCollider()
    {
        foreach (var item in damageColliders_LeftHand)
        {
            item.enabled = true;
        }

    }
    public void DisableLeftDamageCollider()
    {
        foreach (var item in damageColliders_LeftHand)
        {
            item.enabled = false;
        }
    }

    public void EnableLeftLegDamageCollider()
    {
        foreach (var item in damageColliders_LeftLeg)
        {
            item.enabled = true;
        }

    }
    public void DisableLeftLegDamageCollider()
    {
        foreach (var item in damageColliders_LeftLeg)
        {
            item.enabled = false;
        }
    }

    public void EnableRightLegDamageCollider()
    {
        foreach (var item in damageColliders_RightLeg)
        {
            item.enabled = true;
        }

    }
    public void DisableRightLegDamageCollider()
    {
        foreach (var item in damageColliders_RightLeg)
        {
            item.enabled = false;
        }
    }

    public void EnableLeftGroundDamageCollider()
    {
        foreach (var item in damageColliders_LeftGround)
        {
            item.enabled = true;
        }

    }
    public void DisableLeftGroundDamageCollider()
    {
        foreach (var item in damageColliders_LeftGround)
        {
            item.enabled = false;
        }
    }

    public void EnableRightGroundDamageCollider()
    {
        foreach (var item in damageColliders_RightGround)
        {
            item.enabled = true;
        }

    }
    public void DisableRightGroundDamageCollider()
    {
        foreach (var item in damageColliders_RightGround)
        {
            item.enabled = false;
        }
    }

    #endregion





    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
    {

        #region Vertical

        float v = 0;
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            v = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f)
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
        else if (horizontalMovement > 0.55f)
        {
            h = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }
        #endregion

        if (isSprinting)
        {
            v = 2;
            h = horizontalMovement;
        }
        //animator.rootPosition = transform.position;
        animator.SetFloat(aniParam_Vertical, v, 0.1f, Time.deltaTime);
        animator.SetFloat(aniParam_Horizontal, h, 0.1f, Time.deltaTime); // dampTIme 은 또ㅓ 뭐임? 

    }

    /// <summary>
    /// 리스트에 담겨 있는 매개변수와 이름이 똑같은 애니메이션을 찾는 함수
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string GetAniName(EAniName_Action action)
    {
        string returnStr = null;
        foreach (Sc_ActionData item in ActtionDataList)
        {
            returnStr = item.AniName.ToString();            
        }
        return returnStr;
    }
}


