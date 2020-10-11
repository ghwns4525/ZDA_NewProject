using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class RigiMovementTest : MonoBehaviour
{
    Rigidbody rigidbody;
    
    Vector3 velocity;
    float accelerateAmount;

    Coroutine Co_AccelerationMotion;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
        //rigidbody.AddForce(transform.forward * 1000);
        //StartCoroutine()
        velocity = transform.forward;
        accelerateAmount = 100;
        rigidbody.velocity = Vector3.zero;
        Co_AccelerationMotion = StartCoroutine(MoveRigidbodyLerpMotion(10f , Get_Y_AxisPos(transform,10)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 vector = Vector3.zero;
    IEnumerator MoveRigidbodyLerpMotion(float speed , Vector3 moveTarget)
    {
        float EndDistance = Vector3.Distance(rigidbody.position, moveTarget);
        float currentDelta = 0;
       while (currentDelta <= 1)
       {
            rigidbody.position = Vector3.Lerp(rigidbody.position, moveTarget, Time.fixedDeltaTime * speed);
            EndDistance = Vector3.Distance(rigidbody.position, moveTarget);
            currentDelta += Time.fixedDeltaTime;
            //Debug.Log("currentDelta" + EndDistance);
            //rigidbody.velocity = transform.forward * accelerateAmount;
            yield return new WaitForFixedUpdate();
       }
        Debug.Log("ss");
        rigidbody.velocity = Vector3.zero;
        StopCoroutine(Co_AccelerationMotion);
    }


    public Vector3 Get_Z_AxisPos( Transform target , float Length)
    {
        Vector3 vec = target.forward * Length + target.position;
        return vec;
    }
    public Vector3 Get_X_AxisPos(Transform target, float Length)
    {
        Vector3 vec = target.right * Length + target.position;
        return vec;
    }
    public Vector3 Get_Y_AxisPos(Transform target, float Length)
    {
        Vector3 vec = target.up * Length + target.position;
        return vec;
    }

}
