using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBotAttackState : BaseState<WaveBotState>
{
    WaveBotController WaveBotController { set; get; }
    public WaveBotAttackState(WaveBotState key, StateManager<WaveBotState> stateManager) : base(key, stateManager)
    {
        WaveBotController = StateManager.GetComponent<WaveBotController>();
    }

    public override void EnterState()
    {
    }

    public override void ExiteState()
    {
       
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.transform== WaveBotController.target)
        {
            StateManager.TransitionToState(WaveBotState.Chasing);
        }
    }

    public override void UpdateState()
    {
        WaveBotController.Attack();
    }
}
  
