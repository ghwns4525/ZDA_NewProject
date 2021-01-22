using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundManager : MonoBehaviour
{
    [Header("배경음악. 수정 가능")]
    [SerializeField]
    AudioSource[] BGM;

    [Header("전투 효과음. 수정 가능")]
    [SerializeField]
    AudioSource[] playSfx;

    [Header("메뉴 효과음. 수정 가능")]
    [SerializeField]
    AudioSource[] menuSfx;

    
  

    // Start is called before the first frame update


    public void SetBGM_Volume(float volume)
    {
        for(int i = 0; i < BGM.Length; i++)
        {
            BGM[0].volume = volume;
        }
        
    }
   
    public void SetPlay_Volume(float volume)
    {
        for (int i = 0; i < playSfx.Length; i++)
        {
            playSfx[0].volume = volume;
        }
    }
    public void SetMenu_Volume(float volume)
    {
        for (int i = 0; i < menuSfx.Length; i++)
        {
            menuSfx[i].volume = volume;
        }
    }

    public void OnMenuSfx() 
    {
        WhatSound();
    }

    void WhatSound()
    {
        if(EventSystem.current.currentSelectedGameObject.tag == "ButtonA")
        {
            menuSfx[0].Play();
        }
    }
}
