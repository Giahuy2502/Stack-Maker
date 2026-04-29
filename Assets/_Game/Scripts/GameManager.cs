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
    }

    private void OnInit()
    {
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
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        
    }
}
