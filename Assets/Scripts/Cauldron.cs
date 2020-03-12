using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Interactable
{


    void Update()
    {
        if (womanCollding)
        {
            if (woman.interact)
            {
                woman.animationPlaying = true;
                woman.transform.LookAt(this.gameObject.transform);
            }

            if (woman.deinteract)
            {
                woman.animationPlaying = false;
            }
        }
    }
}
