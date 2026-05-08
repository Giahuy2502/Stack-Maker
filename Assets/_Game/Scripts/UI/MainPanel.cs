using System;
using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelTxt;

    private GameManager GameManager => GameManager.Instance;
    private UIManager UIManager => UIManager.Instance;
    private LevelManager LevelManager => LevelManager.Instance;

    void OnEnable()
    {
        OnInit();
    }
    
    private void OnInit()
    {
        levelTxt.text = "Level "+ LevelManager.CurrentLevel.ToString();
    }
    public void OnPlay()
    {
        GameManager.ChangeState(GameState.Playing);
        UIManager.OnPlayGame();
    }

    public void OnNewGame()
    {
        GameManager.NewGame();
        GameManager.ChangeState(GameState.Playing);
        UIManager.OnPlayGame();
    }

    private void OnDespawn()
    {
        
    }
}
