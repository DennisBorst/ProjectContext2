using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float idleTime = 4f;
    private float currentIdleTime;

    public IdleState(StateEnum id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        currentIdleTime = idleTime;

    }
    public override void OnExit()
    {
        Debug.Log("Exit state");
    }
    public override void OnUpdate()
    {
        Debug.Log("Idle state");
        blackBoard.navMeshAgent.destination = blackBoard.npcStealth.transform.position;
        blackBoard.npcStealth.SetAnimation("isWalking", false);

        currentIdleTime = Timer(currentIdleTime);

        if (currentIdleTime <= 0)
        {
            currentIdleTime = idleTime;
            fsm.SwitchState(StateEnum.Wander);
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
