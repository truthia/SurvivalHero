using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public float radius=50;
    public float minDistance=20;
    public float spawnInterval=10;
    public int maxChest=20;
    public LayerMask spawnMask;
    int currentChest;
    public List<GameObject> listChest;
    public List<GameObject> listItems;
    Coroutine cor;
    
    private void Awake()
    {
        listChest = new List<GameObject>();
        listItems = new List<GameObject>();
    }
    public void StartSpawnItem()
    {
       cor=  StartCoroutine(SpawnChest());
    }
    public void ClearItem()
    {
        foreach(GameObject go in listChest)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in listItems)
        {
            go.SetActive(false);
        }
        listChest.Clear();
        listItems.Clear();
    }
    public void StopSpawnItem()
    {
        if(cor!=null)
        StopCoroutine(cor);
    }
    IEnumerator SpawnChest()
    {
        while (currentChest < maxChest)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector3 pos = ChooseSpawnPos();
            GameObject chest = ObjectPooler.instance.SpawnFromPool("Chest", pos);
            listChest.Add(chest);
            chest.transform.Rotate(new Vector3(0, 180, 0));
            currentChest = listChest.FindAll(c => c.activeInHierarchy).Count;
           
        }
    }

    private Vector3 ChooseSpawnPos()
    {
        Vector3 pos = Utilities.ChooseRandomPos(transform, radius, spawnMask);
        while (Vector3.Distance(PlayerController.Instance.transform.position, pos) < minDistance)
        {
            pos = Utilities.ChooseRandomPos(transform, radius, spawnMask);
        }

        return pos;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (gameObject.activeInHierarchy)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
#endif
}
