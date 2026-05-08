using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    private GameManager GameManager=> GameManager.Instance;
    private UIManager UIManager => UIManager.Instance;
    private LevelManager LevelManager => LevelManager.Instance;
    public void OnRestart()
    {
        LevelManager.OnContinue();
        UIManager.OnPlayGame();
        GameManager.Restart();
    }
    public void OnMenuBtn()
    {
        UIManager.OnMenu();
        LevelManager.OnContinue();
        GameManager.Restart();
        GameManager.ChangeState(GameState.Start);
    }
}
