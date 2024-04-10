using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    public LayerMask dropMask;
    public int itemNumbers = 1;
    [System.Serializable]
   public class Drops
    {
        public string name;
        public float dropRate;
    }
    public List<Drops> drops;
    public void OnDeath()
    {
     
       for(int i=0; i < itemNumbers; i++)
        {
            float randomNumber = Random.Range(0, 100);
            List<Drops> possibleDrops = new List<Drops>();

            foreach (Drops rate in drops)
            {
                if (randomNumber <= rate.dropRate)
                {
                    possibleDrops.Add(rate);
                }
            }
            if (possibleDrops.Count > 0)
            {
                Drops drops = possibleDrops[Random.Range(0, possibleDrops.Count)];
                GameObject go = ObjectPooler.instance.SpawnFromPool(drops.name, transform.position);
                Vector3 randomPos = Utilities.ChooseRandomPos(transform, 2, dropMask);
                Utilities.ParabolicMovement(go, randomPos, 0.4f, 2, () => { });
                GameManager.Instance.itemSpawner.listItems.Add(go);
            }
        }
    }
}
