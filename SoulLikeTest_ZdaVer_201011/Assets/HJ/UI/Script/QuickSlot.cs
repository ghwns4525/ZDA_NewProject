using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SG
{
    public class QuickSlot : MonoBehaviour
    {
        [SerializeField] public Sprite[] ItemSprite;
        [SerializeField] public int[] ItemCount;
        [SerializeField] Image quickSlotImage;
        [SerializeField] Text ItemText;
        int slotNum = 0;
        int maxSlotNum;

        H_MinigameInputHandler inputHandler;
        H_PlayerLocomotion thePlayer;

        // Start is called before the first frame update
        void Start()
        {
            inputHandler = FindObjectOfType<H_MinigameInputHandler>();
            thePlayer = FindObjectOfType<H_PlayerLocomotion>();
            maxSlotNum = ItemSprite.Length;
            for(int i = 0; i < maxSlotNum; i++)
            {
                ItemCount[i] = thePlayer.t_item[i];
            }
        }

        // Update is called once per frame
        void Update()
        {
            //@Test
           /* if (inputHandler.testE_Input)
            {
                slotNum++;
                if (slotNum > maxSlotNum - 1)
                {
                    slotNum = 0;
                }
            }
            if (inputHandler.testQ_Input)
            {
                slotNum--;
                if (slotNum < 0)
                {
                    slotNum = maxSlotNum - 1;
                }
            }
            if (inputHandler.testV_Input)
            {
                ItemCount[slotNum]--;
                if (ItemCount[slotNum] <= 0)
                {
                    ItemCount[slotNum] = 0;
                }
            }*/
            quickSlotImage.sprite = ItemSprite[slotNum];
            ItemText.text = string.Format("{0:#,##0}", ItemCount[slotNum]);
            Debug.Log(slotNum);
        }
    }
}
