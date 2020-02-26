using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyCrack : Interactable
{

    //private serialized
    [SerializeField] private float walkToBeginSpeed;
    [SerializeField] private float sneakSpeed;
    [SerializeField] private float distanceToPlaySneak;

    [SerializeField] private Transform crackBegin;
    [SerializeField] private Transform crackEnd;

    //private
    private float distanceBeginCrack;
    private float distanceEndCrack;
    private bool animationStarted;
    private bool startSneaking;
    private bool sneakToEnd;

    private CapsuleCollider collider;

    void Update()
    {
        if (womanCollding)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0) && !animationStarted)
            {
                animationStarted = true;
                woman.GetComponent<CapsuleCollider>().enabled = false;
            }
        }

        if (animationStarted)
        {
            woman.animationPlaying = true;
            Animation();
        }

        if (startSneaking)
        {
            Sneaking();
        }
    }

    private void Animation()
    {
        distanceBeginCrack = Vector3.Distance(woman.gameObject.transform.position, crackBegin.position);
        distanceEndCrack = Vector3.Distance(woman.gameObject.transform.position, crackEnd.position);

        if (distanceBeginCrack < distanceEndCrack)
        {
            woman.transform.position = Vector3.MoveTowards(woman.transform.position, crackBegin.position, walkToBeginSpeed);
            Vector3 direction = (crackBegin.position - woman.transform.position).normalized;
            woman.playerObject.transform.LookAt(direction);
            //woman.playerObject.transform.LookAt(new Vector3(direction.x,(direction.y - 90), direction.z));

            if (distanceBeginCrack <= distanceToPlaySneak && !startSneaking)
            {
                startSneaking = true;
                sneakToEnd = true;
                woman.SetAnimation("isSneaking", true);
            }
        }
        else
        {
            woman.transform.position = Vector3.MoveTowards(woman.transform.position, crackEnd.position, walkToBeginSpeed);
            Vector3 direction = (crackEnd.position - woman.transform.position).normalized;
            woman.playerObject.transform.LookAt(direction);

            if (distanceEndCrack <= distanceToPlaySneak && !startSneaking)
            {
                startSneaking = true;
                sneakToEnd = false;
                woman.SetAnimation("isSneaking", true);
            }
        }
    }

    private void Sneaking()
    {

        if (sneakToEnd)
        {
            Quaternion womanRotation = woman.playerObject.transform.rotation;

            if (woman.transform.position == crackEnd.position)
            {
                woman.playerObject.transform.rotation = Quaternion.Euler(womanRotation.x, womanRotation.y + 90, womanRotation.z);
                ResetBools();
            }
            else
            {
                woman.transform.position = Vector3.MoveTowards(woman.transform.position, crackEnd.position, sneakSpeed);
                woman.playerObject.transform.rotation = Quaternion.Euler(womanRotation.x, womanRotation.y - 90, womanRotation.z);
            }
        }
        else
        {
            if (woman.transform.position == crackBegin.position)
            {
                ResetBools();
            }
            else
            {
                woman.transform.position = Vector3.MoveTowards(woman.transform.position, crackBegin.position, sneakSpeed);
            }
        }
    }

    private void ResetBools()
    {
        woman.SetAnimation("isSneaking", false);
        woman.GetComponent<CapsuleCollider>().enabled = true;
        woman.animationPlaying = false;
        womanCollding = true;
        animationStarted = false;
        startSneaking = false;
        sneakToEnd = false;
    }
}
