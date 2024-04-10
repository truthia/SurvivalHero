using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PractiseUI : MonoBehaviour
{
    public TextMeshProUGUI skillName;
    public Image progressFill;
    public Image Icon;
    public TextMeshProUGUI textPrice;
    public TextMeshProUGUI requireText;
    public UpgradeBtnState btnState;
    public List<int> boughtTools;
    public Tool currentEquip;

    public GameObject pagePrefab;
    public GameObject cardPrefab;
    public Transform cardContainer;
    List<ToolCard> toolCards;
    private void OnEnable()
    {
        SetUpUpgradeBtn();
        SetUpToolCards();
    }

    private void SetUpToolCards()
    {
        List<Tool> tools = DataManager.Instance.dataGame.Tool;
        toolCards = new List<ToolCard>();
        int numberItem = 0;
        Transform page = Instantiate(pagePrefab, cardContainer).transform;
        for (int i = 0; i < tools.Count; i++)
        {
            numberItem++;
            if (numberItem > 2)
            {
                numberItem = 1;
                page = Instantiate(pagePrefab, cardContainer).transform;
            }
            ToolCard toolCard = Instantiate(cardPrefab, page).GetComponent<ToolCard>();
            toolCard.SetupBtn(tools[i]);
            toolCards.Add(toolCard);
        }
        currentEquip = tools[0];
        toolCards[0].OnCardClick();
    }

    void SetUpgradeButtonState(PlayerLevel playerLevel)
    {
       
        if(GameManager.Instance.Wave < playerLevel.UnlockRequire)
        {
            
            btnState = UpgradeBtnState.WaveRequied;
        }
        else if (GameManager.Instance.Money >= playerLevel.SkillCost)
        {
            btnState = UpgradeBtnState.CanUpgrade;
            
        }
        else
        {
            btnState = UpgradeBtnState.NotEnoughCoin;
        }
        if (PlayerController.Instance.heroStats.TrainningLevel >= DataManager.Instance.dataGame.PlayerLevel.Count)
        {
            btnState = UpgradeBtnState.Maxed;
        }

    }
    public void SetUpUpgradeBtn()
    {
        PlayerLevel playerLevel = DataManager.Instance.dataGame.PlayerLevel[PlayerController.Instance.heroStats.TrainningLevel];
        SetUpgradeButtonState(playerLevel);
        string currentSkillName = PlayerController.Instance.heroStats.currentPlayerLevel.SkillName;
        int numberOfSameLevel = DataManager.Instance.dataGame.PlayerLevel.FindAll(p => Utilities.CompareString(p.SkillName,currentSkillName)).Count;
        int numberOfLowerLevel = DataManager.Instance.dataGame.PlayerLevel.FindAll(p => p.UnlockRequire<PlayerController.Instance.heroStats.currentPlayerLevel.UnlockRequire).Count; ;
      
        progressFill.DOFillAmount((float)(PlayerController.Instance.heroStats.TrainningLevel-numberOfLowerLevel)  / numberOfSameLevel, 0.25f);
        switch (btnState)
        {
            case UpgradeBtnState.WaveRequied:
                textPrice.gameObject.SetActive(false);
                requireText.gameObject.SetActive(true);
                requireText.text = $"Clear Wave {playerLevel.UnlockRequire} to advance";
                break;
            case UpgradeBtnState.CanUpgrade:
                textPrice.gameObject.SetActive(true);
                requireText.gameObject.SetActive(false);
                textPrice.text = Utilities.ConvertToKMB(playerLevel.SkillCost);
                break; 
            case UpgradeBtnState.NotEnoughCoin:
                textPrice.gameObject.SetActive(true);
                requireText.gameObject.SetActive(false);
                textPrice.text = Utilities.ConvertToKMB(playerLevel.SkillCost);
                break;
            case UpgradeBtnState.Maxed:
                textPrice.gameObject.SetActive(false);
                requireText.gameObject.SetActive(true);
                requireText.text = $"You've reached maximun Level";
                break;
        }
        skillName.text = PlayerController.Instance.inventory.weaponSlots[0].weaponData.DisplayName;
        Icon.sprite = PlayerController.Instance.inventory.weaponSlots[0].weaponData.Icon;
      
    }
   public void BackToNormal()
    {
       // gameObject.SetActive(false);
        PlayerController.Instance.stateManager.TransitionToState(PlayerState.Idle);
    }
    public void Upgrade()
    {
        if (PlayerController.Instance.heroStats.TrainningLevel >= DataManager.Instance.dataGame.PlayerLevel.Count) return;
        PlayerLevel playerLevel = DataManager.Instance.dataGame.PlayerLevel[PlayerController.Instance.heroStats.TrainningLevel ];
        switch (btnState)
        {
            case UpgradeBtnState.WaveRequied:
               
                break;
            case UpgradeBtnState.CanUpgrade:
                BuyUpgrade(playerLevel);
                break; 
            case UpgradeBtnState.NotEnoughCoin:
        
                break;
            case UpgradeBtnState.Maxed:
              
                break;
        }
        SetUpUpgradeBtn();
    }

    private static void BuyUpgrade(PlayerLevel playerLevel)
    {
        GameManager.Instance.Money -= playerLevel.SkillCost;

        PlayerController.Instance.heroStats.SetUpStats(playerLevel, PlayerController.Instance.heroStats.TrainningLevel + 1);
        WeaponSO weaponData = PlayerController.Instance.inventory.weaponSlots[0].weaponData;
        if (PlayerController.Instance.weaponCollection.GetWeaponByNameID(playerLevel.SkillName).weaponData.Level > weaponData.Level)
        {
            PlayerController.Instance.inventory.LevelUpWeapon(0);
        }
    }
    public void UpdateAllCard()
    {
        foreach(ToolCard t in toolCards)
        {
           t.SetupBtn();
        }
    }
}
[System.Serializable]
public enum UpgradeBtnState
{
    CanUpgrade,
    NotEnoughCoin,
    WaveRequied,
    Maxed,
}