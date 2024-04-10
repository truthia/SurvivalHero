using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeBehaviour : WeaponBehaviour
{

    public LayerMask targetMask;
    public Transform target;

    protected override void OnEnable()
    {
        base.OnEnable();
        Invoke(nameof(NewMethod), 0.25f);
    }

    private void NewMethod()
    {
        if (shooter == null) return;
        if (target.transform.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(GetCurrentDamage(), transform);
        }
        // gameObject.SetActive(false);
    }
}
