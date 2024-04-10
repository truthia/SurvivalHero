using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPunchBehaviour : MeleeWeaponBehaviour
{
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
        Collider[] targetsInView = Physics.OverlapBox(shooter.position, new Vector3 (weaponData.AreaSize/4,2, weaponData.AreaSize*1.6f),transform.rotation, targetMask);

        for (int i = 0; i < targetsInView.Length; i++)
        {

            Transform target = targetsInView[i].transform;
            if (target != transform && !target.IsChildOf(transform))
            {

                if (target.transform.TryGetComponent(out IDamagable damagable))
                {
                    damagable.TakeDamage(GetCurrentDamage(), transform);
                }
                if (target.CompareTag("Dummy"))
                {
                    GameManager.Instance.Money += PlayerController.Instance.heroStats.idleGain;
                    ObjectPooler.instance.SpawnFromPool("EndFireBall", transform.position);
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
