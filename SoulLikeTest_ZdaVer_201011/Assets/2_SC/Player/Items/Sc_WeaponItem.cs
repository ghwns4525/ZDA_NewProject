using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    [CreateAssetMenu(menuName = "Itmes/Weapon Item")]
    public class Sc_WeaponItem : Sc_Item
    {
        public GameObject modelPrefab;
        public bool inUnarmed;

        [Header("Idle Animation")]
        public string right_Hand_Idle;
        public string left_Hand_Idle;

        [Header("One Handed Attack Animation")]
        public string Light_Attack_A;
        public string Light_Attack_B;
        public string Light_Attack_C;

        public string Heavy_Attack_D;
        public string Heavy_Attack_E;
    }


}

