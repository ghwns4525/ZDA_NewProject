using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // 이동 장애물
    [Header("수정 가능")]
    [SerializeField]float moveSpeed = 4.0f; // 이동속도
    [SerializeField] float delta = 5.0f;     // 이동거리
    float firstTr;
    Vector3 currentPos;     // 현재 위치

    LineRenderer Line;

    int turn = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        Line = GetComponent<LineRenderer>();
        Line.SetPosition(0, transform.position + new Vector3(-delta - 1, -0.5f, 0));
        Line.SetPosition(1, transform.position + new Vector3(delta + 1, -0.5f, 0));
        firstTr = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(transform.position.x < firstTr - delta)
        {
            turn = 1;
        }
        else if(transform.position.x > firstTr + delta)
        {
            turn = -1;
        }
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * turn);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject); 
        }
    }

}
