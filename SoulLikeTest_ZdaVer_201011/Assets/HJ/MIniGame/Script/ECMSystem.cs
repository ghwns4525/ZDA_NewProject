using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECMSystem : MonoBehaviour
{
    [Header("속도 제한")]
    [SerializeField] float SpeedLimit;  // 70% -> 0.7
    [SerializeField] float BlinkTime;
    [Header("크기")]
    [SerializeField] float Scale;

    Transform ECMTransform;
    MeshRenderer mesh;

    //[SerializeField] bool isSlow = false;

    float currentTime = 0;
    float slowSpeed = 0;
    [HideInInspector]
    public float maxSpeed = 0;

    Sc_Player sc_Player;
    PlayerDamage theDamage;
    

    // Start is called before the first frame update
    void Start()
    {
        sc_Player = FindObjectOfType<Sc_Player>();
        theDamage = FindObjectOfType<PlayerDamage>();
        mesh = GetComponent<MeshRenderer>();
        ECMTransform = GetComponent<Transform>();
        ECMTransform.localScale = new Vector3(Scale , 0.1f, Scale);
        maxSpeed = sc_Player.moveSpeed;
        slowSpeed = maxSpeed * SpeedLimit;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(maxSpeed);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (!theDamage.isSlow)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                theDamage.isSlow = true;
                sc_Player.moveSpeed = slowSpeed;
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //StartCoroutine(BlinkCo());
            currentTime += Time.deltaTime;

            if (currentTime > BlinkTime)
            {
                mesh.enabled = !mesh.enabled;   // 이렇게 하면 BlinkTime 보다 작으면 계속 실행됨
                currentTime -= BlinkTime;
                if(!theDamage.isSlow)
                {
                    sc_Player.moveSpeed = slowSpeed;
                    theDamage.isSlow = true;
                }

            }
            // 깜빡거리는 효과가 있어야함 -> BlinkTime초 마다 한번씩만 실행해야됨
        }

    }

    private void OnTriggerExit(Collider collider)
    {
        if (theDamage.isSlow)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                sc_Player.moveSpeed = maxSpeed;
                mesh.enabled = false;
                theDamage.isSlow = false;
            }
        }
    }
    
    private void OnDestroy()    // 물체가 삭제시 실행
    {
        if (theDamage.isSlow)
        {
            sc_Player.moveSpeed = maxSpeed;
            theDamage.isSlow = false;
        }
    }

}
