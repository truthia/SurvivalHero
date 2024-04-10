using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public DataGame dataGame;
    public IDataService dataService;
    public string playerDataPath = "/player-stats.json";
    public string gameGeneralDataPath = "/game-data.json";
    protected override void Awake()
    {
        base.Awake();
        dataService = new JsonDataService();
        Invoke(nameof(Load), 0.1f);
    }
    public void Load()
    {
        LoadPlayer();
        LoadGame();
    }

    private void LoadGame()
    {
        GameSave gameSave = dataService.LoadData<GameSave>(gameGeneralDataPath, true);
        GameManager.instance.Wave = gameSave.waveCleared;
    }

    private void LoadPlayer()
    {
        //default
        PlayerStats stats = PlayerController.instance.heroStats ;
        PlayerLevel playerLevel = dataGame.PlayerLevel[0];
        stats.SetUpStats(playerLevel, 1);
        GameManager.instance.Money = 100;
        PlayerController.Instance.inventory.ChooseFirstWeapon(PlayerController.instance.weaponCollection.GetWeaponByNameID(playerLevel.SkillName));
        UIManager.instance.practiseUI.boughtTools = new List<int>() {1 };

        PlayerStatsSave playerStatsSave = dataService.LoadData<PlayerStatsSave>(playerDataPath, true);
        //loadsuccess
        playerLevel = dataGame.PlayerLevel[playerStatsSave.playerLevel - 1];
        stats.SetUpStats(playerLevel, playerStatsSave.playerLevel);
        GameManager.instance.Money = playerStatsSave.money;
        PlayerController.Instance.inventory.ChooseFirstWeapon(PlayerController.instance.weaponCollection.GetWeaponByNameID(playerLevel.SkillName));
        UIManager.instance.practiseUI.boughtTools = playerStatsSave.boughtTools;

    }

    public void Save()
    {
        PlayerStats stats = (PlayerStats)PlayerController.Instance.heroStats;
        if (dataService.SaveData(playerDataPath, new PlayerStatsSave(stats.TrainningLevel, GameManager.Instance.Money
            , UIManager.Instance.practiseUI.boughtTools
           ), true))
        {
            Debug.Log("Save successfully");
        }
        else
        {
            Debug.LogError("Can't save");
        }
        dataService.SaveData(gameGeneralDataPath, new GameSave( GameManager.instance.Wave), true);
    }
    private void OnApplicationPause(bool pause)
    {

        if (pause)
        {
            Save();
        }
    }
    private void OnApplicationQuit()
    {
        Save();
    }
}
