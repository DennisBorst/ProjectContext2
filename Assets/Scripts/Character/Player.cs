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
    public float inputX { get; set; }
    public float inputZ { get; set; }

    //private serialized
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Camera camera;

    //private
    private float currentMovementSpeed;
    private float interactTime = 0.2f;
    private float deinteractTime = 0.2f;
    private float currentInteractTimer;
    private float cameraY;
    private float lookDirectionY;
    private Vector2 lookDirection;


    private Rigidbody rb;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentInteractTimer = interactTime;
        
        currentMovementSpeed = movementSpeed;
        //camera = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
        //camera = camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().transform.rotation;
    }
    public virtual void Update()
    {
        cameraY = camera.transform.rotation.y * 200;
        this.transform.rotation = Quaternion.Euler(transform.rotation.x, cameraY, transform.rotation.z);
        Input();
    }
    public virtual void Walking(float x_input, float z_input)
    {
        inputX = x_input;
        inputZ = z_input;

        if(currentMovementSpeed != 0)
        {
            
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.velocity += transform.right * x_input * currentMovementSpeed;
            rb.velocity += transform.forward * z_input * currentMovementSpeed;


            lookDirection.x = inputX * 90;
            lookDirection.y = (inputZ * -90) + 90;
            if (inputZ <= -0.9 && inputX >= -0.9 && inputX <= 0.9)
            {
                lookDirectionY = 0 + (cameraY - 180);
            }
            else if (inputX > 0 || inputZ > 0)
            {
                lookDirectionY = (lookDirection.x + lookDirection.y) / 2 - 180 + (cameraY - 180);
            }
            else if(inputX < 0 || inputZ < 0)
            {
                lookDirectionY = (lookDirection.x - lookDirection.y) / 2 - 180 + (cameraY - 180);
            }
            playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y + lookDirectionY, 0);

            /*
            if (x_input > 0.5f && z_input < 0.5f && z_input > -0.5f) //going to the right
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y - 90, 0);
            }
            else if (x_input < -0.5f && z_input < 0.5f && z_input > -0.5f) //going to the left
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y + 90, 0);
            }
            else if (x_input < 0.5f && x_input > -0.5f && z_input > 0.5f) //going to the up
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y - 180, 0);
            }
            else if (x_input < 0.5f && x_input > -0.5f && z_input < -0.5f) //going to the down
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y, 0);
            }
            else if (x_input > 0.5f && z_input > 0.5f) //going right and up
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y - 135, 0);
            }
            else if (x_input < -0.5f && z_input < -0.5f) //going left and down
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y + 45, 0);
            }
            else if (x_input < -0.5f && z_input > 0.5f) //going left and up
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y + 135, 0);
            }
            else if (x_input > 0.5f && z_input < -0.5f) //going right and down
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y - 45, 0);
            }
            */
            
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
    }
    public virtual void Interact()
    {
        //interact
    }
    public virtual void Deinteract()
    {
        //Deinteract
    }
    public virtual void Jump()
    {

    }
    public void ResetCharacter(int movementSpeedChange)
    {
        if(movementSpeedChange < 0)
        {
            currentMovementSpeed = 0;
        }
        else
        {
            currentMovementSpeed = movementSpeed;
        }
    }
    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
