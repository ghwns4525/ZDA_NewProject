using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunTurret : MonoBehaviour
{
    [Header("수정가능")]
    [SerializeField] float Speed;   // 포탑 총알 속도
    [SerializeField] float bulletScale;  // 총알 크기

    private Transform bulletTr;
    private Rigidbody bulletRb;

    public Vector3 velo;

    

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
    }

    void Update()
    {
        bulletTr.position += velo * Time.deltaTime;
        //bulletRb.AddForce(velo);

        if (Menu.isGameOver) // 게임오버시 총알 프리팹 다 삭제
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void SetNormalizedDirection(Vector3 dir)
    {
        this.transform.InverseTransformDirection(dir);
        velo = dir * Speed;
    }

    public void Restart()
    {
        Destroy(this.gameObject);
    }
}
