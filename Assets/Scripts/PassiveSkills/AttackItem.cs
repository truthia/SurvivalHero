using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        stats.CurrentMight = stats.characterData.Might + passiveItemData.Multipler / 100f;
    }
    private void OnEnable()
    {
        ApplyModifier();
    }
}
