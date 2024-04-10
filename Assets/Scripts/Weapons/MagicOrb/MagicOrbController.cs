using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrbController : WeaponController
{
    public override void Attack()
    {
        base.Attack();
        GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, transform.position);
        b.transform.localScale = Vector3.one * weaponData.AreaSize * 0.2f;
        MagicOrbBehaviour sb = b.GetComponent<MagicOrbBehaviour>();
        sb.weaponData = weaponData;
        sb.shooter = shooter;
    //    sb.transform.parent = shooter;
        sb.SetUpStats();
    }
}
