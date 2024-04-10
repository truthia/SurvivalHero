using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingStrikeController : WeaponController
{

    public override void Attack()
    {
        base.Attack();
        DelayAttack();
        if (weaponData.Level >= 2) DelayAttack(); ;
        if (weaponData.Level >= 4) DelayAttack(); ;
        if (weaponData.Level >= 5)
        {
            DelayAttack(); ;
            DelayAttack(); ;
        }
    }

    private void DelayAttack()
    {
        if (attack.listTarget.Count <= 0) return;
        Transform target = attack.listTarget[Random.Range(0, attack.listTarget.Count)];
        GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, target.position);
        LightningStrikeBehaviour sb = b.GetComponent<LightningStrikeBehaviour>();
        sb.shooter = shooter;
        sb.target = target;
        sb.SetUpStats();
    }
}
