using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SG;

public class Turret : MonoBehaviour
{
    [Header("Cube위에 Player를 넣을것")]
    public Transform playerTr;  // 플레이어 위치
    private Transform turretTr;  // 포탑 위치
    public Vector3 difference;  // 포탑과 플레이어 간 거리차이
    public GameObject bulletprefab;
    public Transform[] firePos;
    float currentTime = 0;
    bool ismove = false;
    bool isrecognition = true;   // 플레이어를 인식할수 있는 범위에 있는지
    bool isReload = false;
    [SerializeField] public bool isDie = false;
    float distance;


    [Header("산탄형 총알 관련")]
    // 산탄형 총알 발사 관련
    [SerializeField] int cnt; // 몇줄로 나갈것인지
    [SerializeField] float angle; // 줄과 줄사이 각도
    float theta;
    float currentTime1 = 0;

    [Header("유도형 총알 발사 관련")]
    // 유도형 총알 발사 관련
    [SerializeField] Transform curvePos;
    [SerializeField] Transform curvePos1;
    float currentTime2 = 0;

    GameObject t_bullet;


    [Header("수정가능")]
    [SerializeField] float spinSpeed = 100.0f; // 포탑 회전 속도
    [SerializeField] float recognitionRange = 5.0f;  // 포탑 플레이어 인식 범위
    [SerializeField] float bpm = 60f;       // (bullet per min)분당 총알 개수 60f -> 1분에 60개 -> 초당 1개 발사
    [SerializeField] public int turretHp = 10;     // 포탑 체력
    [SerializeField] int currentBullet = 5; // 포탑 총알
    [SerializeField] float reloadTime;  // 재장전 시간
    float currentReloadTime;

    // Hp바 관련
    public GameObject hpBarPrefab;  // HP바를 생성하기 위한 프리팹 리퍼런스 연결
    [HideInInspector]
    public Vector3 hpBarOffset = new Vector3(0, 2, 0);   // 머리 위(Y축)로 표시하기 위한 오프셋
    private Canvas uiCanvas;    // 부모가 될 Canvas
    private Image hpBarImage;
    private Image lateHpBarImage;
    private Image parentHpBarImage;
    [SerializeField] float transparencyRange;
    [SerializeField] float transparencyValue;

    

    //[SerializeField] public List<GameObject> maxTurret;
    [SerializeField] List<int> maxHp;   // 자기 자신밖에 못받아오게 만들어둠 -> 사용할때는 maxHp[0]으로 사용하면 자기 자신을 사용하는것

    TurretText theText;
    Door[] theDoor;
    Sc_Player thePlayer;
    PlayerDamage theDamage;
    ECMSystem theECM;

    // Start is called before the first frame update
    void Start()
    {
        turretTr = GetComponent<Transform>();
        theText = FindObjectOfType<TurretText>();
        theDoor = FindObjectsOfType<Door>();
        thePlayer = FindObjectOfType<Sc_Player>();
        theDamage = FindObjectOfType<PlayerDamage>();
        theECM = FindObjectOfType<ECMSystem>();
        currentReloadTime = reloadTime;
        maxHp.Add(turretHp);
        SetHpBar();
    }

    // Update is called once per frame
    void Update()
    {
        TurretMoveTarget();
        lateHpBarImage.fillAmount = Mathf.Lerp(lateHpBarImage.fillAmount, (float)turretHp / maxHp[0], Time.deltaTime * 5f);
    }

    void TurretMoveTarget()
    {
        if (playerTr != null)
        {
            Quaternion currentQ = Quaternion.LookRotation(playerTr.position - turretTr.position);
            difference = playerTr.position - turretTr.position;

            if (this.gameObject.CompareTag("SpinTurret"))
            {
                transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
            }

            distance = Vector3.Distance(transform.position, playerTr.position);   // 거리의 차
            if (isrecognition)
            {
                if (!isReload)
                {

                    if (distance <= recognitionRange)
                    {

                        if (!this.gameObject.CompareTag("SpinTurret"))
                        {
                            turretTr.rotation = Quaternion.Slerp(turretTr.rotation, currentQ, spinSpeed * Time.deltaTime);
                        }

                        ismove = true;
                    }
                    else
                    {
                        ismove = false;
                    }
                }
            }
            if (currentBullet > 0)
            {
                if (this.gameObject.CompareTag("ShotTurret"))
                {
                    AngleFire();
                }
                else
                {
                    NomalFire();
                }
            }
            else
            {
                if (currentReloadTime == 0)
                {
                    isReload = false;
                    currentBullet = 5;
                    currentReloadTime = 0;
                }
                else
                {
                    isReload = true;
                    currentReloadTime -= Time.deltaTime;
                    if (currentReloadTime < 0)
                    {
                        isReload = false;
                        currentBullet = 5;
                        currentReloadTime = reloadTime;
                    }
                }

            }
        }
            if (distance <= transparencyRange)
            {
                hpBarImage.color = new Color(hpBarImage.color.r, hpBarImage.color.g, hpBarImage.color.b, transparencyValue);
                lateHpBarImage.color = new Color(lateHpBarImage.color.r, lateHpBarImage.color.g, lateHpBarImage.color.b, transparencyValue);
                parentHpBarImage.color = new Color(parentHpBarImage.color.r, parentHpBarImage.color.g, parentHpBarImage.color.b, transparencyValue);
            }
            else
            {
                hpBarImage.color = new Color(hpBarImage.color.r, hpBarImage.color.g, hpBarImage.color.b, 1f);
                lateHpBarImage.color = new Color(lateHpBarImage.color.r, lateHpBarImage.color.g, lateHpBarImage.color.b, 1f);
                parentHpBarImage.color = new Color(parentHpBarImage.color.r, parentHpBarImage.color.g, parentHpBarImage.color.b, 1f);
            }
        
    }

