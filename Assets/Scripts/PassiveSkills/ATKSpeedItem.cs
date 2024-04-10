using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKSpeedItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        stats.CurrentCooldownReduce = passiveItemData.Multipler / 100;
    }
    private void OnEnable()
    {
        ApplyModifier();
    }
}
