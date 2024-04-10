using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Enemy",menuName ="SO/Enemy")]
public class WaveEnemySO : ScriptableObject
{
    [SerializeField]
    float maxHP;
    [SerializeField]
     float damage;
    [SerializeField]
     float speed;

    public float MaxHP { get => maxHP; private set => maxHP = value; }
    public float Damage { get => damage; private set => damage = value; }
    public float Speed { get => speed; private set => speed = value; }
}
