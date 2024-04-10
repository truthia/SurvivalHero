using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName ="Character",menuName ="SO/Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] string beginSkill;
    [SerializeField] float maxHealth;
    [SerializeField] float recovery;
    [SerializeField] float moveSpeed;
    [SerializeField] float might;
    [SerializeField] float projectileSpeed;
    [SerializeField] float magnet;
    [SerializeField] float baseDMG;
    [SerializeField] float cooldownReduce;

    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    public float Recovery { get => recovery; private set => recovery = value; }
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    public float Might { get => might; private set => might = value; }
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }
    public float Magnet { get => magnet; private set => magnet = value; }
    public float BaseDMG { get => baseDMG; private set => baseDMG = value; }
    public float CooldownReduce { get => cooldownReduce; private set => cooldownReduce = value; }
}
