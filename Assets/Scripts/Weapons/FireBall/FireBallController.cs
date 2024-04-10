using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : WeaponController
{
    PlayerAnimEvent playerAnimEvent;
    
    public override void Start()
    {
        base.Start();
        playerAnimEvent = PlayerController.Instance. GetComponent<PlayerAnimEvent>();
    }
    public override void UpdateWeapon()
    {
        base.UpdateWeapon();
    }
    public override void Attack()
    {
        base.Attack();
        //GameObject spawnedFire = Instantiate(prefab,transform.position,Quaternion.identity);
        Invoke(nameof(DelayAttack),0.2f);
        if (weaponData.Level == 5)
        {
            Invoke(nameof(DelayAttack), 0.35f);
            Invoke(nameof(DelayAttack), 0.55f);
        }
    }

    private void DelayAttack()
    {
        BodyInfo bodyInfo = PlayerController.Instance.transform.GetChild(0).GetComponent<BodyInfo>();
        GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet,playerAnimEvent.left==1?bodyInfo.leftHand.transform.position:
           bodyInfo.rightHand.transform.position);
        FireBallBahaviour fireBallBahaviour = b.GetComponent<FireBallBahaviour>();
        fireBallBahaviour.shooter = shooter;
        fireBallBahaviour.DirectionChecker(shooter.transform.forward);
        fireBallBahaviour.SetUpStats();


        ObjectPooler.instance.SpawnFromPool("StartFireBall", b.transform.position);
    }
}
