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

    public void OnInit()
    {
        LoadData();
        score = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Level", level);
        Debug.Log("Save Data: level = "+level);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        level = PlayerPrefs.GetInt("Level",1);
        Debug.Log("Load Data: level = "+level);
    }

    private void OnDespawn()
    {
        SaveData();
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveData(); 
        }
    }

    [ContextMenu("Reset Level")]
    public void ResetLevel()
    {
        level = 1;
        SaveData();
    }
}
