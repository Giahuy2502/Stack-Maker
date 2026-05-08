using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    private GameManager GameManager => GameManager.Instance;
    private LevelManager LevelManager => LevelManager.Instance;
    private UIManager UIManager => UIManager.Instance;
    public void OnExitBtn()
    {
        UIManager.OnPlayGame();
        LevelManager.OnContinue();
    }

    public void OnMenuBtn()
    {
        UIManager.OnMenu();
        LevelManager.OnContinue();
        GameManager.Restart();
        GameManager.ChangeState(GameState.Start);
    }

    public void OnRetryBtn()
    {
        UIManager.OnPlayGame();
        LevelManager.OnContinue();
        GameManager.Restart();
    }
}
