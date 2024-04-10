using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base weapon scripts
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponSO weaponData;
    [HideInInspector]
    public float currentCooldown;

    [HideInInspector] public Transform shooter;
    [HideInInspector] public Attack attack;
    [HideInInspector] public HeroStats stats;
    public virtual void Start()
    {
        currentCooldown = weaponData.CoodownDuration / 2;
    }
    public virtual void UpdateWeapon()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0)
        {
            Attack();
        }
    } public virtual void UpdateMovingWeapon()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0)
        {
            if (weaponData. CanCastWhenMove)
            {

                Attack();
            }
               
        }
    }

    public virtual void Attack()
    {
        currentCooldown = weaponData. CoodownDuration - weaponData.CoodownDuration*stats.CurrentCooldownReduce;
        if(!weaponData.CanCastWhenMove)
        PlayerController.Instance.animator.SetBool("Attack", true);
       
    }

}
