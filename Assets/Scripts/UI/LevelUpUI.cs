using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelUpUI : MonoBehaviour
{
    public Image[] weaponUISLots;
    public Image[] passiveUISlots;

    [System.Serializable]
    public class WeaponUpgrade
    {
       
        public string initWeaponName;
        public WeaponSO weaponData;
    }
    [System.Serializable]
    public class PassiveUpgrade
    {
 
        public string initPassiveName;
        public PassiveItemSO passiveData;
    }
    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveUpgrade> passiveUpgradeOptions = new List<PassiveUpgrade>();
    public List<SkillCard> skillCards = new List<SkillCard>();
    
    private void OnDisable()
    {
        GameManager.Instance.gameStateManager.TransitionToState(GameState.Wave);
    }
    int CheckWeaponUpgradeList(List<WeaponUpgrade> availableWeaponUpgrades)
    {
        if (availableWeaponUpgrades.Count == 0) return 0;
        bool maxSlot = (PlayerController.Instance.inventory.weaponSlots.FindAll(e => e == null).Count == 0);
        for (int j=0; j <availableWeaponUpgrades.Count;j++)
        {
            bool newone=false;
            for (int i = 0; i < PlayerController.Instance.inventory.weaponSlots.Count; i++)
            {

                if (PlayerController.Instance.inventory.weaponSlots[i] != null && PlayerController.Instance.inventory.weaponSlots[i].weaponData.id == availableWeaponUpgrades[j].weaponData.id)
                {
                    newone = false;

                    if (Utilities.CompareString(PlayerController.Instance.inventory.weaponSlots[i].weaponData.NextWeaponName, ""))
                    {
                        availableWeaponUpgrades[j] = null;
                       
                    }
                   
                    break;
                }
                else if(PlayerController.Instance.inventory.weaponSlots[i] != null)
                {
                    newone = true;
                }
            }
            if(newone&& maxSlot)
            {
                availableWeaponUpgrades[j] = null;
            }
        }
        availableWeaponUpgrades.RemoveAll(item => item == null);
        return availableWeaponUpgrades.Count;
    }  int CheckPassiveUpgradeList(List<PassiveUpgrade> passiveUpgrades)
    {
        if (passiveUpgrades.Count == 0) return 0;
        bool maxSlot = (PlayerController.Instance.inventory.passiveSlots.FindAll(e => e == null).Count == 0);
        for (int j=0; j < passiveUpgrades.Count;j++)
        {
            bool newone=false;
            for (int i = 0; i < PlayerController.Instance.inventory.passiveSlots.Count; i++)
            {

                if (PlayerController.Instance.inventory.passiveSlots[i] != null && PlayerController.Instance.inventory.passiveSlots[i].passiveItemData.id == passiveUpgrades[j].passiveData.id)
                {
                    newone = false;

                    if (Utilities.CompareString(PlayerController.Instance.inventory.passiveSlots[i].passiveItemData.NextPassiveName, ""))
                    {
                        passiveUpgrades[j] = null;
                       
                    }
                   
                    break;
                }
                else if(PlayerController.Instance.inventory.passiveSlots[i] != null)
                {
                    newone = true;
                }
            }
            if(newone&& maxSlot)
            {
                passiveUpgrades[j] = null;
            }
        }
        passiveUpgrades.RemoveAll(item => item == null);
        return passiveUpgrades.Count;
    }
    public void ApplyUpgradeOption()
    {
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<PassiveUpgrade> availablePassiveUpgrades = new List<PassiveUpgrade>(passiveUpgradeOptions);
        bool weaponFull = PlayerController.Instance.inventory.CheckIfWeaponFull();
        bool passiveFull = PlayerController.Instance.inventory.CheckIfPassiveFull();
        foreach (var upgradeOption in skillCards)
        {
            int disabled = 0;
            if ((availablePassiveUpgrades.Count == 0 && availableWeaponUpgrades.Count == 0)||weaponFull
                && passiveFull)
            {
               
                return;
            }
            int upgradeType;
            if (availableWeaponUpgrades.Count == 0|| weaponFull)
            {
                upgradeType = 1;
            }
            else if (availablePassiveUpgrades.Count == 0||passiveFull)
            {
                upgradeType = 0;
            }
            else
            {
                upgradeType = Random.Range(0, 2);
            }
            if (upgradeType == 0)
            {
                int count=  CheckWeaponUpgradeList(availableWeaponUpgrades);
                if (count > 0)
                {
                    WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];
                    availableWeaponUpgrades.Remove(chosenWeaponUpgrade);

                    EnableSkillCard(upgradeOption);
                    bool newWeapon = false;


                    for (int i = 0; i < PlayerController.Instance.inventory.weaponSlots.Count; i++)
                    {

                        if (PlayerController.Instance.inventory.weaponSlots[i] != null && PlayerController.Instance.inventory.weaponSlots[i].weaponData.id == chosenWeaponUpgrade.weaponData.id)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {
                                upgradeOption.SetCardType(SkillCardType.UpgradeActive);
                                upgradeOption.index = i;

                                WeaponSO data = PlayerController.Instance.weaponCollection.GetWeaponByNameID
                                   (PlayerController.Instance.inventory.weaponSlots[i].weaponData.NextWeaponName).weaponData;
                                upgradeOption.SetCardInfor(data);
                                break;

                            }

                        }
                        else
                        {
                            newWeapon = true;
                        }

                    }
                    if (newWeapon)
                    {
                        upgradeOption.SetCardType(SkillCardType.GetSkillActive);
                        upgradeOption.stringID = chosenWeaponUpgrade.initWeaponName;
                        WeaponSO data = PlayerController.Instance.weaponCollection.GetWeaponByNameID
                                   (chosenWeaponUpgrade.weaponData.WeaponName).weaponData;
                        upgradeOption.SetCardInfor(data);
                    }
                }
                else
                {
                    DisableSkillCard(upgradeOption);
                    disabled++;
                    if (disabled >= 3)
                    {
                      
                    }
                }
            }
            else if (upgradeType == 1)
            {
                int count = CheckPassiveUpgradeList(availablePassiveUpgrades);
                if (count > 0)
                {
                    PassiveUpgrade chosenPassiveUpgrade = availablePassiveUpgrades[Random.Range(0, availablePassiveUpgrades.Count)];
                    availablePassiveUpgrades.Remove(chosenPassiveUpgrade);

                    EnableSkillCard(upgradeOption);
                    bool newWeapon = false;


                    for (int i = 0; i < PlayerController.Instance.inventory.passiveSlots.Count; i++)
                    {

                        if (PlayerController.Instance.inventory.passiveSlots[i] != null && PlayerController.Instance.inventory.passiveSlots[i].passiveItemData.id == chosenPassiveUpgrade.passiveData.id)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {
                                upgradeOption.SetCardType(SkillCardType.UpgradePassive);
                                upgradeOption.index = i;

                                PassiveItemSO data = PlayerController.Instance.passiveCollection.GetPassiveByNameID
                                   (PlayerController.Instance.inventory.passiveSlots[i].passiveItemData.NextPassiveName).passiveItemData;
                                upgradeOption.SetCardInfor(data);
                                break;

                            }

                        }
                        else
                        {
                            newWeapon = true;
                        }

                    }
                    if (newWeapon)
                    {
                        upgradeOption.SetCardType(SkillCardType.GetSkillPassive);
                        upgradeOption.stringID = chosenPassiveUpgrade.initPassiveName;
                        PassiveItemSO data = PlayerController.Instance.passiveCollection.GetPassiveByNameID
                                   (chosenPassiveUpgrade.passiveData.PassiveName).passiveItemData;
                        upgradeOption.SetCardInfor(data);
                    }
                }
                else
                {
                    DisableSkillCard(upgradeOption);
                    disabled++;
                    if (disabled >= 3)
                    {

                    }
                }
            }
        }
        DelayPauseGame();
      //  Invoke(nameof(DelayPauseGame), 0.1f);
    }

    private void DelayPauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    void DisableSkillCard(SkillCard card)
    {
        card.gameObject.SetActive(false);
    } void EnableSkillCard(SkillCard card)
    {
        card.gameObject.SetActive(true);
    }
    public void Reroll()
    {
        ApplyUpgradeOption();
    }
}
