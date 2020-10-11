using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
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

    /*[SerializeField] GameObject m_goPrefab = null;

    List<Transform> m_objectList = new List<Transform>();
    List<GameObject> m_hpBarList = new List<GameObject>();

    Camera m_cam = null;

    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;

        GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < t_objects.Length; i++)
        {
            m_objectList.Add(t_objects[i].transform);
            GameObject t_hpBar = Instantiate(m_goPrefab, t_objects[i].transform.position, Quaternion.identity, transform);
            m_hpBarList.Add(t_hpBar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < m_objectList.Count; i++)
        {
            m_hpBarList[i].transform.position = m_cam.WorldToScreenPoint(m_objectList[i].position + new Vector3(0, 2, 0));
        }
    }*/


}
