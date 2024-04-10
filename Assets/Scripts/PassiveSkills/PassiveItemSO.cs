using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName ="PassiveSkillSO",menuName ="SO/PassiveSkill")]
public class PassiveItemSO : ScriptableObject
{
    public int id;
    [SerializeField]
    float multipler;  
    [SerializeField]
    string passiveName;
    [SerializeField]
    int level;
    [SerializeField]
    string nextPassiveName;
    [SerializeField]
    Sprite icon;
    [SerializeField]
    string displayName;
    [SerializeField]
    string description;
    public string DisplayName { get => displayName; private set => displayName = value; }
    public string Description { get => description; private set => description = value; }
    public Sprite Icon { get => icon; private set => icon = value; }
    public int Level { get => level; private set => level = value; }
    public string NextPassiveName { get => nextPassiveName; private set => nextPassiveName = value; }
    public float Multipler { get => multipler; private set => multipler = value; }
    public string PassiveName { get => passiveName;private set => passiveName = value; }
}
