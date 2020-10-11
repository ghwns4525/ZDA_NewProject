using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class PlayerDamage : MonoBehaviour
{
    // 플레이어가 피해 받을시 3초간 깜빡이는 효과 및 무적
    bool isBlink = false;
    int currentBlinkCount = 0;
    int BlinkCount = 15;
    [SerializeField] MeshRenderer playerMesh = null;
    [Header("수정 가능")]
    [SerializeField] float BlinkSpeed;   // 피해받을시 깜빡이는 속도
    [SerializeField] public int playerHp = 3;
    [HideInInspector]
    public bool isSlow = false;

    PlayerHp minigamePlayerHp;

    // Start is called before the first frame update
    void Start()
    {
        minigamePlayerHp = FindObjectOfType<PlayerHp>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Obstacle"))
        {
            Damage();
        }

        if(collider.CompareTag("TurretBullet") || collider.CompareTag("DontDestroyBullet"))
        {
            Damage();
            Destroy(collider.gameObject);
            Debug.Log("포탑 총알 삭제");
        }
    }

    void Damage()
    {
        if (!isBlink)
        {
            if (playerHp > 0)
            {
                playerHp--;
                if (playerHp >= 1)
                {
                    StartCoroutine(BlinkCo());
                    
                }
                minigamePlayerHp.DecreaseMinigameHp();
                Debug.Log(playerHp);
            }
        }
    }

    IEnumerator BlinkCo()
    {
        isBlink = true;

        while(currentBlinkCount <= BlinkCount)
        {
            playerMesh.enabled = !playerMesh.enabled;
            yield return new WaitForSeconds(BlinkSpeed);
            currentBlinkCount++;
        }
        playerMesh.enabled = true;
        currentBlinkCount = 0;
        isBlink = false;
    }

    public void Restart()
    {
        playerHp = 3;
    }
}
