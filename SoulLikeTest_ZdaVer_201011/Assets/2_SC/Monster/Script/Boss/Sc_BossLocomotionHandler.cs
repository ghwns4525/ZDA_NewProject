using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BossLocomotionHandler : MonoBehaviour
{
    //  [Move] 
    [SerializeField]
    private float moveSpeed = 5.0f;
    Sc_BossAnimationHandler sc_BossAniHandler;
    Rigidbody rigidbody; 


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        sc_BossAniHandler = GetComponentInChildren<Sc_BossAnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region == Root Motion Locomotion Handleer == 

    public void AnimatorRootMotionMoveHandler(float delta)
    {
        sc_BossAniHandler.animator.SetBool("IsRootAnimation", sc_BossAniHandler.animator.applyRootMotion);
        if (sc_BossAniHandler.animator.applyRootMotion)
        {
            rigidbody.drag = 0; //?
            Vector3 deltaPosition = sc_BossAniHandler.animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            rigidbody.velocity = velocity * 2.5f;
        }

    }
    #endregion

    #region == Inplace Motion Locomotion Handleer == 

    Coroutine Co_MoveRigidbodyLerpMotion;
    // 애니메이션 이벤트 함수에서 자주 쓰임 
    public void InplaceMoveMotionCoroutineStarter(float speedInitialValue, float speedIncreaseValue, float duration)
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
        sc_BossAniHandler.animator.SetBool("isRootMotion", false);
        sc_BossAniHandler.animator.SetInteger("Vertical", 0);
        sc_BossAniHandler.animator.SetInteger("Horizontal", 0);

        while (currentDelta <= duration)
        {
            currentDelta += Time.fixedDeltaTime;
            rigidbody.velocity = transform.forward * accelerateAmount;
            accelerateAmount += speedIncreaseValue;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("ss");
        sc_BossAniHandler.animator.SetBool("isRootMotion", true);
        rigidbody.velocity = Vector3.zero;

        StopCoroutine(Co_MoveRigidbodyLerpMotion);
    }

    #endregion
}
