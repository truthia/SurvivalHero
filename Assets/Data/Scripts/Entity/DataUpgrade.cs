using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable] 
public class PlayerLevel
{
    public int Level;
    public float Idle;
    public float SkillDmg;
    public float SkillCost;
    public int UnlockRequire;
    public string SkillName;
}
[Serializable] 
public class Tool
{
    public int STT;
    public string Name;
    public float Bonus;
    public float Cost;
}
