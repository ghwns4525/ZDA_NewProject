﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
        }
    }
}
