using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Interactable
{
    private Animator anim;
    private AudioSource source;

    //FMOD
    //[FMODUnity.EventRef]
    //public string fallingPlankSFX;

    //FMOD.Studio.EventInstance fallingState;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (womanCollding)
        {
            if (woman.interact)
            {
                //fallingState = FMODUnity.RuntimeManager.CreateInstance(fallingPlankSFX);
                //fallingState.start();
                anim.SetTrigger("BridgeOpen");
            }
        }
    }
}
