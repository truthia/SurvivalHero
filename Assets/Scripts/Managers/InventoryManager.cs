using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/// <summary>
/// Inventory system
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(5);
    public int[] weaponLevels = new int[5]; 
    public List<PassiveItem> passiveSlots = new List<PassiveItem>(5);
    public int[] passiveLevels = new int[5];
    public void CheckFirstWeaponLevel(int level)
    {
        bool doneWhile=false;
        while (!doneWhile)
        {
            WeaponController wc = weaponSlots[0];
            if (wc.weaponData.Level < level)
            {
                LevelUpWeapon(0);
            }
            else doneWhile = true;
        }
    }
    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        UIManager.Instance.pauseUI.weaponUISLots[slotIndex].sprite = weapon.weaponData.Icon;
        UIManager.Instance.levelUpUI.weaponUISLots[slotIndex].sprite = weapon.weaponData.Icon;
    }
    public void AddPassiveSkill(int slotIndex, PassiveItem passiveItem)
    {
        passiveSlots[slotIndex] = passiveItem;
        passiveLevels[slotIndex] = passiveItem.passiveItemData.Level;
        UIManager.Instance.pauseUI.passiveUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
        UIManager.Instance.levelUpUI.passiveUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
    }
    public void LevelUpWeapon(int slotIndex)
    {
        if (weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];

            WeaponController upgradeWeapon = PlayerController.Instance.weaponCollection.GetWeaponByNameID(weapon.weaponData.NextWeaponName);
            weapon.gameObject.SetActive(false);
            upgradeWeapon.gameObject.SetActive(true);
            AddWeapon(slotIndex, upgradeWeapon);
            weaponLevels[slotIndex] = upgradeWeapon.weaponData.Level;
        }
    }
    public void LevelUPPassiveSkill(int slotIndex)
    {
        if (passiveSlots.Count > slotIndex)
        {
            PassiveItem passive = passiveSlots[slotIndex];
            PassiveItem upgradePassive = PlayerController.Instance.passiveCollection.GetPassiveByNameID(passive.passiveItemData.NextPassiveName);
          
            passive.gameObject.SetActive(false);
            upgradePassive.gameObject.SetActive(true);
            AddPassiveSkill(slotIndex, upgradePassive);
            passiveLevels[slotIndex] = upgradePassive.passiveItemData.Level;
        }
    }
    public void ChooseFirstWeapon(WeaponController weaponController)
    {
       /* weaponSlots.Clear();
        passiveSlots.Clear();*/
        /*    UIManager.Instance.pauseUI.weaponUISLots[slotIndex].sprite = weapon.weaponData.Icon;
            UIManager.Instance.levelUpUI.weaponUISLots[slotIndex].sprite */
        foreach (Image i in UIManager.Instance.pauseUI.weaponUISLots)
        {
            i.sprite = null;
        }
        foreach (Image i in UIManager.Instance.levelUpUI.weaponUISLots)
        {
            i.sprite = null;
        }    foreach (Image i in UIManager.Instance.pauseUI.passiveUISlots)
        {
            i.sprite = null;
        }
        foreach (Image i in UIManager.Instance.levelUpUI.passiveUISlots)
        {
            i.sprite = null;
        }  
        for(int i = 0; i < weaponSlots.Count; i++)
        {
            weaponSlots[i] = null;
        }  for(int i = 0; i < passiveSlots.Count; i++)
        {
            passiveSlots[i] = null;
        }
        foreach (WeaponController wc in PlayerController.Instance.weaponCollection.weaponControllers)
        {
            wc.gameObject.SetActive(false);
            if (wc == weaponController) wc.gameObject.SetActive(true);
        }
        foreach (PassiveItem wc in PlayerController.Instance.passiveCollection.passives)
        {
            wc.gameObject.SetActive(false);
        }
      
        AddWeapon(0, weaponController);
     //   CheckFirstWeaponLevel(playerSkillLevel);
    }
    public bool CheckIfWeaponFull()
    {
        return (weaponSlots.FindAll(e => e == null).Count == 0&&weaponSlots.FindAll(e=>e.weaponData.Level<5).Count==0);
    } public bool CheckIfPassiveFull()
    {
        return (passiveSlots.FindAll(e => e == null).Count == 0 && passiveSlots.FindAll(e => e.passiveItemData.Level < 5).Count == 0);
    }
}
