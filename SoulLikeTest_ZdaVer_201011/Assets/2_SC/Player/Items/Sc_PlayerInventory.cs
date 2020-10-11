using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerInventory : MonoBehaviour
{

    Sc_WeaponSlotManager weaponSlotManager;

    public Sc_WeaponItem rightWeapon;
    public Sc_WeaponItem leftWeapon;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<Sc_WeaponSlotManager>();

    }

    private void Start()
    {
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        //weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
    }
}
