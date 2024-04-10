using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : WeaponController
{
  

    public override void Attack()
    {
        base.Attack();
        CallMeteor();
        if (weaponData.Level >= 2) Invoke(nameof(CallMeteor),0.5f);
        if (weaponData.Level >= 4) Invoke(nameof(CallMeteor), 1f);
        if (weaponData.Level >= 5)
        {
            Invoke(nameof(CallMeteor), 1.5f);
            Invoke(nameof(CallMeteor), 2f);
        }
    }

    private void CallMeteor()
    {
        if (attack.listTarget.Count <= 0) return; 
        GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, attack.listTarget[Random.Range(0,attack.listTarget.Count)].position);
        MeteorBehaviour sb = b.GetComponent<MeteorBehaviour>();
        sb.shooter = shooter;
        sb.SetUpStats();
    }
}
