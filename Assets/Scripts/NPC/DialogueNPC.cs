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
    private float damping = 5f;
    private int currentTextNumber;
    private bool interactingMan;
    private bool interactingWoman;
    private bool womanInRange;
    private Vector3 lookPos;
    private TextMeshProUGUI dialogueTextUI;
    private Camera mainCamera;
    private Player talkingPlayer;
    private List<Player> player = new List<Player>();

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
        TurnCharacter();

        if (player != null && talkingPlayer == null)
        {
            for (int i = 0; i < player.Count; i++)
            {
                if (player[i].interact)
                {
                    talkingPlayer = player[i];
                    //fsm.SwitchState(StateEnum.Talk);
                }
            }
        }
    }

    private void InteractingMan()
    {
        if (manCollding && talkingPlayer == null)
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
                talkingPlayer = null;
                currentTextNumber = 0;
            }
        }
    }

    private void InteractingWoman()
    {
        if (womanCollding && talkingPlayer == null)
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
                talkingPlayer = null;
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
        if(player != null)
        {
            if(talkingPlayer != null)
            {
                //transform.LookAt(talkingPlayer.transform);
                lookPos = talkingPlayer.transform.position - transform.position;
            }
            else
            {
                //transform.LookAt(player[0].transform);
                lookPos = player[0].transform.position - transform.position;
            }

            Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
            float eulerY = lookRot.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.GetComponent<Women>())
            {
                player.Add(collider.GetComponent<Women>());
            }

            if (collider.GetComponent<Man>())
            {
                player.Add(collider.GetComponent<Man>());
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<Women>())
        {
            player.Remove(collider.gameObject.GetComponent<Women>());
        }

        if (collider.gameObject.GetComponent<Man>())
        {
            player.Remove(collider.gameObject.GetComponent<Man>());
        }
    }
}
