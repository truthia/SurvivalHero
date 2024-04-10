using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : WeaponBehaviour
{
  
    public LayerMask targetMask;

    protected override void OnEnable()
    {
        base.OnEnable();
        Invoke(nameof(NewMethod), 0.75f);
    }

    private void NewMethod()
    {
        if (shooter == null) return;
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, weaponData.AreaSize, targetMask);
        ObjectPooler.instance.SpawnFromPool("MeteorEnd", transform.position);
        for (int i = 0; i < targetsInView.Length; i++)
        {

            Transform target = targetsInView[i].transform;
            if (target != transform && !target.IsChildOf(transform))
            {
                if (target.transform.TryGetComponent(out IDamagable damagable))
                {
                    damagable.TakeDamage(GetCurrentDamage(), transform);
                }

            }


        }
       // gameObject.SetActive(false);
    }
}
