using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        stats.CurrentSpeed = stats.characterData.MoveSpeed + passiveItemData.Multipler / 100f;
    }
    private void OnEnable()
    {
        ApplyModifier();
    }
}
