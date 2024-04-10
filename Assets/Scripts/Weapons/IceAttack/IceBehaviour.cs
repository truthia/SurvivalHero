using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBehaviour : WeaponBehaviour
{
    public float range;

    public LayerMask targetMask;
    protected override void OnEnable()
    {
        base.OnEnable();

        Invoke(nameof(DelayAttack), 0.1f);
    }

    private void DelayAttack()
    {
        if (shooter == null) return;
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, range, targetMask);

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
    }
}
