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
    [SerializeField] private float distanceToEnd;
    [SerializeField] private Transform climbBegin;
    [SerializeField] private Transform climbEnd;
    [SerializeField] private Transform walkEnd;

    //private
    private float climbAnimationTime = 4.17f;
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
                man.playerObject.transform.eulerAngles = climbBegin.eulerAngles;

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
                float distanceToEnd = Vector3.Distance(man.gameObject.transform.position, walkEnd.position);

                man.SetAnimation("isWalking", true);
                man.transform.position = Vector3.MoveTowards(man.transform.position, walkEnd.position, walkToEndSpeed);
                if (distanceToEnd <= distanceToEnd)
                {
                    man.SetAnimation("isWalking", false);
                    man.animationPlaying = false;
                    ResetBools();
                }
            }
            else
            {
                man.SetAnimation("isClimbing", false);
                man.transform.position = climbEnd.position;
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
                woman.playerObject.transform.eulerAngles = climbBegin.eulerAngles;

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
                float distanceToEnd = Vector3.Distance(woman.gameObject.transform.position, walkEnd.position);

                woman.SetAnimation("isWalking", true);
                woman.transform.position = Vector3.MoveTowards(woman.transform.position, walkEnd.position, walkToEndSpeed);
                if (distanceToEnd <= distanceToEnd)
                {
                    woman.SetAnimation("isWalking", false);
                    woman.animationPlaying = false;
                    ResetBools();
                }
            }
            else
            {
                woman.SetAnimation("isClimbing", false);
                woman.transform.position = climbEnd.position;
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
