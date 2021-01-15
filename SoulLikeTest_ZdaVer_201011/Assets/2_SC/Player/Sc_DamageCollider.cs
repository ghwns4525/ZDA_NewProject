using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
  /// 플레이어 검에 달려있는 콜라이더 제어
    public class Sc_DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
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
            if(collider.tag == "Boss")
            {
                Sc_BossStats bossStats = collider.GetComponent<Sc_BossStats>();

                // 보스가 죽으면 데미지를 안받음
                if(!bossStats.IsDie)
                {
                    if (bossStats != null)
                    {
                        bossStats.TakeDamage(currentWeaponDamage);
                    }
                }
                
            }
            if(collider.tag == "Enemy")
            {
                Hj_MonsterStats monsterStats = collider.GetComponent<Hj_MonsterStats>();
                if(!monsterStats.IsDie)
                {
                    if (monsterStats != null)
                    {
                        monsterStats.TakeDamage(currentWeaponDamage);
                    }
                }
                
            }
        }
    }
}

