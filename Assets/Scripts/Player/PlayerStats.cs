using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : HeroStats
{
    [HideInInspector]
    public int weaponIndex;
    [HideInInspector]
    public int passiveIndex;
    [HideInInspector]
    public PlayerLevel currentPlayerLevel;
    public int TrainningLevel = 1;
    public float idleGain;
    Attack attack;
    protected override void Awake()
    {
        base.Awake();
        attack = GetComponent<Attack>();
    }
    public override void SetUpStats(PlayerLevel playerLevel,int _trainningLevel)
    {
        Awake();
        currentPlayerLevel = playerLevel;
        CurrentDMG = playerLevel.SkillDmg;
        CurrentMaxHP = characterData.MaxHealth;
        idleGain = playerLevel.Idle;
        TrainningLevel = _trainningLevel;
        Level = 1;
        experienceCap = LevelRanges[0].experienceCapIncrease;
    }
    protected override void Start()
    {
        base.Start();

    }
    public override void Kill()
    {
        PlayerController.Instance.characterController.enabled = false;
        PlayerController.Instance.weaponCollection.gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
    }
    Tween pushTween;
    public override void Push(Vector3 direction, float force)
    {
        PlayerController.Instance.characterController.enabled = false;
        if (pushTween.IsActive()&& currentHealth > 0)
            pushTween.Kill();
        pushTween = transform.DOMove(transform.position + direction * force, 0.2f).SetEase(Ease.OutQuad).OnComplete(() => {
            PlayerController.Instance.characterController.enabled = true;
            if (currentHealth <= 0)
            {
                Kill();
            }
        });
    }

    public override void TakeDamage(float dmg, Transform hitby)
    {

        if (pushTween.IsActive())
        {
           /* if (!pushTween.IsPlaying())
                currentHealth -= dmg;*/
        }
        else 
        {
            if (attack.shield <= 0)
            {
                CurrentHealth -= dmg;
                PlayerController.Instance.animator.SetTrigger("GotHit");
            }
            else
            {
                attack.shield--;
            }
        }
       


        Vector3 direction = transform.position - hitby.position;
        direction.y = 0;
        direction = direction.normalized;
        Push(direction, 3f);
    }

    public override void ActivateSkill(WeaponController weaponController)
    {

        if (weaponIndex >= PlayerController.Instance.inventory.weaponSlots.Count )
        {
            Debug.Log("inventory full");
            return;
        }
        foreach (WeaponController wc in PlayerController.Instance.weaponCollection.weaponControllers)
        {
            if (String.Equals(weaponController.weaponData.WeaponName, wc.weaponData.WeaponName, StringComparison.Ordinal) )
            {
                wc.gameObject.SetActive(true);
                PlayerController.Instance.inventory.AddWeapon(weaponIndex, weaponController);
                weaponIndex++;
                break;
            }
        }
        
    } 
    public override void ActivatePassive(PassiveItem passiveItem)
    {

        if (passiveIndex >= PlayerController.Instance.inventory.passiveSlots.Count )
        {
            Debug.Log("inventory full");
            return;
        }
        foreach (PassiveItem pi in PlayerController.Instance.passiveCollection.passives)
        {
            if (String.Equals(pi.passiveItemData.PassiveName, passiveItem.passiveItemData.PassiveName, StringComparison.Ordinal))
            {
                pi.gameObject.SetActive(true);
                PlayerController.Instance.inventory.AddPassiveSkill(passiveIndex, passiveItem);
                passiveIndex++;
                break;
            }
        }
        
    }

    public override void RestoreHP(float amount)
    {
        CurrentHealth += amount;
      /*  if(CurrentHealth> characterData.h)*/
    }

    public override void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += currentRecovery * Time.deltaTime;
        }
    }

    public override void OnHealthChange()
    {
        PlayerController.Instance.hub.hub.fill.DOFillAmount(CurrentHealth / characterData.MaxHealth,0.25f);
    }

    public override void OnMightChange()
    {
       
    }

   
}
