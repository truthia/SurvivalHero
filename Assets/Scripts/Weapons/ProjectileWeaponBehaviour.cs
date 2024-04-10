using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base script of all projectile
/// </summary>
public class ProjectileWeaponBehaviour : WeaponBehaviour
{
   
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private void Awake()
    {
      
        currentSpeed = weaponData.Speed;
        currrentPiece = weaponData.Pirece;
        currentCooldownDuration = weaponData.CoodownDuration;
    }
   

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {


            CheckPirece();
            if (other.transform.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(GetCurrentDamage(), transform);
            }
           
            ObjectPooler.instance.SpawnFromPool("EndFireBall", transform.position);
        }
        if (other.CompareTag("Dummy"))
        {
          
            CheckPirece();
            GameManager.Instance.Money += PlayerController.Instance.heroStats.idleGain;
            ObjectPooler.instance.SpawnFromPool("EndFireBall", transform.position);
        }
    }

    protected void CheckPirece()
    {
        currrentPiece--;
        if (currrentPiece <= 0) gameObject.SetActive(false);
    }
}
