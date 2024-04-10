using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : WeaponController
{
    public override void Attack()
    {
        base.Attack();
        GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, transform.position);
        ShieldBehaviour sb = b.GetComponent<ShieldBehaviour>();
        sb.shooter = shooter;
        sb.SetUpStats();
        sb.weaponData = weaponData;
        b.transform.parent = shooter;
    }
}
