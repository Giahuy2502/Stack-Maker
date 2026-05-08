using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelTxt;
    private LevelManager LevelManager => LevelManager.Instance;
    private UIManager UIManager => UIManager.Instance;
    void OnEnable()
    {
        OnInit();
    }

    private void OnInit()
    {
        levelTxt.text = "Level "+ LevelManager.CurrentLevel.ToString();
    }

    public void OnSetting()
    {
        UIManager.OnSetting();
        LevelManager.OnPause();
    }

    private void OnDisable()
    {
        OnDespawn();
    }

    private void OnDespawn()
    {
        
    }
}
