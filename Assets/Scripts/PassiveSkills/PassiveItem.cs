using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveItem : MonoBehaviour
{
    public HeroStats stats;
    public PassiveItemSO passiveItemData;
    protected abstract void ApplyModifier();
  
}
