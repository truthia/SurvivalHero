using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : WeaponBehaviour
{
    Attack attack;
    
    // Update is called once per frame
    protected override void OnEnable()
    {
        base.OnEnable();

        if (shooter != null)
        {
            attack = shooter.GetComponent<Attack>();
            attack.shield = weaponData.Pirece;
            // Debug.Log(attack.shield);

        }
    }

    private void DelayStart()
    {
       
    }


    private void DisableAct()
    {
        if (attack != null)
        {
            attack.shield = 0;
            shooter = null;
        }
      //  transform.parent = null;
    }

    private void LateUpdate()
    {
        if (attack != null)
        {
            if (attack.shield <= 0)
            {
                Invoke(nameof(DelayDisable), 0.12f);

            }
        }
    }

    private void DelayDisable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        DisableAct();
    }
}
