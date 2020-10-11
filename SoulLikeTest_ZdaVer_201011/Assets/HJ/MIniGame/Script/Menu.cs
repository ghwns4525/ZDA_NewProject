using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject Img_Menu;
    [SerializeField] GameObject Img_GameOver;
    [SerializeField] GameObject Img_StageClear;
    [SerializeField] GameObject Charater;
    [SerializeField] GameObject Item;
    [SerializeField] GameObject MenuContainer;
    [SerializeField] GameObject Setting;
    [SerializeField] GameObject Exit;

    public static bool isGameOver = false;

    Sc_InputHandler inputHandler;
    PlayerDamage thePlayerDamage;
    Sc_Player thePlayer;
    TurretText theText;
    StageClear theClear;
    Turret [] theTurret;
    SpinObstacle[] theObstacle;
    DoorTrigger[] theDoorTrigger;
    PlayerHp thePlayerHp;
    TurretBulletControll[] theTurretBullet;
    InduceBullet[] theInduceBullet;
    ShotGunTurret[] theShotgunBullet;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = FindObjectOfType<Sc_InputHandler>();
        thePlayerDamage = FindObjectOfType<PlayerDamage>();
        thePlayer = FindObjectOfType<Sc_Player>();
        theText = FindObjectOfType<TurretText>();
        theClear = FindObjectOfType<StageClear>();
        theTurret = FindObjectsOfType<Turret>();
        theObstacle = FindObjectsOfType<SpinObstacle>();
        theDoorTrigger = FindObjectsOfType<DoorTrigger>();
        thePlayerHp = FindObjectOfType<PlayerHp>();
        theTurretBullet = FindObjectsOfType<TurretBulletControll>();
        theInduceBullet = FindObjectsOfType<InduceBullet>();
        theShotgunBullet = FindObjectsOfType<ShotGunTurret>();
    }

    // Update is called once per frame
    void Update()
    {
        //@Test
      /*  if (inputHandler.menu_Input)
        {
            MenuOption();
        }
        GameOver();
        StageClear();
        if (isGameOver)
        {
            if (inputHandler.anykey_Input)
            {
                GameRestart();
            }
        }
        if(theClear.isClear)
        {
            if (inputHandler.anykey_Input)
            {
                GameRestart();
            }
        }*/
    }

    public void MenuOption()
    {
        Debug.Log("메뉴");
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.0f;
            Img_Menu.SetActive(true);
            MenuReset();
            Charater.SetActive(true);
        }
        else if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
            Img_Menu.SetActive(false);
        }
    }

    void GameOver()
    {
        if(thePlayerDamage.playerHp == 0)
        {
            Img_GameOver.SetActive(true);
            Time.timeScale = 0.0f;
            isGameOver = true;
        }
    }

    void StageClear()
    {
        if(theClear.isClear)
        {
            Img_StageClear.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    void GameRestart()
    {
        if(isGameOver)
        {
            Img_GameOver.SetActive(false);
        }
        if(theClear.isClear)
        {
            Img_StageClear.SetActive(false);
            theClear.isClear = false;
        }
        Time.timeScale = 1.0f;
        isGameOver = false;

        // 다른 스크립트들에서 가져오는 경우
        thePlayerDamage.Restart(); // 플레이어 Hp
        thePlayer.Restart();    // 플레이어 위치 재정렬
        theText.Restart();
        theClear.Restart();
        thePlayerHp.Restart();
        for (int i = 0; i < theTurret.Length; i++)
        {
            theTurret[i].Restart();
        }
        for (int i = 0; i < theObstacle.Length; i++)
        {
            theObstacle[i].Restart();
        }
        for(int i = 0; i < theDoorTrigger.Length; i++)
        {
            theDoorTrigger[i].Restart();
        }
        for(int i = 0; i < theTurretBullet.Length; i++)
        {
            theTurretBullet[i].Restart();
        }
        for(int i = 0; i < theInduceBullet.Length; i++)
        {
            theInduceBullet[i].Restart();
        }
        for(int i = 0; i < theShotgunBullet.Length; i++)
        {
            theShotgunBullet[i].Restart();
        }
    }

    void MenuReset()
    {
        Charater.SetActive(false);
        Item.SetActive(false);
        MenuContainer.SetActive(false);
        Setting.SetActive(false);
        Exit.SetActive(false);
    }

    public void OnCharater()
    {
        MenuReset();
        Charater.SetActive(true);
    }

    public void OnItem()
    {
        MenuReset();
        Item.SetActive(true);
    }
    public void OnMenu()
    {
        MenuReset();
        MenuContainer.SetActive(true);
    }
    public void OnSetting()
    {
        MenuReset();
        Setting.SetActive(true);
    }
    public void OnExit()
    {
        MenuReset();
        Exit.SetActive(true);
    }
}
