using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        stats.CurrentMaxHP = stats.characterData.MaxHealth+ stats.characterData.MaxHealth* passiveItemData.Multipler/100;
    }

    private void OnEnable()
    {
        ApplyModifier();
    }
}
