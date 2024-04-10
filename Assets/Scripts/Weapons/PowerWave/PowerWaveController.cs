using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWaveController : WeaponController
{
    public override void Start()
    {
        base.Start();
    }
    public override void UpdateWeapon()
    {
        base.UpdateWeapon();
    }
    public override void Attack()
    {
        base.Attack();

        for (int i = 0; i < weaponData.Level ; i++)
        {
            DelayAttack(i);
        }

    }

    private void DelayAttack(int i)
    {
        GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, transform.position);
        b.GetComponent<PowerWaveBahaviour>().DirectionChecker(Utilities.GetInclinedVector(PlayerController.Instance.transform.forward, 90 * i, Vector3.up));
        b.GetComponent<PowerWaveBahaviour>().shooter = shooter;
        b.GetComponent<PowerWaveBahaviour>().SetUpStats();
    }
}
