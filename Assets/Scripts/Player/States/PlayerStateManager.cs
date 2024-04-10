using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : StateManager<PlayerState>
{
    private void Awake()
    {
        States.Add(PlayerState.Idle, new PlayerIdleState(PlayerState.Idle, this));
        States.Add(PlayerState.Move, new PlayerMoveState(PlayerState.Move, this));
        States.Add(PlayerState.Attack, new PlayerAttackState(PlayerState.Attack, this)) ;
        States.Add(PlayerState.Practise, new PlayerPractiseState(PlayerState.Practise, this)) ;
        CurrentState = States[PlayerState.Idle];

    }
}
[System.Serializable]
public enum PlayerState{
    Idle,Move,Attack,Practise
}
