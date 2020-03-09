using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float idleTime = 4f;
    private float currentIdleTime;
    private float distToBegin;
    private bool setPosition = false;
    private Vector3 beginPosition;
    private Quaternion beginRotation;

    public IdleState(StateEnum id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        currentIdleTime = idleTime;
        if(!setPosition)
        {
            setPosition = true;
            beginPosition = blackBoard.npcStealth.transform.position;
            beginRotation = blackBoard.npcStealth.transform.rotation;
            Debug.Log(beginPosition);
        }
    }
    public override void OnExit()
    {
        Debug.Log("Exit state");
    }
    public override void OnUpdate()
    {
        Debug.Log("Idle state");
        distToBegin = Mathf.Abs(Vector3.Distance(blackBoard.npcStealth.transform.position, beginPosition));
        if (distToBegin >= 1f)
        {
            blackBoard.npcStealth.SetAnimation("isWalking", true);
            blackBoard.navMeshAgent.destination = beginPosition;
        }
        else
        {
            blackBoard.npcStealth.SetAnimation("isWalking", false);
            blackBoard.npcStealth.transform.rotation = beginRotation;
        }

        if (!blackBoard.npcStealth.idleNPC)
        {
            currentIdleTime = Timer(currentIdleTime);

            if (currentIdleTime <= 0)
            {
                currentIdleTime = idleTime;
                fsm.SwitchState(StateEnum.Wander);
            }
        }
    }
    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
