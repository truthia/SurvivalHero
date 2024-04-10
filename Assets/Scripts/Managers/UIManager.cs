using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("UI")]
    public PauseUI pauseUI;
    public LevelUpUI levelUpUI;
    public Transform HPUI;
    public GameObject loadingScreen;
    public PractiseUI practiseUI;
    public SettingsUI settingsUI;
    public GameObject startWaveBtn;
    [Header("MainUI Elements")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI killText;
    public Image experienceFill;
    public TextMeshProUGUI levelText;
    public GameObject EXPAndTimeGO;
    public void PauseGame()
    {
        GameManager.Instance.gameStateManager.TransitionToState(GameState.Pause);
    }
    public void ResumeGame()
    {
        GameManager.Instance.gameStateManager.TransitionToState(GameState.Wave);
    }
    public void StartWave()
    {
        GameManager.Instance.gameStateManager.TransitionToState(GameState.Wave);
        GameManager.Instance.enemySpawner.StartWave();
        GameManager.Instance.itemSpawner.StartSpawnItem();
    }
    public void SettingsBtn()
    {
        if (GameManager.Instance.gameStateManager.currentKeyState == GameState.Idle)
        {
            settingsUI.gameObject.SetActive(true);
        } if (GameManager.Instance.gameStateManager.currentKeyState == GameState.Wave)
        {
            PauseGame();
            pauseUI.gameObject.SetActive(true);
        }
    }
}
