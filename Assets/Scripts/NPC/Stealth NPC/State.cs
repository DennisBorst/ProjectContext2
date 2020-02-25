using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class State
{
    public FSM fsm;
    public StateEnum id;

    public void Init(FSM _owner)
    {
        fsm = _owner;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}

