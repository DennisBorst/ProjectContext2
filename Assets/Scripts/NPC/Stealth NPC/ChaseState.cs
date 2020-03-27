using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    private float grabRange = 1f;
    private float distToPlayer;
    private float distToLastPlayerPos;
    private float outOfSightTimer = 3f;
    private float currentOutOfSightTime;
    private Vector3 lastPlayerPosition;

    public ChaseState(StateEnum id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        blackBoard.navMeshAgent.speed = blackBoard.npcStealth.chaseSpeed;
        blackBoard.npcStealth.exclamtionMark.SetActive(true);
        currentOutOfSightTime = outOfSightTimer;
    }
    public override void OnExit()
    {
    }
    public override void OnUpdate()
    {
        /*
        if (!blackBoard.npcStealth.following)
        {
            distToLastPlayerPos = Mathf.Abs(Vector3.Distance(blackBoard.npcStealth.transform.position, lastPlayerPosition));

            if (distToLastPlayerPos <= 1f)
            {
                fsm.SwitchState(StateEnum.Idle);
            }
            return;
        }
        */
        Debug.Log(currentOutOfSightTime);

        blackBoard.npcStealth.SetAnimation("isRunning", true);
        blackBoard.npcStealth.SetAnimation("isWalking", false);
        distToPlayer = Mathf.Abs(Vector3.Distance(blackBoard.npcStealth.transform.position, blackBoard.players[0].transform.position));

        if(grabRange >= distToPlayer)
        {
            Debug.Log("Game Over");
            GameManager.Instance.Reload(1.5f);
            blackBoard.navMeshAgent.destination = blackBoard.npcStealth.transform.position;
        }
        else
        {
            blackBoard.navMeshAgent.destination = blackBoard.players[0].transform.position;
            KeepTargetInSight();
        }
    }

    private void KeepTargetInSight()
    {
        Transform target = blackBoard.npcStealth.visibleTargets[0];
        Vector3 dirToTarget = (target.position - blackBoard.npcStealth.transform.position).normalized;
        if (Vector3.Angle(blackBoard.npcStealth.transform.forward, dirToTarget) < blackBoard.npcStealth.viewAngle / 2)
        {
            float dstToTarget = Vector3.Distance(blackBoard.npcStealth.transform.position, target.position);
            if (!Physics.Raycast(blackBoard.npcStealth.transform.position, dirToTarget, dstToTarget, blackBoard.npcStealth.obstacleMask))
            {
                currentOutOfSightTime = outOfSightTimer;
            }
        }
        else
        {
            currentOutOfSightTime = Timer(currentOutOfSightTime);
        }

        if (currentOutOfSightTime <= 0)
        {
            blackBoard.npcStealth.following = false;
            blackBoard.npcStealth.exclamtionMark.SetActive(false);
            fsm.SwitchState(StateEnum.Idle);
            //lastPlayerPosition = blackBoard.players[0].transform.position;
            //blackBoard.navMeshAgent.destination = lastPlayerPosition;
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
