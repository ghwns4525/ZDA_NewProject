using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Sc_EnemyBase : MonoBehaviour
{

    public float maxHp = 1000f;
    public float currentHp = 1000f;

    public float damage = 100f;

    protected float playerRealizeRange = 10f;
    protected float attackRange = 5f;
    /// <summary>
    /// 쿨타임을 담는 값
    /// </summary>
    protected float attackCoolTime = 5f;
    /// <summary>
    /// 실제로 계산되는 쿨타임
    /// </summary>
    protected float attackCoolTimeCacl = 5f;
    protected bool canAtk = true;

    protected float moveSpeed = 2f;

    protected GameObject Player;
    protected NavMeshAgent nvAgent;
    protected float distance;


    protected Animator Anim;
    protected Rigidbody rb;

    public LayerMask layerMask;

    // Start is called before the first frame update
    protected void Start()
    {
        // 플레이어 태그가 필요하겠네
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player == null)
        {
            Debug.Log("@@@@@@@@@Player Null @@@@@@@@@@@");
            Debug.Log("Player : " + Player);
            Debug.Log("Player.transform.position : " + Player.transform.position);
        }
        

        nvAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();

/*        parentRoom = transform.parent.transform.parent.gameObject;*/

        StartCoroutine(CalcCoolTime());
    }

    protected virtual IEnumerator CalcCoolTime()
    {
        while (true)
        {
            yield return null;
            if (!canAtk)
            {
                attackCoolTimeCacl -= Time.deltaTime;
                if (attackCoolTimeCacl <= 0)
                {
                    attackCoolTimeCacl = attackCoolTime;
                    canAtk = true;
                }
            }
        }
    }

    // 어택을 할 수 있는 상태인가? 
    protected bool CanAtkStateFunRayCast()
    {
        Vector3 targetDir = Player.transform.position - transform.position;

        Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), targetDir, out RaycastHit hit, 30f, layerMask);
        distance = Vector3.Distance(Player.transform.position, transform.position);

        if (hit.transform == null)
        {
            Debug.Log(" hit.transform == null");
            return false;
        }
        //  레이캐스트를 쏴서 맞으면 공격이 된다고 판단한다. 
        if (hit.transform.CompareTag("Player") && distance <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }

        
    }

    // 어택을 할 수 있는 상태인가? 
    protected bool CanAtkStateFunDistance()
    {
        distance = Vector3.Distance(Player.transform.position, transform.position);

        //  거리에 따라서 공격이 된다고 판단한다. 
        if (distance <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
