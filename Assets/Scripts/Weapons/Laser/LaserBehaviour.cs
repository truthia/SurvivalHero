using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : WeaponBehaviour
{
    public Hovl_Laser laserScript;
   
    public LayerMask targetMask;
    private void OnDisable()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.parent = ObjectPooler.instance.transform;
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(DelayAttack());
        StartCoroutine(PrepareDisable());
    }
    
    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.1f);

        if (shooter == null) yield break;
        laserScript.MaxLength = weaponData.AreaSize;
    //    laserScript.Laser.enabled = true;
     
        for (int j = 0; j < 35; j++)
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), weaponData.AreaSize);
         //   DirectionChecker(hits[0].transform.position - transform.position) ;
            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (shooter!=null&&!hit.transform.IsChildOf(shooter))
                    {
                        if (hit.transform.TryGetComponent(out IDamagable damagable))
                        {
                            damagable.TakeDamage(GetCurrentDamage(), transform);
                        }
                       
                    }
                    ObjectPooler.instance.SpawnFromPool("LaserEnd", hit.point);
                }
               
            }
            ObjectPooler.instance.SpawnFromPool("LaserEnd",transform.position+ transform.TransformDirection(Vector3.forward)*weaponData.AreaSize);
            // Collider[] targetsInView = Physics.OverlapSphere(transform.position, weaponData.AreaSize, targetMask);


            /*  for (int i = 0; i < targetsInView.Length; i++)
              {

                  Transform target = targetsInView[i].transform;
                  if (target != transform && !target.IsChildOf(transform))
                  {
                      if (target.transform.TryGetComponent(out IDamagable damagable))
                      {
                          damagable.TakeDamage(GetCurrentDamage(), transform);
                      }
                      // ObjectPooler.instance.SpawnFromPool("LaserEnd", targetsInView[i].transform.position);
                  }


              }*/
            
            yield return new WaitForSeconds(0.05f);
        }

    }
    IEnumerator PrepareDisable()
    { 
        yield return new WaitForSeconds(destroyAfterSeconds-1);
        laserScript.DisablePrepare();
        Destroy(gameObject, 1);
    }
}
