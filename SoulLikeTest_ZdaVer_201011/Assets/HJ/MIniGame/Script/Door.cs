using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    List<GameObject> currentDestroy;
    List<GameObject> TriggerDestroy;
    bool isMove = true;
    public bool isTrigger = false;
    public bool isTurret = false;   // 해당 위치에 터렛이 있는지 확인
    public bool isDestroy = false;
    float currentTime = 0;
    //float destroyTime = 0;
    [Header("수정 가능")]
    [SerializeField] float moveSpeed;   // 이동속도
    [Header("부술 포탑 넣기")]
    [SerializeField] public List<GameObject> Turret;    // 이 리스트에 들어있는 포탑을 다 부숴야 문이 열림
    [Header("트리거")]
    [SerializeField] public List<GameObject> Trigger;

    int maxTurret;
    int maxTrigger;
    int currentTurret;
    int currentTrigger;
    TurretText theText;


    // Start is called before the first frame updateㅉ
    void Start()
    {
        theText = FindObjectOfType<TurretText>();
        maxTurret = Turret.Count;
        maxTrigger = Trigger.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Turret.Count == 0 && Trigger.Count == 0)  // Trigger를 밟고, 모든 포탑을 파괴하면
        {
            currentTime += Time.deltaTime;
            if (isMove)
            {
                if (currentTime < 1.0f)
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);    // 오른쪽으로 이동
                }
                else
                {
                    currentTime = 0;
                    isMove = false;
                }
            }
        }

       /* currentDestroy = Turret.FindAll(x => x == null);    // 포탑이 파괴되어서 Turret 리스트가 비면 currentDestroy 리스트에 추가
        TriggerDestroy = Trigger.FindAll(x => x == null);
        currentTurret = currentDestroy.Count;
        currentTrigger = TriggerDestroy.Count;*/


        /*
        if (isDestroy && current.Count == Turret.Count)  // 문을 삭제시키고 싶다면 주석 풀기
        {
            destroyTime += Time.deltaTime;
            if (destroyTime > 10.0f)
            {
                Destroy(this.gameObject);
                destroyTime = 0;
            }
        }
        */
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Bullet") || collider.gameObject.CompareTag("TurretBullet") || collider.gameObject.CompareTag("DontDestroyBullet"))
        {
            Destroy(collider.gameObject);
        }
    }

}
