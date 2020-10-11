using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SG
{
    public class OverHeating : MonoBehaviour
    {
        [SerializeField] Image img_OverHeating;

        float currentOverHeating;
        float maxOverHeating;
        bool isHeating = false;
        float currentTime = 0;

        H_PlayerLocomotion thePlayer;
        H_MinigameInputHandler InputHandler;

        // Start is called before the first frame update
        void Start()
        {
            thePlayer = FindObjectOfType<H_PlayerLocomotion>();
            InputHandler = FindObjectOfType<H_MinigameInputHandler>();

            currentOverHeating = thePlayer.t_overHeating;
            maxOverHeating = thePlayer.t_maxOverHeating;
        }

        // Update is called once per frame
        void Update()
        {
            img_OverHeating.fillAmount = Mathf.Lerp(img_OverHeating.fillAmount, currentOverHeating / maxOverHeating, Time.deltaTime * 5f);
            IncreaseOH();
            if(isHeating)
            {
                DecreaseOH();
            }
        }

        public void IncreaseOH()
        {
            if (currentOverHeating < maxOverHeating)
            {
                //@Test
                /*if (InputHandler.testR_Input)
                {
                    currentOverHeating++;
                    Debug.Log(currentOverHeating);
                    isHeating = true;
                    if(isHeating)
                    {
                        currentTime += Time.deltaTime;
                        if(currentTime > 1.0f)
                        {
                            isHeating = false;
                            currentTime = 0;
                        }
                    }
                }*/
            }
            else
            {
                currentOverHeating = maxOverHeating;
                Debug.Log(currentOverHeating);
            }
        }

        public void DecreaseOH()
        {
            if (currentOverHeating > 0)
            {
                currentOverHeating -= 0.5f * Time.deltaTime;
            }
            //@Test
           /*if(InputHandler.testG_Input)
            {
                currentOverHeating = 0;
                Debug.Log(currentOverHeating);
            }*/
        }
    }
}