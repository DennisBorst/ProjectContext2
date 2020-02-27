using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(StateEnum id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
    }
    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }
    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
