using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_WeaponSlotManager : MonoBehaviour
{
    [SerializeField]
    Sc_WeaponHolderSlot leftHandSlot;

    [SerializeField]
    Sc_WeaponHolderSlot rightHandSlot;

    [SerializeField]
    Sc_DamageCollider leftHandDamageCollider;
    [SerializeField]
    Sc_DamageCollider rightHandDamageCollider;

    Animator animatior;


    private void Awake()
    {
        animatior = GetComponent<Animator>();
        Sc_WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<Sc_WeaponHolderSlot>();

        foreach(Sc_WeaponHolderSlot weaponsSlot in weaponHolderSlots)
        {
            if(weaponsSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponsSlot;
            }
            else if (weaponsSlot.isRightHandSlot)
            {
                rightHandSlot = weaponsSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(Sc_WeaponItem weaponItem , bool isLeft)
    {
        if(isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();
            #region Handle Left Weapon Idle Ani
            if (weaponItem != null)
            {
                animatior.CrossFade(weaponItem.left_Hand_Idle ,0.2f);
            }
            else
            {
                animatior.CrossFade("Left Arm Empty" ,0.2f);
            }
            #endregion

        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();
            #region Handle Right Weapon Idle Ani
            if (weaponItem != null)
            {
                animatior.CrossFade(weaponItem.right_Hand_Idle, 0.2f);
            }
            else
            {
                animatior.CrossFade("Right Arm Empty", 0.2f);
            }
            #endregion
        }
    }

    #region Handle Weapon Damage Collider

    void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<Sc_DamageCollider>();
    }

    void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<Sc_DamageCollider>();
    }

    public void OpenRightDamageCollider()
    {
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void OpenLeftDamageCollider()
    {
        leftHandDamageCollider.EnableDamageCollider();
    }

    public void CloseRightDamageCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
    }

    public void CloseLeftDamageCollider()
    {
        leftHandDamageCollider.DisableDamageCollider();
    }

    #endregion
}
