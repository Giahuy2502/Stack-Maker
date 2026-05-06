using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuBtn;
    private GameManager manager=> GameManager.Instance;
    private UIManager uiManager => UIManager.Instance;
    private LevelManager levelManager => LevelManager.Instance;
    public void OnRestart()
    {
        levelManager.OnContinue();
        uiManager.OnPlayGame();
        manager.Restart();
    }
    public void OnMenuBtn()
    {
        uiManager.OnMenu();
        levelManager.OnContinue();
        levelManager.OnRestart();
        manager.ChangeState(GameState.Start);
    }
}
