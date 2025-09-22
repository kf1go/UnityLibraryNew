using System;
using System.ComponentModel;
using UnityEngine;

public abstract class State<T>
    where T : State<T>
{
    protected FiniteStateMachine<T> FSM { get; private set; }

    public State(FiniteStateMachine<T> finiteStateMachine)
    {
        FSM = finiteStateMachine;
    }
    internal virtual void OnEnter()
    {
    }
    internal virtual void OnUpdate()
    {
    }
    internal virtual void OnExit()
    {
    }
    internal virtual void OnFixedUpdate()
    {
    }
}
