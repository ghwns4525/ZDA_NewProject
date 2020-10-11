using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    Door[] theDoor;
    // Start is called before the first frame update
    void Start()
    {
        theDoor = FindObjectsOfType<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            for(int i = 0; i < theDoor.Length; i++)
            {
                if (theDoor[i].Trigger.Contains(this.gameObject))
                {
                    theDoor[i].Trigger.Remove(this.gameObject);
                }
            }
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

    public void Restart()
    {
        this.gameObject.SetActive(true);
    }
}
