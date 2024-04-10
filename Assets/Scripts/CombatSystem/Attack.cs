using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public LayerMask targetMask;
    public float range;
    public Transform visibleTarget;
    Coroutine findCor;
    public int shield;

    public List<Transform> listTarget;
    private void Awake()
    {
        listTarget = new List<Transform>();
    }
    public void StartCombat()
    {
        findCor = StartCoroutine(FindVisibleTarget());
    }
    public void StopCombat()
    {
        StopCoroutine(findCor);
    }
    IEnumerator FindVisibleTarget()
    {
        while (true)
        {
            FindTarget();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void FindTarget()
    {
        listTarget.Clear();
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, range, targetMask);
        if (targetsInView.Length > 0)
        {
            Transform closest = targetsInView[0].transform;
            for (int i = 0; i < targetsInView.Length; i++)
            {

                Transform target = targetsInView[i].transform;
                if (target != transform && !target.IsChildOf(transform)&& Vector3.Distance(target.position,transform.position)< Vector3.Distance(closest.position, transform.position))
                {
                    closest = target;
                    listTarget.Add(target);
                }

            }
            visibleTarget = closest;
        }
      
    }

    public bool AimAtTarget()
    {
        if (visibleTarget != null)
        {
            transform.LookAt(visibleTarget);
            Vector3 newRotation = transform.eulerAngles;
            newRotation.x = 0;
            transform.rotation = Quaternion.Euler(newRotation);
   
        }
        return visibleTarget == null;
    }
}
