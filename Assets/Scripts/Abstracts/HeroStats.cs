using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroStats : MonoBehaviour,IDamagable,IPushable
{
    public CharacterSO characterData;

    protected float currentHealth;
    protected float currentRecovery;
    protected float currentMoveSpeed;
    protected float currentMight;
    protected float currentProjectileSpeed;
    protected float currentMagnet;
    protected float currentDMG;
    protected float currentMaxHP;
    protected float currentCooldownReduce;
    private float currentEXPBonus;

    [Header("Experience/Level")]
    [SerializeField] float experience = 0;
    [SerializeField] int level = 1;
    [SerializeField] public float experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> LevelRanges;
    protected virtual void Awake()
    {
        currentHealth = characterData.MaxHealth;
        CurrentMaxHP = characterData.MaxHealth; 
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;
        currentDMG = characterData.BaseDMG;
        CurrentCooldownReduce = characterData.CooldownReduce;
    }
    #region getter setter
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                //do smth if currentHealth changed
                OnHealthChange();
            }
        }
    }
    public abstract void OnHealthChange();
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                //do smth if currentHealth changed
                OnMightChange();
            }
        }
    }
    public abstract void OnMightChange();
    public float CurrentRecover
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                //do smth if currentHealth changed
            }
        }
    }
    public float CurrentSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                //do smth if currentHealth changed
            }
        }
    }
    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
                //do smth if currentHealth changed
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                //do smth if currentHealth changed
            }
        }
    }

    public float CurrentDMG { get => currentDMG; set => currentDMG = value; }
    public float CurrentMaxHP { get => currentMaxHP; set => currentMaxHP = value; }
    public float CurrentCooldownReduce { get => currentCooldownReduce; set => currentCooldownReduce = value; }
    public float CurrentEXPBonus { get => currentEXPBonus; set => currentEXPBonus = value; }
    public float Experience { get => experience; set {
            if (value != experience)
            {
                experience = value;
                UIManager.Instance.experienceFill.DOFillAmount(Experience / experienceCap, 0.25f);
            }
        } }

    public int Level { get => level; set{
            if (value != level)
            {
                level = value;
                UIManager.Instance.levelText.text= level.ToString();
            }
        }
    }
    #endregion
    public abstract void SetUpStats(PlayerLevel playerLevel,int _trainningLevel);
    protected virtual void Start()
    {
        experienceCap = LevelRanges[0].experienceCapIncrease;
    }
    protected virtual void Update()
    {
        if (currentHealth > currentMaxHP)
        {
            currentHealth = currentMaxHP;
        }
        Recover();
    }
    public void IncreaseExperience(float amount)
    {
        Experience += amount;
        LevelUpChecker();
    }
    public abstract void RestoreHP(float amount);
    void LevelUpChecker()
    {
        if (Experience >= experienceCap)
        {
            //levelUp

            Level++;
            Experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach(LevelRange range in LevelRanges)
            {
                if(Level >= range.startLevel && Level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
            if(!PlayerController.Instance.inventory.CheckIfPassiveFull()||! PlayerController.Instance.inventory.CheckIfWeaponFull())
                GameManager.Instance.gameStateManager.TransitionToState(GameState.LevelUp,0.2F);
        }
    }


    public abstract void Recover();
   
    public abstract void TakeDamage(float dmg, Transform hitby);
   

    public abstract void Kill();


    public abstract void Push(Vector3 direction, float force);

    public abstract void ActivateSkill(WeaponController weaponController);
    public abstract void ActivatePassive(PassiveItem passiveItem);
    
}
