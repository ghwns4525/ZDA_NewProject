using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_RootMotion : MonoBehaviour
{
    Rigidbody rigidbody;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(animator.applyRootMotion);
    }
    private void OnAnimatorMove()
    {

       
    }
    Coroutine co;
    public void AnimatorMoveStarter(bool isProgress)
    {
        co = StartCoroutine(Move(10));
    }
    public void AnimatorMoveStoper()
    {
        rigidbody.drag = 0; 
        rigidbody.velocity = Vector3.zero;
        StopCoroutine(co);

    }
    IEnumerator Move(int i)
    {
        while (true)
        {
           /* float delta = Time.deltaTime;
            rigidbody.drag = 0; //?
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            rigidbody.velocity = velocity;
            Debug.Log("aa");*/
            yield return new WaitForFixedUpdate();
        }
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        rigidbody.drag = 0; //?
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        rigidbody.velocity = velocity;
        Debug.Log("aa");
    }

}
