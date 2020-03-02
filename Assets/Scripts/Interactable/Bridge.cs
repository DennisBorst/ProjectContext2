using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Interactable
{
    private Animator anim;
    private AudioSource source;

    //FMOD
    [FMODUnity.EventRef]
    public string fallingPlankSFX;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        FMODUnity.RuntimeManager.PlayOneShot("event:/PlankBridge", transform.position);
        Debug.Log(transform.position);
    }

    private void Update()
    {
        if (womanCollding)
        {
            if (woman.interact)
            {
                FMODUnity.RuntimeManager.PlayOneShot(fallingPlankSFX, transform.position);
                anim.SetTrigger("BridgeOpen");
            }
        }
    }
}
