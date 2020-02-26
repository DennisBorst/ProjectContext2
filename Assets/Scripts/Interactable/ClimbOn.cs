using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbOn : Interactable
{
    //private Serialized
    [SerializeField] private float walkToBeginSpeed;
    [SerializeField] private float walkToEndSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float distanceToClimb;
    [SerializeField] private Transform climbBegin;
    [SerializeField] private Transform climbEnd;
    [SerializeField] private Transform walkEnd;

    //private
    private float climbAnimationTime = 3.8f;
    private bool manClimbing = false;
    private bool womanClimbing = false;
    private bool teleported;
    private bool walking;

    private void Update()
    {
        if(!womanClimbing)
        {
            InputMan();
        }

        if (!manClimbing)
        {
            InputWoman();
        }
    }

    private void InputMan()
    {
        if (manCollding)
        {
            if (man.interact)
            {
                float distance = Vector3.Distance(man.gameObject.transform.position, climbBegin.position);

                man.animationPlaying = true;
                manClimbing = true;
                man.transform.position = Vector3.MoveTowards(man.transform.position, climbBegin.position, walkToBeginSpeed);
                Vector3 direction = (climbBegin.position - man.transform.position).normalized;
                man.playerObject.transform.LookAt(direction);

                if (distance <= distanceToClimb)
                {
                    man.SetAnimation("isClimbing", true);
                    man.SetanimationBoolFalse("isClimbing", climbAnimationTime);
                    StartCoroutine(ClimbTime());
                }
                else
                {
                    man.SetAnimation("isWalking", true);
                }
            }
        }

        if (teleported)
        {
            if (walking)
            {
                man.SetAnimation("isWalking", true);
                man.transform.position = Vector3.MoveTowards(man.transform.position, walkEnd.position, walkToEndSpeed);
                if (man.transform.position == walkEnd.position)
                {
                    man.SetAnimation("isWalking", false);
                    man.animationPlaying = false;
                    ResetBools();
                }
            }
            else
            {
                man.transform.position = climbEnd.position;
                man.SetAnimation("isClimbing", false);
                walking = true;
            }
        }
    }


    private void InputWoman()
    {
        if (womanCollding)
        {
            if (woman.interact)
            {
                float distance = Vector3.Distance(woman.gameObject.transform.position, climbBegin.position);

                woman.animationPlaying = true;
                womanClimbing = true;
                woman.transform.position = Vector3.MoveTowards(woman.transform.position, climbBegin.position, walkToBeginSpeed);
                Vector3 direction = (climbBegin.position - woman.transform.position).normalized;
                woman.playerObject.transform.LookAt(direction);

                if (distance <= distanceToClimb)
                {
                    woman.SetAnimation("isClimbing", true);
                    woman.SetanimationBoolFalse("isClimbing", climbAnimationTime);
                    StartCoroutine(ClimbTime());
                }
                else
                {
                    woman.SetAnimation("isWalking", true);
                }
            }
        }

        if (teleported)
        {
            if (walking)
            {
                woman.SetAnimation("isWalking", true);
                woman.transform.position = Vector3.MoveTowards(woman.transform.position, walkEnd.position, walkToEndSpeed);
                if (woman.transform.position == walkEnd.position)
                {
                    woman.SetAnimation("isWalking", false);
                    woman.animationPlaying = false;
                    ResetBools();
                }
            }
            else
            {
                woman.transform.position = climbEnd.position;
                woman.SetAnimation("isClimbing", false);
                walking = true;
            }
        }
    }

    private void ResetBools()
    {
        StopAllCoroutines();
        teleported = false;
        walking = false;
        manClimbing = false;
        womanClimbing = false;
    }

    IEnumerator ClimbTime()
    {
        yield return new WaitForSeconds(climbAnimationTime);
        teleported = true;
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
