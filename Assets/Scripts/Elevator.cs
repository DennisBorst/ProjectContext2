using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Interactable
{
    //private serialized
    [SerializeField] private float pullSpeed;
    [SerializeField] private GameObject elevator;
    [SerializeField] private Transform elevatorBeginPoint;
    [SerializeField] private Transform elevatorEndPoint;

    //private
    private float pullTime;
    private float currentPullTime;
    private bool readyToPull;
    private bool pulling;
    private Vector2 pullDirection;
    private Vector2 inputPlayer;
    private Vector3 elevatorBeginPointVector;
    private Vector3 elevatorEndPointVector;

    // Start is called before the first frame update
    private void Start()
    {
        elevatorBeginPointVector = elevatorBeginPoint.position;
        elevatorEndPointVector = elevatorEndPoint.position;
        pullTime = Vector3.Distance(elevatorBeginPointVector, elevatorEndPointVector);
        currentPullTime = pullTime;
    }

    // Update is called once per frame
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

        if(pullDirection.x <= inputPlayer.x && pullDirection.y <= inputPlayer.y)
        {
            Pulling();
        }
        else
        {

        }
    }
    private void Pulling()
    {
        currentPullTime = Timer(currentPullTime);

        elevator.transform.position = Vector3.Lerp(elevator.transform.position, elevatorEndPointVector, pullSpeed);
        if(currentPullTime <= 0)
        {
            currentPullTime = 0;
        }
        /*
        if (Vector3.Distance(startPos, endPos).ToString("0.00") == "0.00")
        {
            Debug.Log("Target Reached!!!");
        }
        */
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

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
