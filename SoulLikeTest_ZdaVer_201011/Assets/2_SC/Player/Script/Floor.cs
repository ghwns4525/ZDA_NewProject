using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    Door theDoor;


    // Start is called before the first frame update
    void Start()
    {
        theDoor = FindObjectOfType<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collider)
    {
        if(collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("ShotTurret"))
        {
            theDoor.isTurret = false;
        }
        else
        {
            theDoor.isTurret = true;
        }
    }
}
