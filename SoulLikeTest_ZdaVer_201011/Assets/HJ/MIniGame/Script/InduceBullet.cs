using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InduceBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;   // 총알 속도
    [SerializeField] float bulletScale;  // 총알 크기

    Transform bulletTr;
    Transform playerTr;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);    // 3초뒤 삭제
    }

    void Awake()
    {
        bulletTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        bulletTr.localScale = new Vector3(transform.localScale.x, bulletScale, bulletScale);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += move * moveSpeed * Time.deltaTime;
        // 임의로 위치를 지정해서 꺾이는 방식
/*        WaitTime += Time.deltaTime;
        if(WaitTime < 0.2f)
        {
            transform.Translate(transform.right * moveSpeed * 0.1f, Space.World);            
        }*/
        //else
        {
            //moveSpeed += Time.deltaTime;
            //float t = moveSpeed / dis;
            transform.position = Vector3.MoveTowards(bulletTr.position, playerTr.position, moveSpeed * 0.01f);
        }


        Vector3 move = playerTr.position - bulletTr.position;
        Quaternion rot = Quaternion.LookRotation(move);
        bulletTr.rotation = Quaternion.Slerp(bulletTr.rotation, rot, Time.deltaTime * 2.0f);
        // 총알의 속도가 가까워질수록 느려짐
        // 속도가 같으려면 들어가는 값이 일정해야함

        if(Menu.isGameOver) // 게임오버시 총알 프리팹 다 삭제
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

    public void Restart()
    {
        Destroy(this.gameObject);
    }
}
