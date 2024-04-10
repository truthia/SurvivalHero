using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceController : WeaponController
{
    public override void Attack()
    {
        base.Attack();
        GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, transform.position);
        b.transform.localScale = Vector3.one * weaponData.AreaSize*0.2f;
        IceBehaviour sb = b.GetComponent<IceBehaviour>();
        sb.range = weaponData.AreaSize;
        sb.shooter = shooter;
        sb.SetUpStats();
    }
}
