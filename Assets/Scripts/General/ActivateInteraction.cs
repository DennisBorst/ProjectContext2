using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInteraction : Interactable
{
    [SerializeField] private GameObject setObjectActive;

    [SerializeField] private bool manCanActivate;
    [SerializeField] private bool womanCanActivate;

    private void Update()
    {
        if (manCanActivate)
        {
            if (manCollding)
            {
                if (man.interact)
                {
                    setObjectActive.SetActive(true);
                }
            }
        }
        else if (womanCanActivate)
        {
            if (womanCollding)
            {
                if (woman.interact)
                {
                    setObjectActive.SetActive(true);
                }
            }
        }
    }
}
