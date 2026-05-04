using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextButton;
    
    private GameManager manager=> GameManager.Instance;
    private UIManager uiManager => UIManager.Instance;

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
