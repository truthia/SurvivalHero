using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public GameStateManager gameStateManager;
    [Header("Codes")]
    public EnemySpawner enemySpawner;
    public CameraManager cameraManager;
    public ItemSpawner itemSpawner;
    public PrefabContainer prefabContainer;
    [Header("Atributes")]
    public float money;
    private int wave;
    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            if (value != wave)
            {
                wave = value;
                UIManager.instance.waveText.text = "WAVE " + (wave + 1);
                UIManager.instance.pauseUI.waveText.text = "WAVE " + (wave + 1);
            }
        }
    }
    private void Start()
    {
        Money = 100;
    }
    public void SetUp(float _money,int _wave)
    {
        money = _money;
        Wave = _wave;
    }

    public float Money { 
        get  { return money; } 
        set {
            if (money != value)
            {
                DOTween.To(() => money, x => money = x, value, 0.25f)
                    .OnUpdate(()=> {
                        UIManager.instance.coinText.text = Utilities.ConvertToKMB(money);
                    });
              //  money = value;

            }
        } }

  

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
