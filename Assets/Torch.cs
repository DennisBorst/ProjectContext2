using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Interactable
{
    private Animator anim;
    private bool activated = false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!activated)
        {
            activated = true;
            woman.SetAnimation("gotTorch", true);
            StartCoroutine(woman.SetanimationBoolFalse("gotTorch", 2.2f));
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button0) && !woman.animationPlaying)
        {
            woman.animationPlaying = true;
            woman.SetAnimation("isAttacking", true);
            StartCoroutine(woman.SetanimationBoolFalse("isAttacking", 2.4f));
        }
    }
}
