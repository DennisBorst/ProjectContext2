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

    [Header("Only for vertical elevator")]
    [SerializeField] private Transform contraWeight;
    [SerializeField] private Transform ropeContraWeight;
    [SerializeField] private Transform ropeElevator;

    private Vector3 contraWeightBeginPoint;
    private Vector3 contraWeightCurrentPoint;

    private void Start()
    {
        elevatorBeginPointVector = elevatorBeginPoint.position;
        elevatorEndPointVector = elevatorEndPoint.position;
        anim = GetComponentInChildren<Animator>();


        if (verticalElevator)
        {
            contraWeightBeginPoint = contraWeight.transform.position;
        }
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

        //Animationsw
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

        Debug.Log(GetComponent<FMODUnity.StudioEventEmitter>().IsPlaying());
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
                GetComponent<FMODUnity.StudioEventEmitter>().Stop();

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
                GetComponent<FMODUnity.StudioEventEmitter>().Stop();

            }
        }

    }
    private void Pushing()
    {
        elevatorCurrentPoint = elevator.transform.position;
        elevator.transform.position = Vector3.MoveTowards(elevatorCurrentPoint, elevatorEndPointVector, pullSpeed);

        if (verticalElevator)
        {
            contraWeightCurrentPoint = contraWeight.transform.position;
            contraWeight.transform.position = Vector3.MoveTowards(contraWeightCurrentPoint, contraWeightBeginPoint - (Vector3.down * -4), pullSpeed * 0.8f);

            Vector3 currentScaleRopeContra = ropeContraWeight.localScale;
            ropeContraWeight.transform.localScale = Vector3.MoveTowards(currentScaleRopeContra, new Vector3(currentScaleRopeContra.x, 0.095f, currentScaleRopeContra.z), pullSpeed * 0.018f);

            Vector3 currentScaleRopeElevator = ropeElevator.localScale;
            ropeElevator.transform.localScale = Vector3.MoveTowards(currentScaleRopeElevator, new Vector3(currentScaleRopeElevator.x, 0.001962778f, currentScaleRopeElevator.z), pullSpeed * 0.002f);


        }

        GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("LeftRight", 0.0f);
        if (!GetComponent<FMODUnity.StudioEventEmitter>().IsPlaying())
        {
            GetComponent<FMODUnity.StudioEventEmitter>().Play();

        }
    }
    private void Pulling()
    {
        elevatorCurrentPoint = elevator.transform.position;
        elevator.transform.position = Vector3.MoveTowards(elevatorCurrentPoint, elevatorBeginPointVector, pullSpeed);


        if (verticalElevator)
        {
            contraWeightCurrentPoint = contraWeight.transform.position;
            contraWeight.transform.position = Vector3.MoveTowards(contraWeightCurrentPoint, contraWeightBeginPoint, pullSpeed * 0.8f);

            Vector3 currentScaleRopeContra = ropeContraWeight.localScale;
            ropeContraWeight.transform.localScale = Vector3.MoveTowards(currentScaleRopeContra, new Vector3(currentScaleRopeContra.x, 0.011f, currentScaleRopeContra.z), pullSpeed * 0.018f);

            Vector3 currentScaleRopeElevator = ropeElevator.localScale;
            ropeElevator.transform.localScale = Vector3.MoveTowards(currentScaleRopeElevator, new Vector3(currentScaleRopeElevator.x, 0.011f, currentScaleRopeElevator.z), pullSpeed * 0.002f);

        }

        GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("LeftRight", 1.0f);
        if (!GetComponent<FMODUnity.StudioEventEmitter>().IsPlaying())
        {
            GetComponent<FMODUnity.StudioEventEmitter>().Play();

        }

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
