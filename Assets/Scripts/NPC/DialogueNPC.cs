using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueNPC : Interactable
{
    //private serialized
    [SerializeField] private string[] dialogueText;
    [SerializeField] private GameObject canvasDialogue;

    //private
    private int currentTextNumber;
    private bool interactingMan;
    private bool interactingWoman;
    private bool womanInRange;
    private TextMeshProUGUI dialogueTextUI;
    private Camera mainCamera;

    private void Start()
    {
        dialogueTextUI = GetComponentInChildren<TextMeshProUGUI>();
        mainCamera = FindObjectOfType<Camera>();

        canvasDialogue.SetActive(false);
        dialogueTextUI.text = "";
        currentTextNumber = 0;
    }

    private void Update()
    {
        InteractingMan();
        InteractingWoman();
        TurnCanvas();

        if(!interactingMan && !interactingWoman)
        {
            TurnCharacter();
        }
    }

    private void InteractingMan()
    {
        if (manCollding && !interactingWoman)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                if(currentTextNumber == dialogueText.Length)
                {
                    interactingMan = false;
                }
                else
                {
                    interactingMan = true;
                    dialogueTextUI.text = dialogueText[currentTextNumber];
                    currentTextNumber++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                interactingMan = false;
            }

            if (interactingMan)
            {
                man.ResetCharacter(0);
                transform.LookAt(new Vector3(transform.rotation.x ,man.transform.rotation.y, transform.rotation.z));
                canvasDialogue.SetActive(true);
            }
            else
            {
                man.ResetCharacter(man.movementSpeed);
                canvasDialogue.SetActive(false);
                currentTextNumber = 0;
            }
        }
    }

    private void InteractingWoman()
    {
        if (womanCollding && !interactingMan)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                if (currentTextNumber == dialogueText.Length)
                {
                    interactingWoman = false;
                }
                else
                {
                    interactingWoman = true;
                    dialogueTextUI.text = dialogueText[currentTextNumber];
                    currentTextNumber++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button1))
            {
                interactingWoman = false;
            }

            if (interactingWoman)
            {
                woman.ResetCharacter(0);
                transform.LookAt(new Vector3(transform.rotation.x, woman.transform.rotation.y, transform.rotation.z));
                canvasDialogue.SetActive(true);
            }
            else
            {
                woman.ResetCharacter(woman.movementSpeed);
                canvasDialogue.SetActive(false);
                currentTextNumber = 0;
            }
        }
    }

    private void TurnCanvas()
    {
        canvasDialogue.transform.rotation = mainCamera.transform.rotation;
    }

    private void TurnCharacter()
    {
        if (manCollding && !womanInRange)
        {
            transform.LookAt(man.transform);
        }
        else if (womanCollding && womanInRange)
        {
            transform.LookAt(woman.transform);
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if (womanCollding)
        {
            if (!manCollding)
            {
                womanInRange = true;
            }
        }
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
        if (woman)
        {
            womanInRange = false;
        }
    }
}
