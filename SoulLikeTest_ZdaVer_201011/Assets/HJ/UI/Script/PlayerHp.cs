using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{


    public class PlayerHp : MonoBehaviour
    {
        [SerializeField] Image img_Hp;
        [SerializeField] Image img_LateHp;
        [SerializeField] GameObject[] img_MinigameHp;

        int currentHp;
        int currentMhp = 3;

        H_PlayerLocomotion thePlayer;
        H_MinigameInputHandler InputHandler;

        // Start is called before the first frame update
        void Start()
        {
            thePlayer = FindObjectOfType<H_PlayerLocomotion>();
            InputHandler = FindObjectOfType<H_MinigameInputHandler>();
            currentHp = thePlayer.t_PlayerHp;
        }

        // Update is called once per frame
        void Update()
        {
            img_Hp.fillAmount = (float)currentHp / thePlayer.t_PlayerHp;
            img_LateHp.fillAmount = Mathf.Lerp(img_LateHp.fillAmount, (float)currentHp / thePlayer.t_PlayerHp, Time.deltaTime * 5f);
            DecreaseHp();
        }

        public void DecreaseHp()
        {
            if(InputHandler.testG_Input)
            {
                currentHp--;
            }
        }

        public void DecreaseMinigameHp()
        {
            if (currentMhp > 0)
            {
                img_MinigameHp[--currentMhp].SetActive(false);
            }
        }

        public void Restart()
        {
            for(int i = 0; i < img_MinigameHp.Length; i++)
            {
                img_MinigameHp[i].SetActive(true);
                currentMhp = 3;
            }
        }
    }
}