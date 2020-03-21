using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmNPC : Interactable
{
    //private serialized
    [SerializeField] private string[] dialogueTextMan;
    [SerializeField] private string[] dialogueTextWoman;
    [SerializeField] private Transform teleportMan;
    [SerializeField] private Transform teleportWoman;
    [SerializeField] private GameObject canvasDialogue;
    [SerializeField] private GameObject farmIcons;
    [SerializeField] private Animator anim;

    //private
    private float damping = 5f;
    private int currentTextNumber;
    private bool interactingMan;
    private bool interactingWoman;
    private bool womanInRange;
    private Vector3 lookPos;
    private TextMeshProUGUI dialogueTextUI;
    private Camera mainCamera;
    private Player manScript;
    private Player womanScript;
    private Player talkingPlayer;
    private List<Player> player = new List<Player>();

    private void Start()
    {
        dialogueTextUI = canvasDialogue.GetComponentInChildren<TextMeshProUGUI>();
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
    }

    private void InteractingMan()
    {
        if (manCollding && !interactingWoman)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                if (currentTextNumber == dialogueTextMan.Length)
                {
                    interactingMan = false;
                    anim.SetTrigger("FadeOut");
                    StartCoroutine(Fade());
                }
                else
                {
                    interactingMan = true;
                    dialogueTextUI.text = dialogueTextMan[currentTextNumber];
                    currentTextNumber++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                interactingMan = false;
            }

            if (interactingMan)
            {
                man.animationPlaying = true;
                man.SetAnimation("isWalking", false);
                talkingPlayer = manScript;
                canvasDialogue.SetActive(true);
            }
            else
            {
                man.animationPlaying = false;
                canvasDialogue.SetActive(false);
                talkingPlayer = null;
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

                if (currentTextNumber == dialogueTextWoman.Length)
                {
                    interactingWoman = false;
                    anim.SetTrigger("FadeOut");
                    StartCoroutine(Fade());
                }
                else
                {
                    interactingWoman = true;
                    dialogueTextUI.text = dialogueTextWoman[currentTextNumber];
                    currentTextNumber++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button1))
            {
                interactingWoman = false;
            }

            if (interactingWoman)
            {
                woman.animationPlaying = true;
                woman.SetAnimation("isWalking", false);
                talkingPlayer = womanScript;
                canvasDialogue.SetActive(true);
            }
            else
            {
                woman.animationPlaying = false;
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
        if(manScript != null || womanScript != null)
        {
            if(talkingPlayer != null)
            {
                lookPos = talkingPlayer.transform.position - transform.position;
            }
            else
            {
                lookPos = player[0].transform.position - transform.position;
            }

            Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
            float eulerY = lookRot.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1f);
        man.transform.position = teleportMan.position;
        woman.transform.position = teleportWoman.position;
        yield return new WaitForSeconds(1f);
        farmIcons.SetActive(true);
        anim.SetTrigger("FadeIn");
    }

    private void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (collider.gameObject.tag == "Player")
        {
            if (collider.GetComponent<Woman>())
            {
                womanScript = collider.GetComponent<Woman>();
                player.Add(collider.GetComponent<Woman>());
            }

            if (collider.GetComponent<Man>())
            {
                manScript = collider.GetComponent<Man>();
                player.Add(collider.GetComponent<Man>());
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
        if (collider.gameObject.GetComponent<Woman>())
        {
            womanScript = null;
            player.Remove(collider.GetComponent<Woman>());
        }

        if (collider.gameObject.GetComponent<Man>())
        {
            manScript = null;
            player.Remove(collider.GetComponent<Man>());
        }
    }
}
