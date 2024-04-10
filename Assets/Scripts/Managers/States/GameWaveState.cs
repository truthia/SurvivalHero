using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaveState : BaseState<GameState>
{
    public GameWaveState(GameState key, StateManager<GameState> stateManager) : base(key, stateManager)
    {
    }

    public override void EnterState()
    {
        UIManager.Instance.startWaveBtn.SetActive(false);
        UIManager.Instance.killText.transform.parent.gameObject.SetActive(true);
        UIManager.Instance.waveText.transform.parent.gameObject.SetActive(false);
    }

    public override void ExiteState()
    {
        UIManager.Instance.startWaveBtn.SetActive(true);
       
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    public override void UpdateState()
    {
        GameManager.Instance.enemySpawner.UpdateWave();
    }
}
