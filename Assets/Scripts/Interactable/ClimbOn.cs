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
    [SerializeField] private Transform[] climbBegin;
    [SerializeField] private Transform[] climbEnd;
    [SerializeField] private Transform[] walkEnd;

    //private
    private float climbAnimationTime = 4.183f;
    private float currentAnimTimeMan;
    private float currentAnimTimeWoman;
    private bool teleportedMan;
    private bool teleportedWoman;
    private bool animMan;
    private bool animWoman;

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
                man.animationPlaying = true;
                man.transform.position = Vector3.MoveTowards(man.transform.position, climbBegin[0].position, walkToBeginSpeed);
                man.playerObject.transform.eulerAngles = climbBegin[0].eulerAngles;

                if (distance <= distanceToClimb)
                {
                    man.SetAnimation("isClimbing", true);
                    man.SetanimationBoolFalse("isClimbing", climbAnimationTime);
                    teleportedMan = true;
                    //StartCoroutine(ClimbTime(true));
                }
                else
                {
                    man.SetAnimation("isWalking", true);
                }
            }
        }

        if (teleportedMan)
        {
            currentAnimTimeMan = Timer(currentAnimTimeMan);

            if(currentAnimTimeMan <= 0)
            {
                man.transform.position = climbEnd[0].position;
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
                woman.animationPlaying = true;
                woman.transform.position = Vector3.MoveTowards(woman.transform.position, climbBegin[1].position, walkToBeginSpeed);
                woman.playerObject.transform.eulerAngles = climbBegin[1].eulerAngles;

                if (distance <= distanceToClimb)
                {
                    woman.SetAnimation("isClimbing", true);
                    woman.SetanimationBoolFalse("isClimbing", climbAnimationTime);
                    teleportedWoman = true;
                }
                else
                {
                    woman.SetAnimation("isWalking", true);
                }
            }
        }

        if (teleportedWoman)
        {
            currentAnimTimeWoman = Timer(currentAnimTimeWoman);

            if (currentAnimTimeWoman <= 0)
            {
                woman.transform.position = climbEnd[1].position;
            }
        }
    }
    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
