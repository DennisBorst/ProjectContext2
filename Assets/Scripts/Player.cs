using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private serialized
    [SerializeField] private float movementSpeed;

    //private
    private float inputX;
    private float inputZ;
    private Rigidbody rb;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Walking(float x_input, float z_input)
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        rb.velocity += transform.right * x_input * movementSpeed;
        rb.velocity += transform.forward * z_input * movementSpeed;
    }

    public virtual void Interact()
    {
        //Interaction
    }

    public virtual void Deinteract()
    {
        //Deinteract
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
