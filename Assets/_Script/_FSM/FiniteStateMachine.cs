using System;
using System.Collections.Generic;

public class FiniteStateMachine<DerivenState>
    where DerivenState : State<DerivenState>
{
    private readonly IReadOnlyDictionary<int, DerivenState> stateDictionary;
    private DerivenState currentState;
    public int DBG_CURRENT_HASH;

    public FiniteStateMachine(IReadOnlyDictionary<int ,DerivenState> stateDictionary)
    {
        this.stateDictionary = stateDictionary;
    }
    public void SetState(int hash)
    {
        DBG_CURRENT_HASH = hash;
        currentState = stateDictionary[hash];
        currentState.OnEnter();
    }
    public void ChangeState(int hash)
    {
        currentState.OnExit();
        SetState(hash);
    }
    public void Update()
    {
        currentState.OnUpdate();
    }
    public void FixedUpdate()
    {
        currentState.OnFixedUpdate();
    }
}
