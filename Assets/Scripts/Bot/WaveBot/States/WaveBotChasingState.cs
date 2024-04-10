
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBotChasingState : BaseState<WaveBotState>
{
    WaveBotController WaveBotController { set; get; }
    public WaveBotChasingState(WaveBotState key, StateManager<WaveBotState> stateManager) : base(key, stateManager)
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
        if (other.CompareTag("Player"))
        {
            WaveBotController.target = other.transform;
            StateManager.TransitionToState(WaveBotState.Attack);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
    }



    public override void UpdateState()
    {
        WaveBotController.Movement();
    }
}
