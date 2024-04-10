using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IDamagable
{
    public float HP;
    float currentHP;
    DropRateManager dropRate;
    private void Start()
    {
        dropRate = GetComponent<DropRateManager>();
        ResetChestHP();
    }

    private void ResetChestHP()
    {
        currentHP = HP;
    }

    public void Kill()
    {
        ResetChestHP();
        gameObject.SetActive(false);
        dropRate.OnDeath();
    }

    public void TakeDamage(float dmg, Transform hitby)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Kill();
        }
    }
}
