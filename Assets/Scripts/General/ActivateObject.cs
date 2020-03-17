using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : Interactable
{
    [SerializeField] private GameObject activateObject;
    [SerializeField] private bool manHasToTrigger;
    [SerializeField] private bool womanHasToTrigger;

    private void Update()
    {
        if(manHasToTrigger && womanHasToTrigger)
        {
            if(manCollding && womanCollding)
            {
                activateObject.SetActive(true);
            }
        }
        else if (manHasToTrigger)
        {
            if (manCollding)
            {
                activateObject.SetActive(true);
            }
        }
        else if (womanHasToTrigger)
        {
            if (womanCollding)
            {
                activateObject.SetActive(true);
            }
        }
    }
}
