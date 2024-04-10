using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerCollector : Collector
{
    
    private void Start()
    {
        SetMagnetRadius(PlayerController.Instance.heroStats.CurrentMagnet);
    }
    public override void Collect(Collider other)
    {
        StartCoroutine(CollectCor(other));
    }
    IEnumerator CollectCor(Collider other)
    {
        if (other.transform.TryGetComponent(out ICollectible collectible))
        {
            other.tag = "Untagged";
            other.transform.DOMove(other.transform.position + 3 * (other.transform.position - transform.position).normalized, collectSpeed)
               .SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(collectSpeed);
            while (Vector3.Distance(other.transform.position, transform.position) >0.2f)
            {
                other.transform.Translate((transform.position - other.transform.position).normalized * collectSpeed*100 * Time.deltaTime);
                yield return null;

            }
            collectible.Collect(PlayerController.Instance.heroStats);
            other.tag = "Item";
        }
           
    }
    public override void SetMagnetRadius(float radius)
    {
        colliderSphere.radius = radius;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Collect(other);
        }
    }
}
