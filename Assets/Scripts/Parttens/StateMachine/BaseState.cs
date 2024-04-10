using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState <EState> where EState : Enum
{
    public BaseState(EState key,StateManager<EState> stateManager)
    {
        StateKey = key;
        StateManager = stateManager;
    }
    public EState StateKey { get; set; }
 
    public StateManager<EState> StateManager { get; set; }
    public abstract void EnterState();
    public abstract void ExiteState();
    public abstract void UpdateState();
  
  /*  public abstract EState GetNextState();*/
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerExit(Collider other);

}
