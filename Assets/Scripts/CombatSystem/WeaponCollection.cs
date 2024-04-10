using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollection : MonoBehaviour
{
    public WeaponController[] weaponControllers;
    private void Awake()
    {
        SetWeaponShooter();
    }

    private void SetWeaponShooter()
    {
        /*fireBallController.shooter = transform.parent;
        PowerWaveController.shooter = transform.parent;*/
        foreach(WeaponController wc in weaponControllers)
        {
            wc.shooter = transform.parent;
            wc.attack = transform.parent.GetComponent<Attack>();
            wc.stats = transform.parent.GetComponent<HeroStats>();
        }
    }

    public void UpdateWeapon()
    {
        foreach (WeaponController wc in weaponControllers)
        {
            if(wc.gameObject.activeInHierarchy)
                wc.UpdateWeapon();
        }
    }  public void UpdateMovingWeapon()
    {
        foreach (WeaponController wc in weaponControllers)
        {
            if (wc.gameObject.activeInHierarchy)
                wc.UpdateMovingWeapon();
        }
    }
    public void ResetWPCD()
    {
        foreach (WeaponController wc in weaponControllers)
        {
            if (wc.gameObject.activeInHierarchy)
            {
                wc.currentCooldown += 0.5f;
            }
        }
    }
    public WeaponController GetWeaponByNameID(string id)
    {
        foreach (WeaponController wc in weaponControllers)
        {
            if (String.Equals(wc.weaponData.WeaponName, id, StringComparison.Ordinal))
            {
                return wc;
            }
        }
        return null;
    }
}
