using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Bullet") || collider.gameObject.CompareTag("TurretBullet") || collider.gameObject.CompareTag("DontDestroyBullet"))
        {
            Destroy(collider.gameObject);
        }
    }
}
