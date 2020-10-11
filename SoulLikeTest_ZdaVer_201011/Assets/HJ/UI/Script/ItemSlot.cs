using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] GameObject[] Slots;
        [SerializeField] List<Transform> Img_Slots;

        int Count = 0;
        int MaxCount;

        Sc_InputHandler inputHandler;
        // Start is called before the first frame update
        void Start()
        {
            MaxCount = Slots.Length - 1;
            inputHandler = FindObjectOfType<Sc_InputHandler>();
            for(int i = 0; i < Slots.Length; i++)
            {
                for(int j = 0; j < Slots[i].transform.childCount; j++)
                {
                    Img_Slots.Add(Slots[i].transform.GetChild(j));
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void OnRight()
        {
            if (Count < MaxCount)
            {
                for (int i = 0; i < Slots.Length; i++)
                {
                    Slots[i].SetActive(false);
                }
                Slots[++Count].SetActive(true);
            }
            else if (Count == MaxCount)
            {
                Count = 0;
                for (int i = 0; i < Slots.Length; i++)
                {
                    Slots[i].SetActive(false);
                }
                Slots[Count].SetActive(true);
            }

        }
        public void OnLeft()
        {
            if (Count > 0)
            {
                for (int i = 0; i < Slots.Length; i++)
                {
                    Slots[i].SetActive(false);
                }
                Slots[--Count].SetActive(true);
            }
            else
            {
                Count = MaxCount;
                for (int i = 0; i < Slots.Length; i++)
                {
                    Slots[i].SetActive(false);
                }
                Slots[Count].SetActive(true);
            }
        }
    }
}
