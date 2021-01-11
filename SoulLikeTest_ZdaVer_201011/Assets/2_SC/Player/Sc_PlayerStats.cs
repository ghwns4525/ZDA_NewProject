using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Sc_PlayerStats : MonoBehaviour
    {

        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public Sc_HealthBar healthBar;

        Sc_AnimatorHandler animatorHandler;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<Sc_AnimatorHandler>();
        }
        // Start is called before the first frame update
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private int  SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation("Damage_01", true , false);

            if(currentHealth <=0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Dead_01", true , false);
            }
        }

/*        public bool IsPlayerDie()
        {
            if(currentHealth <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/


    }

}
