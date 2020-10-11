using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SG
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] Text txt_Junk;

        int currentJunk;

        H_PlayerLocomotion thePlayer;
        H_MinigameInputHandler InputHandler;

        // Start is called before the first frame update
        void Start()
        {
            thePlayer = FindObjectOfType<H_PlayerLocomotion>();
            InputHandler = FindObjectOfType<H_MinigameInputHandler>();
            currentJunk = thePlayer.t_Junk;
        }

        // Update is called once per frame
        void Update()
        {
            txt_Junk.text = string.Format("{0:#,##0}", currentJunk);
            IncreaseJunk();
            DecreaseJunk();
        }

        public void IncreaseJunk()
        {
            if(InputHandler.testF_Input)    // testJ_Input = F키
            {
                currentJunk += 10;
                Debug.Log(currentJunk);
            }
        }

        public void DecreaseJunk()
        {
            if (currentJunk != 0)
            {
                //@Test
               /* if (InputHandler.testG_Input)    // test_Input = H키
                {
                    currentJunk -= 10;
                    Debug.Log(currentJunk);
                }*/
            }
        }
    }
}