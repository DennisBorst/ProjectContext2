using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisable : Interactable
{
    //private serialized
    [SerializeField] private GameObject UIToDisable;

    private void Update()
    {
        if (manCollding)
        {
            if (man.interact)
            {
                UIToDisable.gameObject.SetActive(false);
            }
        }
        if (womanCollding)
        {
            if (woman.interact)
            {
                UIToDisable.gameObject.SetActive(false);
            }
        }
    }
}
