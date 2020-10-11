using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;
public class H_PlayerMng : MonoBehaviour
{
    H_MinigameInputHandler inputHandler;
    //Animator anim;    애니메이션 관련
    public bool isInteracting;

    Sc_CameraHandler cameraHandler;
    H_PlayerLocomotion H_Player;

    private void Awake()
    {
        cameraHandler = FindObjectOfType<Sc_CameraHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<H_MinigameInputHandler>();
        //anim = GetComponentInChildren<Animator>();
        H_Player = GetComponent<H_PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        //isInteracting = anim.GetBool("isInteracting");
        //canDoCombo = anim.GetBool("canDoCombo");

        inputHandler.TickInput(delta);
        H_Player.HandleMovement(delta);
        //sc_Player.HandleRollingAndSprinting(delta);
        //sc_Player.HandleFalling(delta, sc_Player.moveDirection);

    }

    private void LateUpdate()
    {
        //@Test
        inputHandler.fire_Input = false;
        inputHandler.menu_Input = false;
        inputHandler.testF_Input = false;
        //inputHandler.anykey_Input = false;
        /*inputHandler.testG_Input = false;
        inputHandler.testR_Input = false;
        inputHandler.testQ_Input = false;
        inputHandler.testE_Input = false;
        inputHandler.testV_Input = false;
        inputHandler.testUp_Input = false;
        inputHandler.testDown_Input = false;
        inputHandler.testZ_Input = false;*/
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }
    }
}
