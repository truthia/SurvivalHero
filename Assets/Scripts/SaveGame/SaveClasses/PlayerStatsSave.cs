using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerStatsSave 
{
    public int playerLevel;
    public float money;
    public List<int> boughtTools;
    public PlayerStatsSave( int playerLevel, float money, List<int> boughtTools)
    {
        this.playerLevel = playerLevel;
        this.money = money;
        this.boughtTools = boughtTools;
    }
    
}
