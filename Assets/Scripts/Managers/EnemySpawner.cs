using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnDistance=50f;
    public LayerMask spawnLayer;
     float spawnTimer;
    public int enemiesAlive;
    public int maxEnemies;
    public bool maxEnemiesReached = false;
    private int enemyKilled;
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota;
        public float spawnInterval;
        public int spawnCount;
    }
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
    }
    public List<Wave> waves;
    public int currentWaveCount;
    LevelUpUI.WeaponUpgrade addedUpgrade;

    public int EnemyKilled { get => enemyKilled; set { 
            if(value!= enemyKilled)
            {
                enemyKilled = value;
                UIManager.Instance.killText.text = enemyKilled.ToString();
   
            }
        } }

    public void StartWave()
    {
        currentWaveCount = GameManager.Instance.Wave;
        PlayerController.Instance.heroStats.weaponIndex = 1;
        addedUpgrade = new LevelUpUI.WeaponUpgrade()
        {
            initWeaponName = PlayerController.Instance.inventory.weaponSlots[0].weaponData.WeaponName,
            weaponData = PlayerController.Instance.inventory.weaponSlots[0].weaponData
        };
        UIManager.Instance.levelUpUI.weaponUpgradeOptions.Add(addedUpgrade);
        UIManager.Instance.EXPAndTimeGO.SetActive(true);
        CalculateWaveQuota();
    }
    public void UpdateWave()
    {
       
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void BeginNextWave()
    {
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }
    void SpawnEnemy()
    {
        if(waves[currentWaveCount].spawnCount<waves[currentWaveCount].waveQuota)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= enemyGroup.enemyCount)
                    {
                        maxEnemiesReached = true;
                     
                        return;
                    }
                    Vector3 spawnPos = GetSpawnPos();
                    ObjectPooler.instance.SpawnFromPool(enemyGroup.enemyName, spawnPos);
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        else if(enemiesAlive<=0)
        {
            EndWave();
        }
        if (enemiesAlive < maxEnemies)
        {
            maxEnemiesReached = false;
        }
    }

    private  void EndWave()
    {
        GameManager.Instance.gameStateManager.TransitionToState(GameState.Idle);
        GameManager.Instance.Wave++;
       

        GameManager.Instance.itemSpawner.StopSpawnItem();
        GameManager.Instance.itemSpawner.ClearItem();
        PlayerLevel data = DataManager.Instance.dataGame.PlayerLevel[PlayerController.Instance.heroStats.TrainningLevel - 1];
        string skillName = data.SkillName;
        PlayerController.Instance.inventory.ChooseFirstWeapon(PlayerController.Instance.weaponCollection.GetWeaponByNameID(skillName));
        PlayerController.Instance.heroStats.passiveIndex = 0;
        PlayerController.Instance.heroStats.weaponIndex = 1;
        PlayerController.Instance.heroStats.SetUpStats(data, PlayerController.Instance.heroStats.TrainningLevel);
        if (addedUpgrade != null)
            UIManager.Instance.levelUpUI.weaponUpgradeOptions.Remove(addedUpgrade);
        UIManager.Instance.killText.transform.parent.gameObject.SetActive(true);
        UIManager.Instance.waveText.transform.parent.gameObject.SetActive(false);
        UIManager.Instance.EXPAndTimeGO.SetActive(true);
        EnemyKilled = 0;
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
        EnemyKilled++;
 
    }
    private Vector3 GetSpawnPos()
    {
        Vector3 spawnPos = Utilities.ChooseRandomPos(PlayerController.Instance.transform, spawnDistance, spawnLayer);
        while (Vector3.Distance(PlayerController.Instance.transform.position, spawnPos) < 30)
        {
            spawnPos = Utilities.ChooseRandomPos(PlayerController.Instance.transform, spawnDistance, spawnLayer);
        }

        return spawnPos;
    }
    /*  public int maxEnemy;
 [HideInInspector]
 public List<GameObject> enemyList;
 private void Start()
 {
     StartCoroutine(Spawn());
 }
 IEnumerator  Spawn()
 {
     int pos = 0;
     while (true)
     {
         yield return new WaitForSeconds(1f);
         if (enemyList.FindAll(e=>e.gameObject.activeInHierarchy).Count < maxEnemy)
         {
             GameObject enemy = ObjectPooler.instance.SpawnFromPool("BasicBot", transform.GetChild(pos).position);
             enemyList.Add(enemy);
             pos++;
             if (pos == transform.childCount) pos = 0;
         }
     }
 }*/
}
