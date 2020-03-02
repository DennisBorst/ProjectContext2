﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyCrack : Interactable
{
    //private serialized
    [SerializeField] private float walkToBeginSpeed;
    [SerializeField] private float sneakSpeed;
    [SerializeField] private float distanceToPlaySneak;
    [SerializeField] private float rotationToEnd;
    [SerializeField] private float rotationToBegin;

    [SerializeField] private Transform crackBegin;
    [SerializeField] private Transform crackEnd;

    //private
    private float distanceBeginCrack;
    private float distanceEndCrack;
    private bool animationStarted;
    private bool startSneaking;
    private bool sneakToEnd;
    private CapsuleCollider collider;

    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.layer == 8)
            {
                manCollding = true;
            }
            else if (collider.gameObject.layer == 9)
            {
                womanCollding = true;
            }
        }
    }
    public override void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.layer == 8)
            {
                manCollding = false;
            }
            else if (collider.gameObject.layer == 9)
            {
                womanCollding = false;
            }
        }
    }
    private void Update()
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
                woman.playerObject.transform.rotation = Quaternion.Euler(womanRotation.x, womanRotation.y + rotationToEnd, womanRotation.z);
                ResetBools();
            }
            else
            {
                woman.transform.position = Vector3.MoveTowards(woman.transform.position, crackEnd.position, sneakSpeed);
                woman.playerObject.transform.rotation = Quaternion.Euler(womanRotation.x, womanRotation.y - rotationToEnd, womanRotation.z);
            }
        }
        else
        {
            Quaternion womanRotation = woman.playerObject.transform.rotation;

            if (woman.transform.position == crackBegin.position)
            {
                ResetBools();
            }
            else
            {
                woman.transform.position = Vector3.MoveTowards(woman.transform.position, crackBegin.position, sneakSpeed);
                woman.playerObject.transform.rotation = Quaternion.Euler(womanRotation.x, womanRotation.y + rotationToBegin, womanRotation.z);
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