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
    private Vector2 inputPlayer;
    private Vector3 elevatorBeginPointVector;
    private Vector3 elevatorEndPointVector;
    private Vector3 elevatorCurrentPoint;

    private Animator anim;

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
        }
        if (man.deinteract)
        {
            readyToPull = false;
            man.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            man.ResetCharacter(1);
        }

        if (readyToPull)
        {
            WaitForPulling();
            man.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            man.ResetCharacter(-1);
        }


        //Animations
        if (isPulling)
        {
            anim.SetBool("ElevatorMoving", true);
        }
        else
        {
            anim.SetBool("ElevatorMoving", false);
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
    }
    private void Pulling()
    {
        elevatorCurrentPoint = elevator.transform.position;
        elevator.transform.position = Vector3.MoveTowards(elevatorCurrentPoint, elevatorBeginPointVector, pullSpeed);
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
