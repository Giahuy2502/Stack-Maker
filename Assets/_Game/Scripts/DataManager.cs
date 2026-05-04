using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int level;
    
    public int Score => score;
    public int Level => level;
    public static DataManager Instance { get; private set; }
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
        LoadData();
        score = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        level = PlayerPrefs.GetInt("Level",1);
    }

    private void OnDespawn()
    {
        SaveData();
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        SaveData(); 
    }
}
