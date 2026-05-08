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
    private UIManager UIManager => UIManager.Instance;
    private LevelManager LevelManager => LevelManager.Instance;
    private DataManager DataManager => DataManager.Instance;
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
        LevelManager.OnInit();
        LevelManager.LoadLevel(LevelManager.CurrentLevel);
        UIManager.OnInit();
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
        UIManager.DisaleGamePlayPanel();
        LevelManager.OnWin();
        Invoke(nameof(CallWinUI),2.6f);
    }

    void CallWinUI()
    {
        UIManager.OnWin();
    }
    
    public void OnLoseGame()
    {
        LevelManager.OnLose();
        UIManager.OnLose();
    }

    public void Restart()
    {
        OnDespawn();
        OnInit();
        LevelManager.OnRestart();
    }

    public void NextLevel()
    {
        OnDespawn();
        OnInit();
        LevelManager.OnNext();
    }

    public void ChangeState(GameState newState)
    {
        state = newState;
    }

    public void NewGame()
    {
        OnDespawn();
        OnInit();
        DataManager.ResetLevel();
        LevelManager.OnRestart();
    }
}
