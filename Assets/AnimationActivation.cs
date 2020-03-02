using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationActivation : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void AnimationActive(string animation)
    {
        anim.SetBool(animation, true);
    }
}