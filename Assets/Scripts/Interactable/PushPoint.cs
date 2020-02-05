using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPoint : Interactable
{
    [SerializeField] private GameObject manReadyCanvas;
    [SerializeField] private Transform teleportPoint;

    private bool manReady;

    private void Update()
    {
        if (manCollding)
        {
            if (man.interact)
            {
                man.ResetCharacter(-1);
                manReady = true;
                manReadyCanvas.SetActive(true);
                Debug.Log("Standing next to the wall");
            }
            else if (man.deinteract)
            {
                man.ResetCharacter(1);
                manReady = false;
                manReadyCanvas.SetActive(false);
                Debug.Log("Walking away from the wall");
            }
        }

        if (manReady)
        {
            if (womanCollding)
            {
                if (woman.interact)
                {
                    Debug.Log("Teleporting");
                    man.ResetCharacter(1);
                    manReady = false;
                    manReadyCanvas.SetActive(false);
                    woman.transform.position = teleportPoint.transform.position;
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
