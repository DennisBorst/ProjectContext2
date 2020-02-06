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
    private Vector2 inputPlayer;
    private Vector3 elevatorBeginPointVector;
    private Vector3 elevatorEndPointVector;
    private Vector3 elevatorCurrentPoint;

    private void Start()
    {
        elevatorBeginPointVector = elevatorBeginPoint.position;
        elevatorEndPointVector = elevatorEndPoint.position;
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
            man.ResetCharacter(1);
        }

        if (readyToPull)
        {
            WaitForPulling();
            man.ResetCharacter(-1);
        }
    }
    private void WaitForPulling()
    {
        inputPlayer = new Vector2(man.inputX, man.inputZ);

        if (verticalElevator)
        {
            //if (man.interact)
            //{
            if ((pullPushDirection * -1) >= inputPlayer.y)
            {
                Pulling();
            }
            else if (pullPushDirection <= inputPlayer.y)
            {
                Pushing();
            }
            //}
        }
        else
        {
            //if (man.interact)
            //{
            if ((pullPushDirection * -1) >= inputPlayer.x)
            {
                Pulling();
            }
            else if (pullPushDirection <= inputPlayer.x)
            {
                Pushing();
            }
            //}
        }

    }
    private void Pushing()
    {
        elevatorCurrentPoint = elevator.transform.position;
        elevator.transform.position = Vector3.Lerp(elevatorCurrentPoint, elevatorEndPointVector, pullSpeed);
    }
    private void Pulling()
    {
        elevatorCurrentPoint = elevator.transform.position;
        elevator.transform.position = Vector3.Lerp(elevatorCurrentPoint, elevatorBeginPointVector, pullSpeed);
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
