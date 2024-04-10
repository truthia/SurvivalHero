using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricOrbController : WeaponController
{
    PlayerAnimEvent playerAnimEvent;

    public override void Start()
    {
        base.Start();
        playerAnimEvent = PlayerController.Instance.GetComponent<PlayerAnimEvent>();
    }
    public override void UpdateWeapon()
    {
        base.UpdateWeapon();
    }
    public override void Attack()
    {
        base.Attack();
        //GameObject spawnedFire = Instantiate(prefab,transform.position,Quaternion.identity);
       
        if (weaponData.Level == 5)
        {
            Invoke(nameof(DelayUpgradedAttack), 0.2f);
         
        }
        else
        {
            Invoke(nameof(DelayAttack), 0.2f);
        }
    }

    private void DelayAttack()
    {
        BodyInfo bodyInfo = PlayerController.Instance.transform.GetChild(0).GetComponent<BodyInfo>();
        GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, playerAnimEvent.left == 1 ? bodyInfo.leftHand.transform.position :
           bodyInfo.rightHand.transform.position);
        ElectricOrbBehaviour wp = b.GetComponent<ElectricOrbBehaviour>();
        wp.shooter = shooter;
        if (attack.listTarget.Count > 0)
        {
            Transform target = attack.listTarget[Random.Range(0, attack.listTarget.Count)];
            wp.DirectionChecker((target.position - transform.position).normalized);
        }
        else
        {
            wp.DirectionChecker(shooter.forward);

        }
        wp.SetUpStats();
        wp.weaponData = weaponData;
        b.transform.localScale = Vector3.one * weaponData.AreaSize / 1.5f;

        ObjectPooler.instance.SpawnFromPool("StartElectricOrb", b.transform.position);
    } private void DelayUpgradedAttack()
    {
        BodyInfo bodyInfo = PlayerController.Instance.transform.GetChild(0).GetComponent<BodyInfo>();
        for(int i = -1; i < 2; i++)
        {
            GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, playerAnimEvent.left == 1 ? bodyInfo.leftHand.transform.position :
          bodyInfo.rightHand.transform.position);
            ElectricOrbBehaviour wp = b.GetComponent<ElectricOrbBehaviour>();
            wp.shooter = shooter;
            Transform target ;
            if (attack.listTarget.Count > 0)
            {

                target = attack.listTarget[Random.Range(0, attack.listTarget.Count)];
                Vector3 direction = (target.position - transform.position).normalized;

                wp.DirectionChecker(Utilities.GetInclinedVector(direction, 30 * i, Vector3.up));
        
            }
            else
            {
                wp.DirectionChecker(Utilities.GetInclinedVector(shooter.forward, 30 * i, Vector3.up));
            }
            wp.SetUpStats();
            wp.weaponData = weaponData;
            b.transform.localScale = Vector3.one * weaponData.AreaSize / 1.5f;
            ObjectPooler.instance.SpawnFromPool("StartElectricOrb", b.transform.position);
        }

     
    }
}
