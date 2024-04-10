using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeaponBehaviour : WeaponBehaviour
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected abstract void DealAOEDamge();
}
