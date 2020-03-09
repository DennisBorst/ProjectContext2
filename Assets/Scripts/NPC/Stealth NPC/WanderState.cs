using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : State
{
    private float distanceFromDestination = 2f;
    private float distanceToLocation;
    private Transform targetPoint;

    public WanderState(StateEnum id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        blackBoard.navMeshAgent.speed = blackBoard.npcStealth.walkSpeed;
        GetRandomWanderPoint();
    }
    public override void OnExit()
    {
        Debug.Log("Exit state");
    }
    public override void OnUpdate()
    {
        blackBoard.npcStealth.SetAnimation("isWalking", true);
        distanceToLocation = Mathf.Abs(Vector3.Distance(blackBoard.npcStealth.transform.position, targetPoint.transform.position));
        Debug.Log(distanceToLocation);

        if (distanceFromDestination >= distanceToLocation)
        {
            fsm.SwitchState(StateEnum.Idle);
        }
        else
        {
            blackBoard.navMeshAgent.destination = targetPoint.transform.position;
        }
    }
    private Vector3 GetRandomWanderPoint()
    {
        System.Random random = new System.Random();
        int wanderNumberPoint = random.Next(0, blackBoard.wanderPoints.Length);
        targetPoint = blackBoard.wanderPoints[wanderNumberPoint];
        return targetPoint.transform.position;
    }
}
