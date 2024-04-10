using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrbBehaviour : WeaponBehaviour
{


    public LayerMask targetMask;
    Coroutine cor;
  /*  private void OnDisable()
    {
        if(gameObject.activeInHierarchy)
        transform.SetParent (ObjectPooler.instance.transform);
        if(cor !=null)
        StopCoroutine(cor);
    }*/
    private void Update()
    {
        if(shooter!=null)
        transform.position = shooter.position;
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        cor = StartCoroutine(DelayAttack());
    }
   

    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.1f);
        if (shooter == null) yield break;
        for (int j=0;j<4;j++)
        {
            Collider[] targetsInView = Physics.OverlapSphere(transform.position, weaponData.AreaSize, targetMask);

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
            yield return new WaitForSeconds(0.5f);
        }
       
    }
}
