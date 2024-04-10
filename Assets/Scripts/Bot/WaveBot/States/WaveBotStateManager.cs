using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBotStateManager : StateManager<WaveBotState>
{

    private void Awake()
    {
        States.Add(WaveBotState.Chasing, new WaveBotChasingState(WaveBotState.Chasing, this));
        States.Add(WaveBotState.Attack, new WaveBotAttackState(WaveBotState.Attack, this));

        CurrentState = States[WaveBotState.Chasing];
    }
}
[System.Serializable]
public enum WaveBotState
{
    Chasing,Die,
    Attack
}