using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Interactable
{
    private Animator anim;
    private AudioSource source;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        Debug.Log(transform.position);
    }

    private void Update()
    {
        if (womanCollding)
        {
            if (woman.interact)
            {
                GetComponent<FMODUnity.StudioEventEmitter>().Play();
                anim.SetTrigger("BridgeOpen");
            }
        }
    }
}
