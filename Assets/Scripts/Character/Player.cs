using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    //public
    public float movementSpeed;
    public bool interact;
    public bool deinteract;
    public bool funInteract;
    public bool animationPlaying;
    public float inputX { get; set; }
    public float inputZ { get; set; }
    public GameObject playerObject;

    //private serialized
    [SerializeField] private Camera camera;
    [SerializeField] private AudioClip[] walkingSounds;
    [SerializeField] private Transform cameraObject;

    //private
    private float currentMovementSpeed;
    private float interactTime = 0.2f;
    private float deinteractTime = 0.2f;
    private float currentInteractTimer;
    private float walkingSoundTime = 0.5f;
    private float currentSoundWalkingTimer;
    private float cameraY;
    private float lookDirectionY;
    private Vector2 lookDirection;
    private Rigidbody rb;
    private Animator anim;
    private AudioSource source;

    //FMOD
    [FMODUnity.EventRef]
    public string walkingEvent;

    FMOD.Studio.EventInstance walkingInstance;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();

        currentInteractTimer = interactTime;
        currentSoundWalkingTimer = walkingSoundTime;

        currentMovementSpeed = movementSpeed;

        walkingInstance = FMODUnity.RuntimeManager.CreateInstance(walkingEvent);
    }
    public virtual void Update()
    {
        if (!animationPlaying)
        {
            cameraY = camera.transform.eulerAngles.y;
            this.transform.rotation = Quaternion.Euler(transform.rotation.x, cameraY, transform.rotation.z);
            Input();
        }
    }
    public virtual void Walking(float x_input, float z_input)
    {
        inputX = x_input;
        inputZ = z_input;

        if (animationPlaying)
        {
            rb.isKinematic = true;
            SetAnimation("isWalking", false);
            GetComponent<FMODUnity.StudioEventEmitter>().Stop();
            return;
        }
        else
        {
            rb.isKinematic = false;
        }

        if (currentMovementSpeed == 0)
        {
            return;
        }

        //Moving part
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        rb.velocity += transform.right * x_input * currentMovementSpeed;
        rb.velocity += transform.forward * z_input * currentMovementSpeed;

        //Rotation Character
        lookDirection.x = x_input * 90;
        lookDirection.y = (z_input * -90) + 90;
            
        if (z_input <= -0.9 && x_input >= -0.9 && x_input <= 0.9)
        {
            lookDirectionY = 0 + (cameraY - 180);
        }
        else if (x_input > 0 || z_input > 0)
        {
            lookDirectionY = (lookDirection.x + lookDirection.y) / 2 - 180 + (cameraY - 180);
        }
        else if(x_input < 0 || z_input < 0)
        {
            lookDirectionY = (lookDirection.x - lookDirection.y) / 2 - 180 + (cameraY - 180);
        }

        playerObject.transform.rotation = Quaternion.Euler(0, lookDirectionY, 0);

        //Sounds & Animatie
        if (inputX > 0.5f || inputZ > 0.5f || inputX < -0.5f || inputZ < -0.5f && !animationPlaying)
        {
            if (anim != null)
            {
                SetAnimation("isWalking", true);
            }
            if (!GetComponent<FMODUnity.StudioEventEmitter>().IsPlaying())
            {
                GetComponent<FMODUnity.StudioEventEmitter>().Play();

            }
        }
        else
        {
            if (anim != null)
            {
                SetAnimation("isWalking", false);
            }
            GetComponent<FMODUnity.StudioEventEmitter>().Stop();
        }
    }
    public virtual void Input()
    {
        if (interact)
        {
            currentInteractTimer = Timer(currentInteractTimer);

            if (currentInteractTimer <= 0)
            {
                interact = false;
                currentInteractTimer = interactTime;
            }
        }
        else if (deinteract)
        {
            currentInteractTimer = Timer(currentInteractTimer);

            if (currentInteractTimer <= 0)
            {
                deinteract = false;
                currentInteractTimer = interactTime;
            }
        }
        else if (funInteract)
        {
            currentInteractTimer = Timer(currentInteractTimer);

            if (currentInteractTimer <= 0)
            {
                funInteract = false;
                currentInteractTimer = interactTime;
            }
        }
    }
    public virtual void Interact()
    {
        //interact
    }
    public virtual void Deinteract()
    {
        //Deinteract
    }
    public virtual void FunInteraction()
    {
        //Funinteract
    }

    public void ResetCharacter(float movementSpeedChange)
    {
        if(movementSpeedChange < 0)
        {
            currentMovementSpeed = 0;
        }
        else
        {
            currentMovementSpeed = movementSpeedChange;
        }
    }

    public void SetAnimation(string state, bool val)
    {
        if(state != null && val != null)
        {
            anim.SetBool(state, val);
        }
    }
    public IEnumerator SetanimationBoolFalse(string val, float duration)
    {
        animationPlaying = true;
        currentMovementSpeed = 0;
        yield return new WaitForSeconds(duration);
        currentMovementSpeed = movementSpeed;
        animationPlaying = false;
        anim.SetBool(val, false);
        Debug.Log(val);
    }
    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
