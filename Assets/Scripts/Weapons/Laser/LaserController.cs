using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : WeaponController
{
    public GameObject laserPrefab; 
    public override void Attack()
    {
        base.Attack();
        Invoke(nameof(DelayAttack), 0.1f);
    }
    private void DelayAttack()
    {
        BodyInfo bodyInfo = PlayerController.Instance.transform.GetChild(0).GetComponent<BodyInfo>();
        for(int i =-1; i < 2; i++)
        {
            if (i != 0)
            {
               // GameObject b = ObjectPooler.instance.SpawnFromPool(weaponData.Bullet, bodyInfo.head.transform.position + transform.right * i/2);
                GameObject b = Instantiate(laserPrefab, bodyInfo.head.transform.position-transform.up/2 + transform.right * i / 4f-transform.forward*0.5f,Quaternion.identity);
                LaserBehaviour ls = b.GetComponent<LaserBehaviour>();
                b.transform.parent = bodyInfo.head.transform;
                ls.shooter = shooter;
                ls.DirectionChecker(shooter.transform.forward);
                ls.SetUpStats();
            }

        }
       
    }
}
