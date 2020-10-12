using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class Charater : MonoBehaviour
    {
        int reinforce;

        [Header("클래스")]
        [SerializeField] Image Class_view;
        [SerializeField] Sprite[] Class;

        [Header("패시브")]
        [SerializeField] Sprite[] Passive;
        [SerializeField] Image[] Passive_view;

        [Header("강화")]
        [SerializeField] GameObject[] Btn_reinforce;
        [SerializeField] Image[] Weapon;
        int MaxWeaponReinforce = 11;
        int CurrentWeaponReinforce = 1;
        [SerializeField] Image[] Health;
        int MaxHealthReinforce = 11;
        int CurrentHealthReinforce = 1;
        [SerializeField] Image[] Defense;
        int MaxDefenseReinforce = 11;
        int CurrentDefenseReinforce = 1;
        [SerializeField] Image[] Attack;
        int MaxAttackReinforce = 11;
        int CurrentAttackReinforce = 1;

        [Header("세부설명")]
        [SerializeField] string[] ClassKinds;
        [SerializeField] Text txt_Class;
        [SerializeField] Text txt_MaxHp;
        [SerializeField] Text txt_Power;
        [SerializeField] Text txt_Defense;

        [Header("퀘스트")]
        [SerializeField] string[] QuestList;
        [SerializeField] Text txt_Quest;
        int Quest_num = 0;


        H_PlayerLocomotion Player;
        H_MinigameInputHandler InputHandler;

        // Start is called before the first frame update
        void Start()
        {
            Player = FindObjectOfType<H_PlayerLocomotion>();
            InputHandler = FindObjectOfType<H_MinigameInputHandler>();
            Class_view.overrideSprite = Class[4];
            txt_Class.text = ClassKinds[0];
        }

        // Update is called once per frame
        void Update()
        {
            IncreaseReinforce();
            if(reinforce > 0)
            {
                for(int i = 0; i < Btn_reinforce.Length; i++)
                {
                    Btn_reinforce[i].SetActive(true);
                }
            }
            else if(reinforce == 0)
            {
                for (int i = 0; i < Btn_reinforce.Length; i++)
                {
                    Btn_reinforce[i].SetActive(false);
                }
            }
            if(CurrentWeaponReinforce == MaxWeaponReinforce)
            {
                Btn_reinforce[0].SetActive(false);
            }
            if (CurrentHealthReinforce == MaxHealthReinforce)
            {
                Btn_reinforce[1].SetActive(false);
            }
            if (CurrentDefenseReinforce == MaxDefenseReinforce)
            {
                Btn_reinforce[2].SetActive(false);
            }
            if (CurrentAttackReinforce == MaxAttackReinforce)
            {
                Btn_reinforce[3].SetActive(false);
            }
            txt_MaxHp.text = Player.t_PlayerHp.ToString();
            txt_Defense.text = Player.t_Defense.ToString();
            txt_Power.text = Player.t_Power.ToString();
            txt_Quest.text = QuestList[Quest_num];
        }

        public void OnPassive1()
        {
            if(Player.t_Passive1)
            {
                Passive_view[0].overrideSprite = Passive[1];
                Player.t_Passive1 = false;
            }
            else
            {
                Passive_view[0].overrideSprite = Passive[0];
                Player.t_Passive1 = true;
            }
        }
        public void OnPassive2()
        {
            if (Player.t_Passive2)
            {
                Passive_view[1].overrideSprite = Passive[1];
                Player.t_Passive2 = false;
            }
            else
            {
                Passive_view[1].overrideSprite = Passive[0];
                Player.t_Passive2 = true;
            }
        }
        public void OnPassive3()
        {
            if (Player.t_Passive3)
            {
                Passive_view[2].overrideSprite = Passive[1];
                Player.t_Passive3 = false;
            }
            else
            {
                Passive_view[2].overrideSprite = Passive[0];
                Player.t_Passive3 = true;
            }
        }
        public void OnPassive4()
        {
            if (Player.t_Passive4)
            {
                Passive_view[3].overrideSprite = Passive[1];
                Player.t_Passive4 = false;
            }
            else
            {
                Passive_view[3].overrideSprite = Passive[0];
                Player.t_Passive4 = true;
            }
        }
        public void OnWeaponReinforce()
        {
            if (reinforce > 0)
            {
                for (int i = 0; i < CurrentWeaponReinforce; i++)
                {
                    Weapon[i].color = Color.yellow;
                }
                if (CurrentWeaponReinforce < MaxWeaponReinforce)
                {
                    CurrentWeaponReinforce++;
                }
                Player.t_Power *= 1.2f;
                reinforce--;
            }
        }
        public void OnHealthReinforce()
        {
            if (reinforce > 0)
            {
                for (int i = 0; i < CurrentHealthReinforce; i++)
                {
                    Health[i].color = Color.yellow;
                }
                if (CurrentHealthReinforce < MaxHealthReinforce)
                {
                    CurrentHealthReinforce++;
                }
                Player.t_PlayerHp += 10;
                reinforce--;
            }
        }
        public void OnDefenseReinforce()
        {
            if (reinforce > 0)
            {
                for (int i = 0; i < CurrentDefenseReinforce; i++)
                {
                    Defense[i].color = Color.yellow;
                }
                if (CurrentDefenseReinforce < MaxDefenseReinforce)
                {
                    CurrentDefenseReinforce++;
                }
                Player.t_Defense += 11;
                reinforce--;
            }
        }
        public void OnAttackReinforce()
        {
            if (reinforce > 0)
            {
                for (int i = 0; i < CurrentAttackReinforce; i++)
                {
                    Attack[i].color = Color.yellow;
                }
                if (CurrentAttackReinforce < MaxAttackReinforce)
                {
                    CurrentAttackReinforce++;
                }
                Player.t_Power += 12;
                reinforce--;
            }
        }
        void IncreaseReinforce()
        {
            if (InputHandler.testF_Input)
            {
                reinforce++;
                Quest_num++;
                if (Quest_num >= QuestList.Length)
                {
                    Quest_num = 0;
                }
            }
        }
    }
}
