using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    private Transform bulletTr;
    private Rigidbody bulletRb;

    float currnetTime = 0;

    [Header("수정가능")]
    [SerializeField] float BulletSpeed = 1000.0f;   // 총알 속도
    [SerializeField] float bulletScale = 0.2f;  // 총알 크기

    // Start is called before the first frame update
    void Awake()
    {
        bulletTr = GetComponent<Transform>();
        bulletRb = GetComponent<Rigidbody>();
        bulletTr.localScale = new Vector3(bulletScale, bulletScale, bulletScale);
    }

    void OnEnable()
    {
        bulletRb.AddForce(transform.forward * BulletSpeed);
    }

    void Update()
    {
        currnetTime += Time.deltaTime;
        if(currnetTime > 3.0f)
        {
            Destroy(this.gameObject);
            Debug.Log("삭제");
        }

        if (Menu.isGameOver) // 게임오버시 총알 프리팹 다 삭제
        {
            Destroy(this.gameObject);
        }
    }


}
