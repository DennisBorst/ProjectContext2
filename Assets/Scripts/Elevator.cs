using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Interactable
{
    private bool readyToPull;
    private Vector2 pullDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (manCollding && man.interact)
        {
            readyToPull = true;
        }
        if (man.deinteract)
        {
            readyToPull = false;
        }

        if (readyToPull)
        {
            man.ResetCharacter(-1);
        }
    }

    private void Pulling(Vector2 pullDirection)
    {

        //return 
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }
    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }
    public override void SwitchBool(bool man)
    {
        base.SwitchBool(man);
    }
}
