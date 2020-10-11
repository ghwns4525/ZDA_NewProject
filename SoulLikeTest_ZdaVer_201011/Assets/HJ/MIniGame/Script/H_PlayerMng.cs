using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;
public class H_PlayerMng : MonoBehaviour
{
    Sc_InputHandler inputHandler;
    //Animator anim;    애니메이션 관련
    public bool isInteracting;
    [Header("Player_Flag")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGround;
    public bool canDoCombo;

    Sc_CameraHandler cameraHandler;
    H_PlayerLocomotion sc_Player;

    private void Awake()
    {
        cameraHandler = FindObjectOfType<Sc_CameraHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<Sc_InputHandler>();
        //anim = GetComponentInChildren<Animator>();
        sc_Player = GetComponent<H_PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        //isInteracting = anim.GetBool("isInteracting");
        //canDoCombo = anim.GetBool("canDoCombo");

        inputHandler.TickInput(delta);
        sc_Player.HandleMovement(delta);
        //sc_Player.HandleRollingAndSprinting(delta);
        //sc_Player.HandleFalling(delta, sc_Player.moveDirection);

    }

    private void LateUpdate()
    {
        //@Test
       /* inputHandler.fire_Input = false;
        inputHandler.menu_Input = false;
        inputHandler.anykey_Input = false;
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;
        inputHandler.testG_Input = false;
        inputHandler.testF_Input = false;
        inputHandler.testR_Input = false;
        inputHandler.testQ_Input = false;
        inputHandler.testE_Input = false;
        inputHandler.testV_Input = false;
        inputHandler.testUp_Input = false;
        inputHandler.testDown_Input = false;
        inputHandler.testZ_Input = false;*/

        inputHandler.rollFlag = false;
        inputHandler.sprintFlag = false;


        if (isInAir) // 공중이라면
        {
            sc_Player.inAirTimer = sc_Player.inAirTimer + Time.deltaTime;   // 공중 체공시간 + 시간
        }
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
