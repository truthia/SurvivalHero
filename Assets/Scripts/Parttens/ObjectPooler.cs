using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public static ObjectPooler instance;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public int fileNumbers;
    private void Awake()
    {
        instance = this;
        Time.timeScale = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
      StartCoroutine(SetUp());
    }

    IEnumerator  SetUp()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                fileNumbers++;
               
            }
            poolDictionary.Add(pool.tag, objectPool);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Time.timeScale = 1;
        UIManager.Instance.loadingScreen.SetActive(false);
    }

    public GameObject SpawnFromPool(string tag, Vector3 positon, Transform parent=null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        if (parent == null) parent = transform;
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetParent(parent);
        objectToSpawn.transform.position = positon;
       /* objectToSpawn.transform.rotation = Quaternion.identity;*/

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    public GameObject SpawnFromPool(string tag,Transform parent)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetParent ( parent);
        objectToSpawn.transform.rotation = Quaternion.identity;
        objectToSpawn.transform.localScale = Vector3.one;
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
