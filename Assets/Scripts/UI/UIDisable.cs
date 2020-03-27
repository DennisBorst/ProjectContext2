using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisable : Interactable
{
    //private serialized
    [SerializeField] private GameObject UIToDisable;
    [SerializeField] private float timeBeforeDisappear;
    [SerializeField] private bool manCanDisable;
    [SerializeField] private bool womanCanDisable;

    private bool actived;

    private void Update()
    {
        if (manCanDisable)
        {
            if (manCollding)
            {
                if (man.interact)
                {
                    actived = true;
                }
            }
        }

        if (womanCanDisable)
        {
            if (womanCollding)
            {
                if (woman.interact)
                {

                    actived = true;
                }
            }
        }

        if (actived)
        {
            timeBeforeDisappear = Timer(timeBeforeDisappear);
            if(timeBeforeDisappear < 0)
            {
                UIToDisable.gameObject.SetActive(false);
            }
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
