using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;

    private RectTransform rectParent;
    private RectTransform recthp;

    [HideInInspector]
    public Vector3 offset = Vector3.zero;
    [HideInInspector]
    public Transform targetTr;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();

        uiCamera = canvas.worldCamera;

        rectParent = canvas.GetComponent<RectTransform>();
        recthp = this.gameObject.GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        // 적 캐릭터의 월드좌표를 스크린 좌표로 변환
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset); // 화면을 보는것 자체는 main카메라로 보기 때문에 Camera.main 사용

        // 적의 위치가 카메라 뒤쪽 영역(180도 회전) 일 때의 좌표값 보정
        if (screenPos.z < 0.0f)
        {
        screenPos *= -1.0f;
        }

        // 스크린 좌표를 RectTransform 기준 좌표로 변환
        var localPos = Vector2.zero;    // RectTranform 좌표값을 전달받는 변수
        RectTransformUtility.ScreenPointToLocalPointInRectangle( // 부모의 Recttransform, 스크린 좌표, UI랜더링 카메라(별도의 카메라가 존재하지 않으면 null), out 변환될 좌표를 받을 변수
        rectParent,
        screenPos,
        uiCamera,
        out localPos);

        // 게이지 위치 변경
        recthp.localPosition = localPos;
    }

}
