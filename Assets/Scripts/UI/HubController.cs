using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : MonoBehaviour
{
    public HubBar hub;
    public float offset = 300;
    private void OnEnable()
    {
        if (hub == null)
        {
            GameObject go = ObjectPooler.instance.SpawnFromPool("EnemyHub", UIManager.Instance.HPUI);
            if (go == null) return;
            hub = go.GetComponent<HubBar>();
            hub.transform.localScale = Vector3.zero;
            hub.fill.fillAmount = 1;
           // hub.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > 30)
        {
            hub.transform.localScale = Vector3.zero;
            return;
        }
      
        hub.transform.position = Utilities.GetWorldToScreenPos(transform.position)+Vector2.up*offset;
      
    }
    private void OnDisable()
    {
        if (hub != null) hub.gameObject.SetActive(false);
        hub = null;
    }
}
