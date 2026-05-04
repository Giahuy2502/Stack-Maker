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
    [SerializeField] private Button playBtn;

    private GameManager manager => GameManager.Instance;
    private UIManager managerUI => UIManager.Instance;
    private LevelManager levelManager => LevelManager.Instance;

    void OnEnable()
    {
        OnInit();
    }
    
    private void OnInit()
    {
        levelTxt.text = "Level "+ levelManager.CurrentLevel.ToString();
    }
    public void OnPlay()
    {
        manager.ChangeState(GameState.Playing);
        managerUI.OnPlayGame();
    }

    private void OnDespawn()
    {
        
    }
}
