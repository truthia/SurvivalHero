using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState<PlayerState>
{

    private PlayerController PlayerController { set; get; }
    public PlayerIdleState(PlayerState key, StateManager<PlayerState> stateManager) : base(key, stateManager)
    {
        PlayerController = PlayerController.Instance;
     
    }

    public override void EnterState()
    {
          if (GameManager.Instance.gameStateManager.currentKeyState == GameState.Idle) return;
        PlayerController.attack.StartCombat();
    }

    public override void ExiteState()
    {
       
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerExit(Collider other)
    {
    }


    public override void UpdateState()
    {
        PlayerController.playerMovement.Idle();
        if (GameManager.Instance.gameStateManager.currentKeyState == GameState.Idle) return;
        if (PlayerController.attack.visibleTarget != null)
        {

            StateManager.TransitionToState(PlayerState.Attack);
        }
    }
}
