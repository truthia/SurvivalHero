using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class WaveBotStats : MonoBehaviour,IDamagable,IPushable
{
    public WaveEnemySO enemyData;

    protected float currentSpeed;
    protected float currentHP;
    protected float currentDamage;
    

    NavMeshAgent agent;
    Collider col;
    NavMeshObstacle obstacle;
    DropRateManager dropRateManager;
    HubController hubController;
    #region getter setter
    public float CurrentHealth
    {
        get { return currentHP; }
        set
        {
            if (currentHP != value)
            {
                currentHP = value;
                //do smth if currentHealth changed
                hubController.hub.fill.DOFillAmount(CurrentHealth / enemyData.MaxHP, 0.25f);
            }
        }
    }

   
   
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set
        {
            if (currentSpeed != value)
            {
                currentSpeed = value;
                //do smth if currentHealth changed
            }
        }
    }public float CurrentDamage
    {
        get { return currentDamage; }
        set
        {
            if (currentDamage != value)
            {
                currentDamage = value;
                //do smth if currentHealth changed
            }
        }
    }
  
    #endregion
    private void Awake()
    {
       

        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        col = GetComponent<Collider>();
        dropRateManager = GetComponent<DropRateManager>();
        hubController = GetComponent<HubController>();
        ResetStats();
    }

    private void ResetStats()
    {
        currentSpeed = enemyData.Speed;
        currentHP = enemyData.MaxHP;
        currentDamage = enemyData.Damage;

        agent.speed = currentSpeed;
    }

    public void Kill()
    {
        ResetStats();
        GameManager.Instance.enemySpawner.OnEnemyKilled();
        gameObject.SetActive(false);
        dropRateManager.OnDeath();
     //   GameManager.Instance.enemySpawner.enemiesAlive--;

    }
    Tween pushTween;
 
    public void Push(Vector3 direction, float force)
    {
        agent.enabled = false;
        col.isTrigger = false;
        obstacle.enabled = true;
        if (pushTween.IsActive() && currentHP > 0)
            pushTween.Kill();
        pushTween = transform.DOMove(transform.position+ direction * force, 0.2f).SetEase(Ease.OutQuad).OnComplete(()=> {
            agent.enabled = true;
            col.isTrigger = true;
            obstacle.enabled = false;
            if (currentHP <= 0)
            {
                Kill();
            }
        });
    }

    public void TakeDamage(float dmg,Transform hitby)
    {
        if (pushTween.IsActive())
        {
            /* if (!pushTween.IsPlaying())
                 currentHealth -= dmg;*/
        }
        else
        {
            CurrentHealth -= dmg;
        }
    
        hubController.hub.transform.localScale = Vector3.one;
        hubController.hub.gameObject.SetActive(true);
      
        Vector3 direction = transform.position - hitby.position;
        direction.y = 0;
        direction = direction.normalized;
        Push(direction, 1);

    }

  

}
