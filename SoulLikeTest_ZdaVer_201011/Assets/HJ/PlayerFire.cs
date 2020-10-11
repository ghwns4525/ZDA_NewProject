using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    float currentTime = 0;

    [Header("수정 가능")]
    [SerializeField] float bpm; // 60초 / bpm

    H_MinigameInputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = FindObjectOfType<H_MinigameInputHandler>();   
    }

    // Update is called once per frame
    void Update()
    {
      if (inputHandler.fireFlag)    // fire_Input를 계속 false로만 받음
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 60f / bpm)
            {
                Fire();
                currentTime -= 60f / bpm;
            }
            
        }
    }

    void Fire()
    {
        /*GameObject t_bullet = ObjectPool.instance.bulletQueue.Dequeue();
        t_bullet.transform.position = firePos.position;
        t_bullet.SetActive(true);*/
        Instantiate(bullet, firePos.position, firePos.rotation);
    }
}
