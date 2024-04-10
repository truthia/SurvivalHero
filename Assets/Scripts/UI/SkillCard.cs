using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    public SkillCardType type;
    public GameObject bgActive;
    public GameObject bgPassive;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI descriptionText;
    public Image icon;
    public GameObject[] stars;

    public int index;
    public string stringID;
   
    public void SetCardType(SkillCardType _type)
    {
        type = _type;
        ResetBG();
        switch (type)
        {
            case SkillCardType.UpgradeActive:
            case SkillCardType.GetSkillActive:
                bgActive.SetActive(true);
                break;
            case SkillCardType.UpgradePassive:
            case SkillCardType.GetSkillPassive:
                bgPassive.SetActive(true);
                break;
        }
    }
   
    void ResetBG()
    {
        bgActive.SetActive(false);
        bgPassive.SetActive(false);
      
    }
    public void OnCardClick()
    {
        switch (type)
        {
            case SkillCardType.UpgradeActive:
                PlayerController.Instance.inventory.LevelUpWeapon(index);
                break;
        
            case SkillCardType.GetSkillActive:
                PlayerController.Instance.heroStats.ActivateSkill(PlayerController.Instance.weaponCollection.GetWeaponByNameID(stringID));
                break;

            case SkillCardType.UpgradePassive:
                PlayerController.Instance.inventory.LevelUPPassiveSkill(index);
                break;

            case SkillCardType.GetSkillPassive:
                PlayerController.Instance.heroStats.ActivatePassive(PlayerController.Instance.passiveCollection.GetPassiveByNameID(stringID));
                break;
        }
    }
    public void SetCardInfor(WeaponSO data)
    {
       descriptionText.text = data.Description;
       cardNameText.text = data.DisplayName;
       icon.sprite = data.Icon;
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
        for (int i=0; i < data.Level; i++)
        {
            stars[i].SetActive(true);
        }
     //   gameObject.SetActive(true);
    } 
    public void SetCardInfor(PassiveItemSO data)
    {
       descriptionText.text = data.Description;
       cardNameText.text = data.DisplayName;
       icon.sprite = data.Icon;
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
        for (int i = 0; i < data.Level; i++)
        {
            stars[i].SetActive(true);
        }
    //    gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        descriptionText.text = "DMMMMM";
    }
}
[System.Serializable]
public enum SkillCardType
{
    UpgradeActive, GetSkillActive,UpgradePassive,GetSkillPassive
}