using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkHyena : State
{
    private float distanceToLocation;

    public WalkHyena(StateEnum id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        blackBoard.navMeshAgent.speed = blackBoard.hyena.sneakSpeed;

        blackBoard.hyena.SetAnimation("isWalking", true);
    }
    public override void OnExit()
    {
        blackBoard.hyena.SetAnimation("isWalking", false);
    }
    public override void OnUpdate()
    {
        distanceToLocation = Mathf.Abs(Vector3.Distance(blackBoard.hyena.transform.position, blackBoard.playerScripts[0].transform.position));

        if (distanceToLocation < blackBoard.hyena.distanceFromPlayer && distanceToLocation > blackBoard.navMeshAgent.stoppingDistance)
        {
            ClosingUpToPlayer();
        }
        else if(distanceToLocation > blackBoard.hyena.distanceFromPlayer)
        {

        }
        else
        {
            fsm.SwitchState(StateEnum.Attack);
        }
    }

    private void ClosingUpToPlayer()
    {
        blackBoard.navMeshAgent.destination = blackBoard.playerScripts[0].transform.position;
    }

    private void CircleAroundPlayer()
    {

    }
}
