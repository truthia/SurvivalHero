using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPractiseState : BaseState<PlayerState>
{
    CharacterController CharacterController { set; get; }
    public PlayerPractiseState(PlayerState key, StateManager<PlayerState> stateManager) : base(key, stateManager)
    {
        CharacterController = StateManager.GetComponent<CharacterController>();
    }

    public override void EnterState()
    {
        PlayerController.Instance.attack.AimAtTarget();
        PlayerController.Instance.animator.SetFloat("Speed", 0);
        PlayerController.Instance.hub.hub.gameObject.SetActive(false);

    }

    public override void ExiteState()
    {
        PlayerController.Instance.attack.StopCombat();
        PlayerController.Instance.hub.hub.gameObject.SetActive(true);
        PlayerController.Instance.attack.visibleTarget = null;

        GameManager.Instance.cameraManager.ChangeCamera(CameraType.MainCamera);
        CharacterController.enabled = true;
        PlayerController.Instance.attack.visibleTarget = null;
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    public override void UpdateState()
    {
        if (PlayerController.Instance.attack.visibleTarget != null)
        {

            PlayerController.Instance.weaponCollection.UpdateWeapon();

        }
    }
}
