using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricOrbBehaviour : ProjectileWeaponBehaviour
{
    
    protected override void Start()
    {
        base.Start();

    }
    private void OnDisable()
    {
        if (shooter == null) return;
        Explode();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CheckPirece();
          
        }
    }

    private void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, weaponData.AreaSize);
        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                if (!col.transform.IsChildOf(shooter))
                {
                    if (col.transform.TryGetComponent(out IDamagable damagable))
                    {
                        damagable.TakeDamage(GetCurrentDamage(), transform);
                    }
                }
            }
        }

        ObjectPooler.instance.SpawnFromPool("EndElectricOrb", transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }
}
