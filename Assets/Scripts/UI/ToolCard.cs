using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolCard : MonoBehaviour
{
    public ToolCardState state;
    public TextMeshProUGUI textEquip;
    public TextMeshProUGUI textPrice;
    public GameObject textAds;
    public TextMeshProUGUI textPercent;
    public Image icon;
    public GameObject bg_Buy;
    public GameObject bg_Ads;
    public GameObject bg_Equipped;
    public GameObject bg_Equip;
    public Tool toolInfo;
    [System.Serializable]
    public enum ToolCardState
    {
        Buy,Ads,Equipped,Equip,CantBuy
    }
    public void SetupBtn(Tool tool)
    {
        toolInfo = tool;
        CheckCardStatus();
        textPercent.text = (toolInfo.Bonus * 100).ToString()+"%";
        switch (state)
        {
            case ToolCardState.Buy:
                ActiveBG(bg_Buy);
                ActiveText(textPrice.gameObject);
                textPrice.text = Utilities.ConvertToKMB(toolInfo.Cost);
                break;
            case ToolCardState.Ads:
                ActiveBG(bg_Ads);
                ActiveText(textAds);
               
                break;
            case ToolCardState.CantBuy:
                ActiveBG(bg_Buy);
                ActiveText(textPrice.gameObject);
                textPrice.text = Utilities.ConvertToKMB(toolInfo.Cost);
                break;
            case ToolCardState.Equip:
                ActiveBG(bg_Equip);
                ActiveText(textEquip.gameObject);
                textEquip.text = "Equip";
                break;
            case ToolCardState.Equipped:
                ActiveBG(bg_Equipped);
                ActiveText(textEquip.gameObject);
                textEquip.text = "Equipped";
                break;

        }
    } 
    public void SetupBtn()
    {
        CheckCardStatus();
        textPercent.text = (toolInfo.Bonus * 100).ToString()+"%";
        switch (state)
        {
            case ToolCardState.Buy:
                ActiveBG(bg_Buy);
                ActiveText(textPrice.gameObject);
                textPrice.text = Utilities.ConvertToKMB(toolInfo.Cost);
                break;
            case ToolCardState.Ads:
                ActiveBG(bg_Ads);
                ActiveText(textAds);
               
                break;
            case ToolCardState.CantBuy:
                ActiveBG(bg_Buy);
                ActiveText(textPrice.gameObject);
                textPrice.text = Utilities.ConvertToKMB(toolInfo.Cost);
                break;
            case ToolCardState.Equip:
                ActiveBG(bg_Equip);
                ActiveText(textEquip.gameObject);
                textEquip.text = "Equip";
                break;
            case ToolCardState.Equipped:
                ActiveBG(bg_Equipped);
                ActiveText(textEquip.gameObject);
                textEquip.text = "Equipped";
                break;

        }
    }
    public void OnCardClick()
    {
        switch (state)
        {
            case ToolCardState.Buy:
                GameManager.Instance.Money -= toolInfo.Cost;
                UIManager.Instance.practiseUI.boughtTools.Add(toolInfo.STT);
                UIManager.Instance.practiseUI.currentEquip = toolInfo;
                UIManager.Instance.practiseUI.UpdateAllCard();
                break;
            case ToolCardState.Ads:
                UIManager.Instance.practiseUI.boughtTools.Add(toolInfo.STT);
                UIManager.Instance.practiseUI.currentEquip = toolInfo;
                UIManager.Instance.practiseUI.UpdateAllCard();
                break;
            case ToolCardState.CantBuy:
               //Say something
                break;
            case ToolCardState.Equip:
                UIManager.Instance.practiseUI.currentEquip = toolInfo;
                UIManager.Instance.practiseUI.UpdateAllCard();
                break;
            case ToolCardState.Equipped:
               //say something
                break;

        }
        SetupBtn(toolInfo);
    }
    void ActiveText(GameObject text)
    {
        textEquip.gameObject.SetActive(false);
        textPrice.gameObject.SetActive(false); 
        textAds.gameObject.SetActive(false); 
        textPercent.gameObject.SetActive(false); 

        text.SetActive(true);
    }
    void ActiveBG(GameObject bg)
    {
        bg_Buy.gameObject.SetActive(false);
        bg_Ads.gameObject.SetActive(false);
        bg_Equipped.gameObject.SetActive(false);
        bg_Equip.gameObject.SetActive(false);

        bg.SetActive(true);
    }
    void CheckCardStatus()
    {
        if(toolInfo.Cost<= GameManager.Instance.Money)
        {
            state = ToolCardState.Buy;
        }
        else if(toolInfo.STT%3==0 && toolInfo.STT <8)
        {
            state = ToolCardState.Ads;
        }
        else
        {
            state = ToolCardState.CantBuy;
        }
        if (UIManager.Instance.practiseUI.boughtTools.Contains(toolInfo.STT))
        {
            if(UIManager.Instance.practiseUI.currentEquip.STT == toolInfo.STT)
            {
                state = ToolCardState.Equipped;
            }
            else
            {
                state = ToolCardState.Equip;
            }
        }
    }
   
}
