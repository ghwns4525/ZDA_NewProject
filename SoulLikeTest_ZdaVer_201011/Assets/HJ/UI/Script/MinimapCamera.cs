using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{


    public class MinimapCamera : MonoBehaviour
    {
        [SerializeField] Transform playerTr;
        float moveSpeed = 5f;
        [SerializeField]float rotSpeed = 10f;
        float h = 0;
        float lookAngle;

        Sc_InputHandler InputHandler;
        Sc_CameraHandler sc_cameraHandler;


        // 회전도 있어야함

        // Start is called before the first frame update
        void Start()
        {
            InputHandler = FindObjectOfType<Sc_InputHandler>();
            sc_cameraHandler = FindObjectOfType<Sc_CameraHandler>();
        }

        // Update is called once per frame
        void Update()
        {
            // 플레이어 추적
            transform.position = Vector3.Lerp(transform.position, playerTr.position + new Vector3(0, 30, 0), Time.deltaTime * moveSpeed);
            //transform.rotation = Quaternion.Euler(90f, playerTr.eulerAngles.y, 0f);

            // 마우스 회전에 맞춤 
            Quaternion cameraHolder = sc_cameraHandler.GetCameraHolderRotation();
            //h = transform.rotation.z;
            /*Vector3 moveDir = Vector3.right * h;
            transform.Translate(moveDir * Time.deltaTime * 2, Space.Self);*/
            /*transform.Rotate(Vector3.back * Time.deltaTime * rotSpeed * InputHandler.mouseX);
            Debug.Log(InputHandler.mouseX);*/
            //Debug.Log("xxxxxxxxxxxxxxxxxx :: "+ InputHandler.mouseX);
            //transform.Rotate(Vector3.back * Time.deltaTime * rotSpeed * cameraHolder.y);
            //transform.Rotate(new Vector3(0, 0,) );


            transform.rotation = cameraHolder;


            //transform.rotation = Quaternion.LookRotation(cameraPivot.eulerAngles);
        }
    }
}
