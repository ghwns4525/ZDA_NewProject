using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretText : MonoBehaviour
{
    Text txt_Turret;
    public int currentTurret = 0;
    int maxTurret = 0;

    Door[] theDoor;   // 스크립트 참조

    // Start is called before the first frame update
    void Start()
    {
        txt_Turret = GetComponent<Text>();
        theDoor = FindObjectsOfType<Door>();
        for (int i = 0; i < theDoor.Length; i++)
        {
            maxTurret += theDoor[i].Turret.Count;
        }
        currentTurret = maxTurret;
    }

    // Update is called once per frame
    void Update()
    {
        txt_Turret.text = string.Format("{0:#,##0}", currentTurret);
    }

    public void Restart()
    {
        currentTurret = maxTurret;
    }
}
