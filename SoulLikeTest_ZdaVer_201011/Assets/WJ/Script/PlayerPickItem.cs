using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickItem : MonoBehaviour
{

    [SerializeField]
    private InventoryManager inventoryManager;

 
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            Debug.Log(collision.transform.GetComponent<ItemPick>().item.itemName);
            Destroy(collision.gameObject);
            if (inventoryManager != null)
            {
                inventoryManager.AcquirItem(collision.transform.GetComponent<ItemPick>().item);
            }
        }
    }
}
