using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBahaviour : ProjectileWeaponBehaviour
{

   
    protected override void Start()
    {
        base.Start();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }
}