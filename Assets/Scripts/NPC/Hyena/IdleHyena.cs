using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleHyena : State
{
    private float distToPlayer;
    private float distToBegin;
    private bool setPosition = false;
    private bool scared = false;
    private Vector3 beginPosition;

    public IdleHyena(StateEnum id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);

        if (!setPosition)
        {
            setPosition = true;
            beginPosition = blackBoard.hyena.transform.position;
        }
    }
    public override void OnExit()
    {
        scared = true;
    }
    public override void OnUpdate()
    {
        if (scared)
        {
            distToBegin = Mathf.Abs(Vector3.Distance(blackBoard.hyena.transform.position, beginPosition));

            if (distToBegin < 1)
            {
                blackBoard.hyena.SetAnimation("isRunning", false);
                return;
            }

            blackBoard.navMeshAgent.destination = beginPosition;
            blackBoard.hyena.SetAnimation("isRunning", true);
            return;
        }

        distToPlayer = Mathf.Abs(Vector3.Distance(blackBoard.hyena.transform.position, blackBoard.playerScripts[0].transform.position));

        if (distToPlayer < blackBoard.hyena.distanceFromPlayer)
        {
            fsm.SwitchState(StateEnum.Wander);
        }
    }
}
