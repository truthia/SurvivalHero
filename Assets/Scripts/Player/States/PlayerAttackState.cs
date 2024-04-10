using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState<PlayerState>
{
   
    public PlayerAttackState(PlayerState key, StateManager<PlayerState> stateManager) : base(key, stateManager)
    {
      
    }

    public override void EnterState()
    {
        PlayerController.Instance.weaponCollection.ResetWPCD();
     
    }

    public override void ExiteState()
    {
        PlayerController.Instance.animator.SetTrigger("BackToMove");
        PlayerController.Instance.animator.SetBool("Attack",false);
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    public override void UpdateState()
    {
        if (PlayerController.Instance.attack.AimAtTarget())
        {

            StateManager.TransitionToState(PlayerState.Idle);
        }
        PlayerController.Instance.playerMovement.Idle();
        PlayerController.Instance.weaponCollection.UpdateWeapon();
       
    }
}
