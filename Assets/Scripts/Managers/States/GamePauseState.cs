using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseState : BaseState<GameState>
{
    public GamePauseState(GameState key, StateManager<GameState> stateManager) : base(key, stateManager)
    {
    }

    public override void EnterState()
    {
        GameManager.Instance.PauseGame();
    }

    public override void ExiteState()
    {
        GameManager.Instance.ResumeGame();
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    public override void UpdateState()
    {
    }
}
