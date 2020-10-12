using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class H_MinigameInputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    /// <summary>
    /// 운동량
    /// </summary>
    public float moveAmount;
    public float mouseX;
    public float mouseY;
    /// <summary>
    /// Shit Key :: 롤링 버튼
    /// </summary>
    public bool b_Input;
    public bool fire_Input;
    public bool menu_Input;
    public bool anykey_Input;

    public bool testG_Input;
    public bool testF_Input;
    public bool testR_Input;
    public bool testQ_Input;
    public bool testE_Input;
    public bool testV_Input;
    public bool testUp_Input;
    public bool testDown_Input;
    public bool testZ_Input;

    public bool fireFlag;


    PlayerCtrl inputActions;
    Sc_PlayerAttacker playerAttacker;
    Sc_PlayerInventory playerInventory;
    Sc_PlayerManager playerManager;



    Vector2 movementInput;
    Vector2 cameraInput;


    private void Awake()
    {
        playerAttacker = GetComponent<Sc_PlayerAttacker>();
        playerInventory = GetComponent<Sc_PlayerInventory>();
        playerManager = GetComponent<Sc_PlayerManager>();
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerCtrl();
            inputActions.PlayerMovement.Movement.performed += (inputActions) =>
            movementInput = inputActions.ReadValue<Vector2>();

            inputActions.PlayerMovement.Camera.performed += (i) => cameraInput = i.ReadValue<Vector2>();
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
        FireInput(delta);
        MenuInput(delta);
        TestFKeyInput(delta);
        TestGKeyInput(delta);
        TestQKeyInput(delta);
        TestEKeyInput(delta);
        TestRKeyInput(delta);
        TestUpKeyInput(delta);
        TestDownKeyInput(delta);
        TestAnyKeyInput(delta);
        /*        
                
                
                
                TestVKeyInput(delta);
                
                TestZKeyInput(delta);*/
    }
    public void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    public void FireInput(float delta)
    {

        if (inputActions.PlayerActions.RB.phase == UnityEngine.InputSystem.InputActionPhase.Started)
        {
            fire_Input = true;
            fireFlag = true;
            Debug.Log(fire_Input);
        }
        else if (inputActions.PlayerActions.RB.phase == UnityEngine.InputSystem.InputActionPhase.Waiting)
        {
            fire_Input = false;
            fireFlag = false;
        }
        //inputActions.PlayerActions.RB.performed += i => rb_Input = true;


    }

    public void MenuInput(float delta)
    {
        inputActions.UI.Menu.performed += i => menu_Input = true;
    }

    public void TestFKeyInput(float delta)
    {
        inputActions.UI.Test_F.performed += i => testF_Input = true;
    }
    public void TestGKeyInput(float delta)
    {
        inputActions.UI.Test_G.performed += i => testG_Input = true;
    }
    public void TestQKeyInput(float delta)
    {
        inputActions.UI.Test_Q.performed += i => testQ_Input = true;
    }
    public void TestEKeyInput(float delta)
    {
        inputActions.UI.Test_E.performed += i => testE_Input = true;
    }
    public void TestRKeyInput(float delta)
    {
        inputActions.UI.Test_R.performed += i => testR_Input = true;
    }
    public void TestUpKeyInput(float delta)
    {
        inputActions.UI.Test_Up.performed += i => testUp_Input = true;
    }
    public void TestDownKeyInput(float delta)
    {
        inputActions.UI.Test_Down.performed += i => testDown_Input = true;
    }
        
    public void TestAnyKeyInput(float delta)    
    {
        inputActions.UI.Test_Anykey.performed += i => anykey_Input = true;        
    }

    // UI Test용
    /*




        

        public void TestVKeyInput(float delta)
        {
            inputActions.Test.Test_V.performed += i => testV_Input = true;
        }
        
        public void TestZKeyInput(float delta)
        {
            inputActions.Test.Test_Z.performed += i => testZ_Input = true;
        }*/
}
