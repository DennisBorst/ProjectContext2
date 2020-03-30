using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbOn : Interactable
{
    //private Serialized
    [SerializeField] private float walkToBeginSpeed;
    [SerializeField] private float distanceToClimb;
    [SerializeField] private Transform[] climbBegin;
    [SerializeField] private Transform[] climbEnd;

    //private
    private float climbAnimationTime = 3.137f;
    private float currentAnimTimeMan;
    private float currentAnimTimeWoman;
    private bool teleportedMan;
    private bool teleportedWoman;

    private void Start()
    {
        currentAnimTimeMan = climbAnimationTime;
        currentAnimTimeWoman = climbAnimationTime;
    }

    private void Update()
    {
        InputMan();
        InputWoman();
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);

        if (collider.gameObject.layer == 8)
        {
            teleportedMan = false;
            currentAnimTimeMan = climbAnimationTime;
            man.SetAnimation("isClimbing", false);
            man.animationPlaying = false;
        }

        if (collider.gameObject.layer == 9)
        {
            teleportedWoman = false;
            currentAnimTimeWoman = climbAnimationTime;
            woman.SetAnimation("isClimbing", false);
            woman.animationPlaying = false;
        }
    }

    private void InputMan()
    {
        if (manCollding)
        {
            if (man.interact && !teleportedMan)
            {
                float distance = Vector3.Distance(man.gameObject.transform.position, climbBegin[0].position);

                if (distance <= distanceToClimb)
                {
                    man.walking = false;
                    man.SetAnimation("isClimbing", true);
                    man.SetanimationBoolFalse("isClimbing", climbAnimationTime);
                    teleportedMan = true;
                    return;
                }

                man.animationPlaying = true;
                man.walking = true;
                man.SetAnimation("isWalking", true);
                man.transform.position = Vector3.MoveTowards(man.transform.position, climbBegin[0].position, walkToBeginSpeed);
                man.playerObject.transform.eulerAngles = climbBegin[0].eulerAngles;
            }
        }

        if (teleportedMan)
        {
            currentAnimTimeMan = Timer(currentAnimTimeMan);

            if(currentAnimTimeMan <= 0)
            {
                man.transform.position = climbEnd[0].position;
                man.SetAnimation("isClimbing", false);
            }
        }
    }

    private void InputWoman()
    {
        if (womanCollding)
        {
            if (woman.interact && !teleportedWoman)
            {
                float distance = Vector3.Distance(woman.gameObject.transform.position, climbBegin[1].position);

                if (distance <= distanceToClimb)
                {
                    woman.walking = false;
                    woman.SetAnimation("isClimbing", true);
                    woman.SetanimationBoolFalse("isClimbing", climbAnimationTime);
                    teleportedWoman = true;
                    return;
                }

                woman.animationPlaying = true;
                woman.walking = true;
                woman.SetAnimation("isWalking", true);
                woman.transform.position = Vector3.MoveTowards(woman.transform.position, climbBegin[1].position, walkToBeginSpeed);
                woman.playerObject.transform.eulerAngles = climbBegin[1].eulerAngles;

            }
        }

        if (teleportedWoman)
        {
            currentAnimTimeWoman = Timer(currentAnimTimeWoman);

            if (currentAnimTimeWoman <= 0)
            {
                woman.transform.position = climbEnd[1].position;
                woman.SetAnimation("isClimbing", false);
            }
        }
    }
    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
