using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelUpState : BaseState<GameState>
{
    public GameLevelUpState(GameState key, StateManager<GameState> stateManager) : base(key, stateManager)
    {
    }

    public override void EnterState()
    {
        UIManager.Instance.levelUpUI.gameObject.SetActive(true);
        UIManager.Instance.levelUpUI.ApplyUpgradeOption();
      //  Utilities.SpamAction(() => GameManager.Instance.PauseGame(), 1, 0.1f);
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
