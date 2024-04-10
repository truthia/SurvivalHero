using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState:Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    public BaseState<EState> CurrentState;

    protected bool IsTransitioningState = false;
    [Header("Don't change this")]
    [ReadOnly]
    public EState currentKeyState;
    private void Start()
    {
      
        CurrentState.EnterState();
    }
    public  virtual void Update()
    {
      //  EState nextStateKey = CurrentState.GetNextState();
        if (!IsTransitioningState/*&& nextStateKey.Equals(CurrentState.StateKey)*/)
        {
            CurrentState.UpdateState();
        }
        currentKeyState = CurrentState.StateKey;
    }
    public void TransitionToState(EState stateKey, float delay = 0)
    {
        if (delay > 0)
        {
            StartCoroutine(TransitionCor(stateKey, delay));
        }
        else Transition(stateKey);
    }
    IEnumerator TransitionCor(EState stateKey, float time)
    {
        yield return new WaitForSeconds(time);
        Transition( stateKey);
    }
    void Transition(EState stateKey)
    {
        IsTransitioningState = true;
        CurrentState.ExiteState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTransitioningState = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }
   
    private void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }
}
