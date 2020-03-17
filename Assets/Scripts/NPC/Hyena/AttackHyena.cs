using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHyena : State
{
    private float distanceToPlayer;

    public AttackHyena(StateEnum id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        blackBoard.navMeshAgent.speed = blackBoard.hyena.attackSpeed;
        blackBoard.navMeshAgent.stoppingDistance = blackBoard.hyena.attackDistance;

        blackBoard.hyena.SetAnimation("isRunning", true);
    }
    public override void OnExit()
    {
        blackBoard.hyena.SetAnimation("isRunning", false);
    }
    public override void OnUpdate()
    {
        distanceToPlayer = Mathf.Abs(Vector3.Distance(blackBoard.hyena.transform.position, blackBoard.playerScripts[0].transform.position));

        blackBoard.navMeshAgent.destination = blackBoard.playerScripts[0].transform.position;

        if(distanceToPlayer <= blackBoard.hyena.attackDistance)
        {
            Debug.Log("Game Over");
        }

        if (blackBoard.playerScripts[1].interact)
        {
            fsm.SwitchState(StateEnum.Idle);
        }
    }
}
