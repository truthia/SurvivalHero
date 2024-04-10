using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Skill",menuName ="SO/Skill")]
public class WeaponSO : ScriptableObject
{
    public int id;
    [SerializeField]
     string weaponName; 
    [SerializeField]
     string bullet;
    [SerializeField]
     float damageMulti;
    [SerializeField]
     float speed;
    [SerializeField]
     float coodownDuration;
    [SerializeField]
     int pirece;
    [SerializeField]
     bool canCastWhenMove;
    [SerializeField]
     int level;  
    [SerializeField]
     string nextWeaponName ;
    [SerializeField]
    Sprite icon;
    [SerializeField]
    string displayName;
    [SerializeField]
    string description; 
    [SerializeField]
    float areaSize;
    public string DisplayName { get => displayName; private set => displayName = value; }
    public string Description { get => description; private set => description = value; }
    public Sprite Icon { get => icon; private set => icon = value; }
    public int Level { get => level; private set => level = value; }
    public string NextWeaponName { get => nextWeaponName; private set => nextWeaponName = value; }
    public string WeaponName { get => weaponName; private set => weaponName = value; }
    public float DamageMulti { get => damageMulti; private set => damageMulti = value; }
    public float Speed { get => speed; private set => speed = value; }
    public float CoodownDuration { get => coodownDuration; private set => coodownDuration = value; }
    public int Pirece { get => pirece; private set => pirece = value; }
    public bool CanCastWhenMove { get => canCastWhenMove; private set => canCastWhenMove = value; }
    public string Bullet { get => bullet; private set => bullet = value; }
    public float AreaSize { get => areaSize; set => areaSize = value; }
}
