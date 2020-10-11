using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public Vector3 normalVector;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        target.transform.position = Get_Z_ProjectOnPlaneAxisPos(transform, 10f);
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 2f))
        {
            normalVector = hit.normal;
            Vector3 tp = hit.point;
        }
    }
    public Vector3 Get_Z_ProjectOnPlaneAxisPos(Transform target, float Length)
    {
        Vector3 vec = target.forward * Length + target.position;
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(vec, normalVector);

        return projectedVelocity;
    }
}
