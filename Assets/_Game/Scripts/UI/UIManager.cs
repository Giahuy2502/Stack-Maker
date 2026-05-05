using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private WinPanel winPanel;
    [SerializeField] private MainPanel mainPanel;
    [SerializeField] private SettingPanel settingPanel;
    [SerializeField] private GamePlayPanel gamePlayPanel;
    [SerializeField] private LosePanel losePanel;
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    

    public void OnInit()
    {
        OnMenu();
    }

    public void OnMenu()
    {
        winPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);
        settingPanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(false);
    }

    public void OnWin()
    {
        winPanel.gameObject.SetActive(true);
        mainPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(false);
    }

    public void OnPlayGame()
    {
        winPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(true);
        losePanel.gameObject.SetActive(false);
    }

    public void OnSetting()
    {
        winPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(true);
        gamePlayPanel.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(false);
    }

    public void OnLose()
    {
        winPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(true);
    }
    private void OnDespawn()
    {
        winPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(false);
    }

}
