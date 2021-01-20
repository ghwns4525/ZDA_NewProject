using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hj_MonsterStats : MonoBehaviour
{
    [SerializeField]
    int maxHp;

    int currentHp;
    bool isDie;

    Hj_MonsterAnimationHandler hj_MonsterAnimationHandler;

    public bool IsDie
    {
        get
        {
            return isDie;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        hj_MonsterAnimationHandler = GetComponentInChildren<Hj_MonsterAnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterDie();
    }

    public void TakeDamage(int Damage)
    {
        currentHp = currentHp - Damage;
        Debug.Log(currentHp);
        hj_MonsterAnimationHandler.PlayTargetActionAnimation("Act_Hit", true);
    }

    void MonsterDie()
    {
        if (currentHp <= 0)
        {
            isDie = true;
            hj_MonsterAnimationHandler.PlayTargetActionAnimation("Act_Die", true);
        }
        else
        {
            isDie = false;
        }
    }
}
