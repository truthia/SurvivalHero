using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Test")]
    public int startArms;
    [HideInInspector]
    public PlayerStateManager stateManager; 
    [HideInInspector]
    public PlayerMovement playerMovement;   
    [HideInInspector]
    public Attack attack;

    [HideInInspector]
    public PlayerStats heroStats;
  
    [HideInInspector]
    public CharacterController characterController;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public InventoryManager inventory; 
    [HideInInspector]
    public HubController hub;

    [Header("Codes")]
    public WeaponCollection weaponCollection;
    public PassiveCollection passiveCollection;
    public PlayerCollector collector;
    public SkinManager skinManager;
    protected override void Awake()
    {
        base.Awake();
        stateManager = GetComponent<PlayerStateManager>();
        playerMovement = GetComponent<PlayerMovement>();
        attack = GetComponent<Attack>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        heroStats = GetComponent<PlayerStats>();
        inventory = GetComponent<InventoryManager>();
        hub = GetComponent<HubController>();
    }
    private void Start()
    {
       // inventory.ChooseFirstWeapon(weaponCollection.GetWeaponByNameID(heroStats.currentPlayerLevel.SkillName));
        // heroStats.ActivateSkill(weaponCollection.weaponControllers[1]);
        //  heroStats.ActivatePassive(passiveCollection.passives[0]);
        //   heroStats.ActivatePassive(passiveCollection.passives[5]);

        skinManager.ChangeArm(GameManager.Instance.prefabContainer.hands[startArms], transform.GetChild(0).gameObject);
    }
}
