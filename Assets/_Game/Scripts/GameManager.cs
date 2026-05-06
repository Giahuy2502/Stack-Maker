using System;
using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState state = GameState.Playing;
    public static GameManager Instance { get; private set; }
    public GameState State => state;
    private UIManager uiManager => UIManager.Instance;
    private LevelManager levelManager => LevelManager.Instance;
    private DataManager dataManager => DataManager.Instance;

    public UnityEvent onWinGame;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        OnInit();
        levelManager.OnInit();
        levelManager.LoadLevel(levelManager.CurrentLevel);
        uiManager.OnInit();
    }

    private void OnInit()
    {
        state = GameState.Start;
    }

    private void OnDespawn()
    {
        state = GameState.End;
    }
    public void OnWinGame()
    {
        state = GameState.Win;
        onWinGame?.Invoke();
        Invoke(nameof(CallWinUI),2.6f);
    }

    void CallWinUI()
    {
        uiManager.OnWin();
    }
    
    public void OnLoseGame()
    {
        levelManager.OnPause();
        uiManager.OnLose();
    }

    public void Restart()
    {
        OnDespawn();
        OnInit();
        levelManager.OnRestart();
    }

    public void NextLevel()
    {
        OnDespawn();
        OnInit();
        levelManager.OnNext();
    }

    public void ChangeState(GameState newState)
    {
        state = newState;
    }

    public void NewGame()
    {
        OnDespawn();
        OnInit();
        dataManager.ResetLevel();
        levelManager.OnRestart();
    }
}
