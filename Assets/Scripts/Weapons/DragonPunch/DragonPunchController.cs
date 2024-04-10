using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPunchController : WeaponController
{
    PlayerAnimEvent playerAnimEvent;

    public override void Start()
    {
        base.Start();
        playerAnimEvent = PlayerController.Instance.GetComponent<PlayerAnimEvent>();
    }
    public override void Attack()
    {
        base.Attack();


        Invoke(nameof(DelayAttack), 0.1f);
      /*  if (weaponData.Level == 5)
        {
            Invoke(nameof(DelayAttack), 0.3f);
            Invoke(nameof(DelayAttack), 0.5f);
        }*/
    }

    private void DelayAttack()
    {
        BodyInfo bodyInfo = PlayerController.Instance.transform.GetChild(0).GetComponent<BodyInfo>();
        GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, playerAnimEvent.left == 1 ? bodyInfo.leftHand.transform.position :
           bodyInfo.rightHand.transform.position);
        b.transform.localScale = Vector3.one * weaponData.AreaSize / 5;
        b.GetComponent<DragonPunchBehaviour>().DirectionChecker(transform.forward);
        b.GetComponent<DragonPunchBehaviour>().shooter = shooter;
        b.GetComponent<DragonPunchBehaviour>().SetUpStats();
    }
}
