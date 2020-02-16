using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
public class InputHandler : MonoBehaviour
{
    //private serialized
    [SerializeField] private int controllerID = 0;

    //private
    private bool canOnlyMoveCamera = false;
    private Player player;
    
    private KeyCode jumpCode;
    private KeyCode interact;
    private KeyCode deInteract;
    private KeyCode startButton;

    private void Start()
    {
        //finds the character script.
        if (GetComponent<Man>())
        {
            player = GetComponent<Man>();
        }

        if (GetComponent<Women>())
        {
            player = GetComponent<Women>();
        }
        ConfigureControlButtons();

    }
    public void ConfigureControlButtons()
    {
        //controller identification for the buttons
        switch (controllerID)
        {
            case 1:
                //jumpCode = KeyCode.Joystick1Button0;
                interact = KeyCode.Joystick1Button0;
                deInteract = KeyCode.Joystick1Button1;
                startButton = KeyCode.Joystick1Button7;
                break;
            case 2:
                //jumpCode = KeyCode.Joystick2Button0;
                interact = KeyCode.Joystick2Button0;
                deInteract = KeyCode.Joystick2Button1;
                startButton = KeyCode.Joystick2Button7;
                break;
            default:
                jumpCode = KeyCode.Space;
                startButton = KeyCode.Escape;
                break;
        }
    }
    private void Update()
    {
        CheckInput();
    }
    public int ControllerID
    {
        get { return controllerID; }
        set
        {
            controllerID = value;
            ConfigureControlButtons();
        }
    }
    public bool CanOnlyMoveCamera
    {
        get { return canOnlyMoveCamera; }
        set
        {
            canOnlyMoveCamera = value;
        }
    }
    public void CheckInput()
    {
        if (Input.GetKeyDown(startButton))
        {
            //GameManager.instance.Pause(Time.timeScale == 1);
        }

        if (Time.timeScale == 0) { return; }
        //player view change
        //character.Rotate(
            //CrossPlatformInputManager.GetAxis("RotateHorizontal" + controllerID),
            //CrossPlatformInputManager.GetAxis("RotateVertical" + controllerID)
            //);

        if (canOnlyMoveCamera) { return; }

        if (Input.GetKeyDown(interact))
        {
            player.Interact();
        }

        if (Input.GetKeyDown(deInteract))
        {
            player.Deinteract();
        }

        if (Input.GetKeyDown(startButton))
        {
            PauseMenu.Instance.Toggle();
        }

        //player movement
        player.Walking(Input.GetAxis("Horizontal" + controllerID), Input.GetAxis("Vertical" + controllerID));
    }

    #region Singleton
    #endregion
}
