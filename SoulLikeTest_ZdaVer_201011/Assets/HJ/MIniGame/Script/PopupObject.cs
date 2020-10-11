using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupObject : MonoBehaviour
{
    // Hp바 관련
    private Canvas uiCanvas;    // 부모가 될 Canvas
    [HideInInspector]
    public Vector3 PopupOffset = new Vector3(0, 2, 0);   // 머리 위(Y축)로 표시하기 위한 오프셋
    public GameObject PopupPrefab;  // HP바를 생성하기 위한 프리팹 리퍼런스 연결
    GameObject Popup;

    float distance;
    [Header("인식 범위")]
    [SerializeField] float recognitionRange;
    [Header("Cube위에 Player를 넣을것")]
    [SerializeField] Transform playerTr;

    // Start is called before the first frame update
    void Start()
    {
        SetPopup();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(GetComponentInParent<Transform>().position, playerTr.position);
        if (distance <= recognitionRange)
        {
            Popup.SetActive(true);
        }
        else
        {
            Popup.SetActive(false);
        }
    }

    void SetPopup()
    {
            uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

            // 적 머리 위에 표시할 Hp바 오브젝트 실제 생성
            Popup = Instantiate<GameObject>(PopupPrefab, uiCanvas.transform);

            // 생명 게이지가 따라가야 할 대상 및 offset 값 설정
            var _Popup = Popup.GetComponent<PopupText>();
            _Popup.targetTr = this.gameObject.transform;    // 적 캐릭터의 위치를 획득
            _Popup.offset = PopupOffset;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
        }
    }
}
