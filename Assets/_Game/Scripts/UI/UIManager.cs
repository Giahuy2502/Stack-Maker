using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private WinPanel winPanel;
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        winPanel.gameObject.SetActive(false);
    }

    public void OnWin()
    {
        winPanel.gameObject.SetActive(true);
    }

    private void OnDespawn()
    {
        
    }

}
