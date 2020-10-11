using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    public bool isClear = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isClear = true;
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

    public void Restart()
    {
        this.gameObject.SetActive(true);
    }
}
