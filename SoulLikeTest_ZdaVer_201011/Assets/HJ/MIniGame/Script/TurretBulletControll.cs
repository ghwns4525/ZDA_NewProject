using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletControll : MonoBehaviour
{
    [Header("수정가능")]
    [SerializeField] float TurretBulletSpeed;   // 포탑 총알 속도
    [SerializeField] float bulletScale;  // 총알 크기

    private Transform bulletTr;
    private Rigidbody bulletRb;

    public Vector3 velo = new Vector3(0, 0, 0);



    void Start()
    {
        Destroy(gameObject, 3.0f);    // 3초뒤 삭제
    }

    // Start is called before the first frame update
    void Awake()
    {
        bulletTr = GetComponent<Transform>();
        bulletRb = GetComponent<Rigidbody>();
        bulletTr.localScale = new Vector3(bulletScale, bulletScale, bulletScale);
    }

    void OnEnable()
    {
        bulletRb.AddForce(transform.forward * TurretBulletSpeed); // 속도가 일정 -> TurretBulletSpeed == 1000;
    }

    void Update()
    {
        //bulletRb.AddForce(transform.forward * TurretBulletSpeed);   // 속도가 갈수록 증가 -> TurretBulletSpeed == 10;

        if (Menu.isGameOver) // 게임오버시 총알 프리팹 다 삭제
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Bullet") && this.gameObject.CompareTag("TurretBullet"))
        {
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void Restart()
    {
        Destroy(this.gameObject);
    }
}
