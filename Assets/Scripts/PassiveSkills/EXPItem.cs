using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        stats.CurrentEXPBonus = passiveItemData.Multipler;
    }
    private void OnEnable()
    {
        ApplyModifier();
    }
}
