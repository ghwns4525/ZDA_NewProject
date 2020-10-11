using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BossDamageCollider : MonoBehaviour
{
    Collider damageCollider;
    public int currentWeaponDamage = 25;

    private void Awake()
    {
        damageCollider = GetComponentInChildren<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Sc_PlayerStats playerStats = collider.GetComponent<Sc_PlayerStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(currentWeaponDamage);
            }
        }
    }
}