    public void NomalFire()
    {
        if (ismove)
        {
            if (!isReload)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= 60f / bpm)
                {

                    for (int i = 0; i < firePos.Length; i++)
                    {
                        GameObject t_bullet = Instantiate(bulletprefab, firePos[i].position, firePos[i].rotation);

                        currentBullet--;
                    }
                    currentTime -= 60f / bpm;
                }
            }
        }

    }

    //Vector3 t_Vector = new Vector3(0, 0, 0);

    public void AngleFire()
    {
        if (ismove)
        {
            if (!isReload)
            {
                // 360도 각도 구하는 법
                float angler = Quaternion.FromToRotation(Vector3.back, transform.position - playerTr.position).eulerAngles.x
                    + Quaternion.FromToRotation(Vector3.back, transform.position - playerTr.position).eulerAngles.y
                    + Quaternion.FromToRotation(Vector3.back, transform.position - playerTr.position).eulerAngles.z;

                currentTime1 += Time.deltaTime;
                float gap = cnt > 1 ? angle / (float)(cnt - 1) : 0;
                float startAngle = -angle / 2.0f;
                if (currentTime1 >= 60f / bpm)
                {
                    for (int i = 0; i < cnt; i++)
                    {
                        for (int x = 0; x < firePos.Length; x++)
                        {
                            theta = startAngle + gap * (float)i + angler;    // theta값이 변경되니 돌아감 타겟 위치의 각도값(0~360)을 넣어주면 그만큼 돌아감
                            theta *= Mathf.Deg2Rad;
                            t_bullet = Instantiate(bulletprefab, firePos[x].position, firePos[x].rotation);
                            t_bullet.GetComponent<ShotGunTurret>().SetNormalizedDirection(
                            new Vector3(Mathf.Sin(theta), 0, Mathf.Cos(theta)));    // 각도 조절                            
                            // firePos를 기준으로 직선방향으로 발사를 하고 싶음
                            // 근데 지금은 글로벌 위치가 고정되서 잡혀있는것 같음.
                        }
                    }
                    currentTime1 -= 60f / bpm;
                }
            }
        }
    }

    public void InduceFire()
    {
        if (ismove)
        {
            if (!isReload)
            {
                currentTime2 += Time.deltaTime;
                if (currentTime2 >= 60f / bpm)
                {

                    for (int i = 0; i < firePos.Length; i++)
                    {
                        GameObject t_bullet = Instantiate(bulletprefab, firePos[i].position, firePos[i].rotation);

                        currentBullet--;
                    }
                    currentTime2 -= 60f / bpm;
                }
            }
        }
    }

    /*    Vector3 GetPointOnBezierCurve(float t)
        {
            float u = 1f - t;
            float t2 = t * t;
            float u2 = u * u;
            float u3 = u2 * u;
            float t3 = t2 * t;

            Vector3 result = (u3 * turretTr.position) + ((3f * u2 * t) * curvePos.position) + ((3f * u * t2) * curvePos1.position) + (t3 * playerTr.position);   // 점 4개의 베지에 곡선
            // Vector3 result1 = (u2 * turretTr.position) + (2f * t * u * curvePos.position) + (t2 * playerTr.position);    // 점 3개의 베지에 곡선
            return result;
        }*/

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            turretHp--;
            hpBarImage.fillAmount = (float)turretHp / maxHp[0];
            Debug.Log("turret HP : " + turretHp);
            Destroy(collider.gameObject);
            if (turretHp == 0)
            {
                for (int i = 0; i < theDoor.Length; i++)
                {
                    if (theDoor[i].Turret.Contains(this.gameObject))
                    {
                        theDoor[i].Turret.Remove(this.gameObject);
                    }
                }
                this.gameObject.SetActive(false);
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;   // Hp가 0이되면 투명처리
                lateHpBarImage.color = new Color(lateHpBarImage.color.r, lateHpBarImage.color.g, lateHpBarImage.color.b, 0f);
                if (!this.CompareTag("Wall"))
                {
                    theText.currentTurret--;
                }
                isDie = true;
                if (transform.GetChild(0).CompareTag("SlowZone"))
                {
                    thePlayer.moveSpeed = theECM.maxSpeed;
                    theDamage.isSlow = false;
                }
            }
        }
    }


    public void Restart()
    {
        if (isDie)
        {
            this.gameObject.SetActive(true);
            isDie = false;
            Debug.Log(turretHp);
        }
        turretHp = maxHp[0];
        hpBarImage.fillAmount = (float)turretHp / maxHp[0];
        lateHpBarImage.fillAmount = (float)turretHp / maxHp[0];
        hpBarImage.color = new Color(hpBarImage.color.r, hpBarImage.color.g, hpBarImage.color.b, 1f);
        parentHpBarImage.color = new Color(parentHpBarImage.color.r, parentHpBarImage.color.g, parentHpBarImage.color.b, 1f);
    }

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        // 적 머리 위에 표시할 Hp바 오브젝트 실제 생성
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);

        // fillAmount 속성을 변경할 Image 객체 획득
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[2];

        lateHpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        parentHpBarImage = hpBar.GetComponent<Image>();

        // 생명 게이지가 따라가야 할 대상 및 offset 값 설정
        var _hpBar = hpBar.GetComponent<HpBar>();
        _hpBar.targetTr = this.gameObject.transform;    // 적 캐릭터의 위치를 획득
        _hpBar.offset = hpBarOffset;
    }

    // 1. 태그로 찾는 방법
    // 2. 따로 캔버스를 생성해서 그 위에 그린다음 포지션을 따라가는 방법
}
