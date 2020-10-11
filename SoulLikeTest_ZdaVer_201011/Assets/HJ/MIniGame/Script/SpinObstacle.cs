using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObstacle : MonoBehaviour
{
    [Header("수정 가능")]
    [SerializeField]float spinSpeed = 100f; // 회전 속도
    [SerializeField]int obstacleHp = 5; // 체력
    int maxHp;

    TurretText theText;

    // Start is called before the first frame update
    void Start()
    {
        theText = FindObjectOfType<TurretText>();
        maxHp = obstacleHp;
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
    }

    void Spin()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
            obstacleHp--;
            Debug.Log("장애물 HP : " + obstacleHp);
        }
        if (obstacleHp == 0)
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
            theText.currentTurret--;
        }
    }

    public void Restart()
    {
        this.gameObject.SetActive(true);
        obstacleHp = maxHp;
    }
}
