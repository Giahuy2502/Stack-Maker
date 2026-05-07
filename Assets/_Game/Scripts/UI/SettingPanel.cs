using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button menuBtn;
    [SerializeField] private Button retryBtn;
    
    private GameManager manager => GameManager.Instance;
    private LevelManager managerLevel => LevelManager.Instance;
    private UIManager managerUI => UIManager.Instance;
    public void OnExitBtn()
    {
        managerUI.OnPlayGame();
        managerLevel.OnContinue();
    }

    public void OnMenuBtn()
    {
        managerUI.OnMenu();
        managerLevel.OnContinue();
        manager.Restart();
        manager.ChangeState(GameState.Start);
    }

    public void OnRetryBtn()
    {
        managerUI.OnPlayGame();
        managerLevel.OnContinue();
        manager.Restart();
    }
}
