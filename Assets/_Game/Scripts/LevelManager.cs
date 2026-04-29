using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    public static LevelManager Instance { get; private set; }
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
        
    }

    public void LoadLevel(int level)
    {
        
    }

    public void OnPlay()
    {
        
    }

    public void OnPause()
    {
        
    }

    public void OnContinue()
    {
        
    }

    public void OnDespawn()
    {
        
    }

    public void OnWin()
    {
        
    }

    public void OnLose()
    {
        
    }

    public void OnRestart()
    {
        
    }

    public void OnNext()
    {
        
    }
}
