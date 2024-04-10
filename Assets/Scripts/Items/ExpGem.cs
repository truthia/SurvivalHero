using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : MonoBehaviour,ICollectible
{
    public float experienceGain;
    public void Collect(HeroStats stats)
    {
        stats.IncreaseExperience(experienceGain);
        gameObject.SetActive(false);
    }

   
}
