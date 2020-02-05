using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : Interactable
{
    [SerializeField] private GameObject manCanvas;
    [SerializeField] private GameObject womanCanvas;

    private bool collidingHere;
    private bool manReading;
    private bool womanReading;

    private void Update()
    {
        if (!womanReading)
        {
            if (manCollding)
            {
                if (man.interact)
                {
                    manReading = true;
                    manCanvas.SetActive(true);
                    man.ResetCharacter(-1);
                }
                else if (man.deinteract)
                {
                    manReading = false;
                    manCanvas.SetActive(false);
                    man.ResetCharacter(1);
                }
            }
        }

        if (!manReading)
        {
            if (womanCollding)
            {
                if (woman.interact)
                {
                    womanReading = true;
                    womanCanvas.SetActive(true);
                    woman.ResetCharacter(-1);
                }
                else if (woman.deinteract)
                {
                    womanReading = false;
                    womanCanvas.SetActive(false);
                    woman.ResetCharacter(1);
                }
            }
        }
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
