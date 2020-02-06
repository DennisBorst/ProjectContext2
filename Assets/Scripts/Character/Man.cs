using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : Player
{
    private bool pulling;
    private float x_input;
    private float z_input;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
    public override void Input()
    {
        base.Input();
    }
    public override void Interact()
    {
        //Does some interaction
        Debug.Log("Interact");
        interact = true;
    }

    public override void Deinteract()
    {
        base.Deinteract();
        deinteract = true;
        
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
