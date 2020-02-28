using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisable : Interactable
{
    //private serialized
    [SerializeField] private GameObject UIToDisable;
    [SerializeField] private bool manCanDisable;
    [SerializeField] private bool womanCanDisable;

    private void Update()
    {
        if (manCollding)
        {
            if (man.interact)
            {
                if (manCanDisable)
                {
                    UIToDisable.gameObject.SetActive(false);
                }
            }
        }
        if (womanCollding)
        {
            if (woman.interact)
            {
                if (womanCanDisable)
                {
                    UIToDisable.gameObject.SetActive(false);
                }
            }
        }
    }
}
