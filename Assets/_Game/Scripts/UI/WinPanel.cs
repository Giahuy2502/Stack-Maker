using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private GameManager GameManager=> GameManager.Instance;
    private UIManager UIManager => UIManager.Instance;
    private DataManager DataManager => DataManager.Instance;

    private void OnEnable()
    {
        OnInit();
    }

    private void OnInit()
    {
        scoreText.text = "SCORE: "+ DataManager.Score;
    }

    public void OnRestart()
    {
        GameManager.Restart();
        UIManager.OnPlayGame();
    }

    public void OnNext()
    {
        GameManager.NextLevel();
        UIManager.OnPlayGame();
    }
}
