using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Sc_DamagePlayer : MonoBehaviour
    {
        private int damage = 25;
        private void OnTriggerEnter(Collider other)
        {
            Sc_PlayerStats playerStats =  other.GetComponent<Sc_PlayerStats>();

            if(playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
        }
    }

}
