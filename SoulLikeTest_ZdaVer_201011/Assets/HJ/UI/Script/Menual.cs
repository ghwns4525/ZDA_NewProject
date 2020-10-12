using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class Menual : MonoBehaviour
    {
        [Header("메뉴 텍스트")]
        [SerializeField] Text[] menu;
        [Header("설명 이미지")]
        [SerializeField] Image Img_explanation;
        [SerializeField] Image[] Img_explanationGroup;
        [Header("설명 텍스트")]
        [SerializeField] Text Txt_explanation;
        [SerializeField] string[] Txt_explanationGroup;


        int count = 0;
        int maxCount;

        H_MinigameInputHandler inputHandler;

        // Start is called before the first frame update
        void Start()
        {
            inputHandler = FindObjectOfType<H_MinigameInputHandler>();
            maxCount = menu.Length - 1;
        }

        // Update is called once per frame
        void Update()
        {
           if(inputHandler.testDown_Input)
            {
                if (count < maxCount)
                {
                    // 텍스트의 위치를 맨위에 있는 오브젝트를 맨 밑으로 보낸다.
                    menu[count++].transform.SetAsLastSibling();
                }
            }
            if(inputHandler.testUp_Input)
            {
                if(count > 0)
                {
                    // 텍스트의 위치를 맨뒤에 있는 오브젝트를 맨 위으로 보낸다.
                    menu[--count].transform.SetAsFirstSibling();
                    //Img_explanation.sprite = Img_explanationGroup[count].sprite;
                    //Txt_explanation.text = Txt_explanationGroup[count];
                }
            }
            Txt_explanation.text = Txt_explanationGroup[count];
            //Img_explanation.sprite = Img_explanationGroup[count].sprite;
            Debug.Log(count);
        }
    }
}
