using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_GameMng : MonoBehaviour
{

    public static Sc_GameMng ins;
    public enum StageEnum
    {
        None,
        NomalStage,
        BossStage

    }
    public StageEnum stageEnum = StageEnum.None;

    [SerializeField]
    GameObject bossObj;
    public GameObject BossObj
    {
        get
        {
            bossObj = GameObject.FindGameObjectWithTag("Boss");
            return bossObj;
        }
    }


    public GameObject[] LockOnTargetObjs;
    [SerializeField]
    GameObject lockOnTargetObj;
    public GameObject LockOnTargetObj
    {
        get
        {
            if(lockOnTargetObj == null)
            {
                lockOnTargetObj = bossObj;
            }
            return lockOnTargetObj;
        }
        set
        {
            lockOnTargetObj = value;
        }
    }

    private void Awake()
    {
        if(ins == null)
        {
            ins = this;
        }
        else
        {
            Debug.LogWarning("싱글턴인데 2번이나 생성해!?");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bossObj = GameObject.FindGameObjectWithTag("Boss");
        if(bossObj == null)
        {
            stageEnum = StageEnum.NomalStage;
        }
        else
        {
            stageEnum = StageEnum.BossStage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Targetting Logic 

    #region Targetting Value

    int lockOnTargetIndex = 0;
    public int LockOnTargetIndex
    {

        get
        {
            return lockOnTargetIndex;
        }
        set
        {
            // 8개 있으면 0~7

            Debug.Log("TargetsIndex : " + lockOnTargetIndex);
            // 7개 보다 크면 0으로

            // LockOnTargetObjs 예외 처리

            if (LockOnTargetObjs != null)
            {
                if (LockOnTargetObjs.Length - 1 < lockOnTargetIndex)
                {
                    lockOnTargetIndex = 0;
                }
                // 0보다 작으면 ( 음수가 되면 ) 최대값으로 
                else if (lockOnTargetIndex < 0)
                {
                    lockOnTargetIndex = LockOnTargetObjs.Length - 1;
                }
            }


        }
    }

    #endregion


    /// <summary>
    /// 가장 가까운 Enemy를 찾아서 리턴한다.
    /// </summary>
    /// <returns></returns>
    public GameObject NearEnemyTargetting()
    {
        //타겟팅될 오브젝트를 모두 TargetObj에 집어넣는다.
        LockOnTargetObjs = GameObject.FindGameObjectsWithTag("Enemy");        //타겟이 가능한 오브젝트 집어넣기        
                                                                              //거리순으로 정렬한다.        
        Vector3 playerPos = transform.position;

        Debug.Log("현재 캐릭터의 좌표는 " + transform.position);
        Array.Sort<GameObject>(LockOnTargetObjs, (x, y) => (playerPos - x.transform.position).sqrMagnitude.CompareTo((playerPos - y.transform.position).sqrMagnitude));
        //TargetObjs.Sort((x,y) => (ch_pos - x.transform.position).sqrMagnitude.CompareTo((ch_pos -y.transform.position).sqrMagnitude));  //소트가 제대로 되지 않음 (거리기준으로 되야 됨)
        for (int i = 0; i < LockOnTargetObjs.Length; i++)
        {
            Debug.Log(i + "번째 오브젝트(" + LockOnTargetObjs[i].name + ")의 거리 = " + (playerPos - LockOnTargetObjs[i].transform.position).sqrMagnitude);
        }
        //가장 가까운 오브젝트를 반환한다.

        LockOnTargetObj = LockOnTargetObjs[0];
        Debug.Log("타게팅 된 오브제 :" + LockOnTargetObj.name);

        return LockOnTargetObj;
    }

    public void NearEnemyTargeReset()
    {
        LockOnTargetIndex = 0;
        LockOnTargetObjs = null;

    }

    #endregion

}
