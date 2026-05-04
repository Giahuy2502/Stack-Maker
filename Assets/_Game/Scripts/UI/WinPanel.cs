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
        uiManager.OnPlayGame();
        manager.Restart();
    }

    public void OnNext()
    {
        uiManager.OnPlayGame();
        manager.NextLevel();
    }
}
