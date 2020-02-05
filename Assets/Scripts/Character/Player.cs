using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //private
    private float currentMovementSpeed;
    private float interactTime = 0.3f;
    private float deinteractTime = 0.3f;
    private float currentInteractTimer;

    private Rigidbody rb;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentInteractTimer = interactTime;

        currentMovementSpeed = movementSpeed;
    }
    public virtual void Update()
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
    public virtual void Walking(float x_input, float z_input)
    {
        if(currentMovementSpeed != 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.velocity += transform.right * x_input * currentMovementSpeed;
            rb.velocity += transform.forward * z_input * currentMovementSpeed;

            if (x_input > 0.5f && z_input < 0.5f && z_input > -0.5f) //going to the right
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y + 90, 0);
            }
            else if (x_input < -0.5f && z_input < 0.5f && z_input > -0.5f) //going to the left
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y - 90, 0);
            }
            else if (x_input < 0.5f && x_input > -0.5f && z_input > 0.5f) //going to the up
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y, 0);
            }
            else if (x_input < 0.5f && x_input > -0.5f && z_input < -0.5f) //going to the down
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y - 180, 0);
            }
            else if (x_input > 0.5f && z_input > 0.5f) //going right and up
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y + 45, 0);
            }
            else if (x_input < -0.5f && z_input < -0.5f) //going left and down
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y - 135, 0);
            }
            else if (x_input < -0.5f && z_input > 0.5f) //going left and up
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y - 45, 0);
            }
            else if (x_input > 0.5f && z_input < -0.5f) //going right and down
            {
                playerObject.transform.rotation = Quaternion.Euler(0, playerObject.transform.position.y + 135, 0);
            }
        }
    }
    public virtual void Interact()
    {
        //Interaction
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
