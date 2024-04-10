using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWaveBahaviour : MeleeWeaponBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    private void Awake()
    {
       
        currentSpeed = weaponData.Speed;
        currrentPiece = weaponData.Pirece;
        currentCooldownDuration = weaponData.CoodownDuration;
    }
    protected override void DealAOEDamge()
    {
        if (shooter == null) return;
        Collider[] targetsInView = Physics.OverlapSphere(shooter.position, radius, targetMask);

        for (int i = 0; i < targetsInView.Length; i++)
        {

            Transform target = targetsInView[i].transform;
            if (target != transform && !target.IsChildOf(transform))
            {


                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                   
                    if (target.transform.TryGetComponent(out IDamagable damagable))
                    {
                        damagable.TakeDamage(GetCurrentDamage(), transform);
                    }
                }
            }
           

        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
       
        Invoke(nameof(DealAOEDamge), 0.2f);
    }
    protected override void Start()
    {
        base.Start();

    }


}
