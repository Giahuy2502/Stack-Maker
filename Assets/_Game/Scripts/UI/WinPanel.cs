using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private GameManager manager=> GameManager.Instance;
    private UIManager uiManager => UIManager.Instance;
    private DataManager dataManager => DataManager.Instance;

    private void OnEnable()
    {
        OnInit();
    }

    private void OnInit()
    {
        scoreText.text = "SCORE: "+ dataManager.Score;
    }

    public void OnRestart()
    {
        manager.Restart();
        uiManager.OnPlayGame();
    }

    public void OnNext()
    {
        manager.NextLevel();
        uiManager.OnPlayGame();
    }
}
