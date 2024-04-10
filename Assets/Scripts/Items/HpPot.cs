using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPot : MonoBehaviour, ICollectible
{
    public float amount = 20;
    public void Collect(HeroStats heroStats)
    {
        heroStats.RestoreHP(amount);
        gameObject.SetActive(false);
    }
}
