using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : Player
{
    public bool interact { get; set; }
    private float interactTime = 0.3f;
    private float currentInteractTimer;

    public override void Start()
    {
        base.Start();
        currentInteractTimer = interactTime;
    }

    void Update()
    {
        if (interact)
        {
            currentInteractTimer = Timer(currentInteractTimer);

            if(currentInteractTimer <= 0)
            {
                interact = false;
                currentInteractTimer = interactTime;
            }
        }
    }
    public override void Interact()
    {
        //Does some interaction
        Debug.Log("Interact");
        interact = true;

    }
    public override void Walking(float x_input, float z_input)
    {
        base.Walking(x_input, z_input);
    }
    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }

    #region Singleton
    private static Man instance;
    private void Awake()
    {
        instance = this;    
    }

    public static Man Instance
    {
        get
        {
            if(instance != null)
            {
                instance = new Man();
            }

            return instance;
        }
    }
    #endregion
}
