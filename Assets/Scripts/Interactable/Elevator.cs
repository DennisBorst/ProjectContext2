using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Interactable
{
    //private serialized
    [SerializeField] private float pullSpeed;
    [SerializeField] private int pullPushDirection;
    [SerializeField] private bool verticalElevator;
    [Space]
    [SerializeField] private GameObject elevator;
    [SerializeField] private Transform elevatorBeginPoint;
    [SerializeField] private Transform elevatorEndPoint;

    //private
    private bool readyToPull;
    private bool isPulling = false;
    private bool startsPulling = false;
    private Vector2 inputPlayer;
    private Vector3 elevatorBeginPointVector;
    private Vector3 elevatorEndPointVector;
    private Vector3 elevatorCurrentPoint;

    private Animator anim;

    //FMOD
    //[FMODUnity.EventRef]
    //public string bridgeLeftSFX;
    //[FMODUnity.EventRef]
    //public string bridgeRightSFX;

    //FMOD.Studio.EventInstance bridgeLeft;
    //FMOD.Studio.EventInstance bridgeRight;

    private void Start()
    {
        elevatorBeginPointVector = elevatorBeginPoint.position;
        elevatorEndPointVector = elevatorEndPoint.position;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (manCollding && man.interact)
        {
            readyToPull = true;
            man.animationPlaying = true;
            man.SetAnimation("isWalking", false);
            man.SetAnimation("stopsInteracting", false);
        }
        if (manCollding && man.deinteract)
        {
            readyToPull = false;
            man.animationPlaying = false;
            man.SetAnimation("isPullingRope", false);
            man.SetAnimation("stopsInteracting", true);
            man.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            man.ResetCharacter(1);
        }

        if (readyToPull)
        {
            WaitForPulling();
            man.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            man.ResetCharacter(-1);
        }
        else
        {
            man.SetAnimation("isPullingRope", false);
        }

        //Animations
        if (isPulling && readyToPull)
        {
            anim.SetBool("ElevatorMoving", true);
            man.SetAnimation("isPullingRope", true);
        }
        else
        {
            anim.SetBool("ElevatorMoving", false);
            man.SetAnimation("isPullingRope", false);
        }
    }
    private void WaitForPulling()
    {
        inputPlayer = new Vector2(man.inputX, man.inputZ);

        if (verticalElevator)
        {
            if ((pullPushDirection * -1) >= inputPlayer.y)
            {
                Pulling();
                isPulling = true;
            }
            else if (pullPushDirection <= inputPlayer.y)
            {
                Pushing();
                isPulling = true;
            }
            else
            {
                isPulling = false;
            }
        }
        else
        {
            if ((pullPushDirection * -1) >= inputPlayer.x)
            {
                Pulling();
                isPulling = true;
            }
            else if (pullPushDirection <= inputPlayer.x)
            {
                Pushing();
                isPulling = true;
            }
            else
            {
                isPulling = false;
            }
        }

    }
    private void Pushing()
    {
        elevatorCurrentPoint = elevator.transform.position;
        elevator.transform.position = Vector3.MoveTowards(elevatorCurrentPoint, elevatorEndPointVector, pullSpeed);
        //bridgeLeft = FMODUnity.RuntimeManager.CreateInstance(bridgeLeftSFX);
        //bridgeLeft.start();
    }
    private void Pulling()
    {
        elevatorCurrentPoint = elevator.transform.position;
        elevator.transform.position = Vector3.MoveTowards(elevatorCurrentPoint, elevatorBeginPointVector, pullSpeed);
        //bridgeRight = FMODUnity.RuntimeManager.CreateInstance(bridgeRightSFX);
        //bridgeRight.start();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }
    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }
    public override void SwitchBool(bool man)
    {
        base.SwitchBool(man);
    }
}
