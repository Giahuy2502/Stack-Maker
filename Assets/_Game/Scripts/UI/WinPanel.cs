using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextButton;
    
    private GameManager manager=> GameManager.Instance;

    public void OnRestart()
    {
        manager.Restart();
        gameObject.SetActive(false);
    }

    public void OnNext()
    {
        manager.NextLevel();
        gameObject.SetActive(false);
    }
}
