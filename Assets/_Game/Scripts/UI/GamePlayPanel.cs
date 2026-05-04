using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private Button settingBtn;

    private LevelManager levelManager => LevelManager.Instance;
    private UIManager uiManager => UIManager.Instance;
    void OnEnable()
    {
        OnInit();
    }

    private void OnInit()
    {
        levelTxt.text = "Level "+ levelManager.CurrentLevel.ToString();
    }

    public void OnSetting()
    {
        uiManager.OnSetting();
        levelManager.OnPause();
    }

    private void OnDisable()
    {
        OnDespawn();
    }

    private void OnDespawn()
    {
        
    }
}
