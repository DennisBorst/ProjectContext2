using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : Interactable
{
    [SerializeField] private GameObject manCanvas;
    [SerializeField] private GameObject womanCanvas;

    private bool collidingHere;
    private bool manReading = false;
    private bool womanReading = false;

    private void Start()
    {
        manCanvas.SetActive(false);
        womanCanvas.SetActive(false);
    }

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
                    man.animationPlaying = true;
                    man.SetAnimation("isWalking", false);
                    //man.ResetCharacter(-1);
                }

                if (man.deinteract)
                {
                    manReading = false;
                    manCanvas.SetActive(false);
                    man.animationPlaying = false;
                    //man.ResetCharacter(1);
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
                    woman.animationPlaying = true;
                    woman.SetAnimation("isWalking", false);
                    //woman.ResetCharacter(-1);
                }

                if (woman.deinteract)
                {
                    womanReading = false;
                    womanCanvas.SetActive(false);
                    woman.animationPlaying = false;
                    //woman.ResetCharacter(1);
                }
            }
        }
    }
}
