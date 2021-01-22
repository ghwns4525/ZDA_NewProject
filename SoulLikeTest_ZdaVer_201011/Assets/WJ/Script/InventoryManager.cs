using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;

    

    

    [SerializeField]
    private Slot[] slots;


    private void Start()
    {
         
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
      
    }

    public void AcquirItem(ItemData_Test _item, int _count = 1)
    {
        Debug.Log("액콰이어 호출");
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                if(slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
