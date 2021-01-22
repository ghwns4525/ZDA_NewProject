using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public ItemData_Test item; // 아이템 정보를 슬롯에 넣어줌
    public int itemCount; // 아이템 개수
    public Image itemImage; // 슬롯에 보여질 아이템 이미지

    [SerializeField]
    private Text text_Count; // 슬롯에 아이템 개수가 보여질 텍스트
    [SerializeField]
    private GameObject text_BackGround; // 아이템이 없을 때 


    //아이템의 이미지 알파값 조정( 기본값 0)
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;

    }

    //아이템을 인벤토리에 추가(직관적으로 보이게) 함수
    public void AddItem(ItemData_Test _item, int _count = 1)
    {
        Debug.Log("애드아이템 호출");
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemIcon;

        text_BackGround.SetActive(true);
        text_Count.text = itemCount.ToString();



        SetColor(1);
    }

    //슬롯에 있는 아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

    }

    //슬롯을 비어있는 상태로 돌리는 함수
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "";
    }

    //드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
    }

    //드래그 유지
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }
    //드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    //마우스 드래그가 끝날 때 마우스 위치가 다른 슬롯일 때 실행
    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    //아이템 드롭 시 실행
    private void ChangeSlot()
    {
        ItemData_Test _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if(_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
   
}
