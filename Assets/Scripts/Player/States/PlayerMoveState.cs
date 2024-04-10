using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Observer;

public class PlayerMoveState : BaseState<PlayerState>
{

    CharacterController CharacterController{ set;get;}
private PlayerController PlayerController { set;get;}
    public PlayerMoveState(PlayerState key, StateManager<PlayerState> stateManager) : base(key, stateManager)
    {
        PlayerController = PlayerController.Instance;
        CharacterController = PlayerController.GetComponent<CharacterController>();
    }

    public override void EnterState()
    {
      
   
    }

    public override void ExiteState()
    {
    }


    public override void OnTriggerEnter(Collider other)
    {
        /* if (other.transform.TryGetComponent(out ICollectible collectible))
         {
             collectible.Collect(PlayerController.Instance.heroStats);
         }*/
        if (other.CompareTag("Practise")&&GameManager.Instance.gameStateManager.currentKeyState==GameState.Idle)
        {
            StateManager.transform.position = other.GetComponent<PractiseInfo>().practisePos.position;
            GameManager.Instance.cameraManager.ChangeCamera(CameraType.Practise1);
            StateManager.TransitionToState(PlayerState.Practise);
            CharacterController.enabled = false;
            PlayerController.attack.visibleTarget = other.transform;
            UIManager.Instance.practiseUI.gameObject.SetActive(true);
            StateManager.transform.forward = other.transform.position - StateManager.transform.position;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
      
    }

    public override void UpdateState()
    {
        PlayerController.playerMovement.Movement();
        PlayerController.weaponCollection.UpdateMovingWeapon();
      
    }
}
