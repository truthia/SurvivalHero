using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : StateManager<GameState>
{
    private void Awake()
    {
        States.Add(GameState.Pause, new GamePauseState(GameState.Pause, this));
        States.Add(GameState.Wave, new GameWaveState(GameState.Wave, this));
        States.Add(GameState.LevelUp, new GameLevelUpState(GameState.LevelUp, this));
        States.Add(GameState.Idle, new GameIdleState(GameState.Idle, this));

        CurrentState = States[GameState.Idle];
    }
}
[System.Serializable]
public enum GameState
{
    Pause, Idle,Wave,LevelUp
}