using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New TestItem", menuName ="Item/TestItem")]
public class ItemData_Test : ScriptableObject
{
    public ItemType itemType;

    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab;

    public enum ItemType 
    { 
        Used,
        ETC
    }



}
