using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string DATA_KEY = "StackMakerDataKey";
    [SerializeField] private GameData gameData = new GameData();
    
    public int Score => gameData.score;
    public int Level => gameData.level;
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
        Debug.Log("OnInit");
        LoadData();
        gameData.score = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        gameData.score += scoreToAdd;
    }

    public void SetLevel(int level)
    {
        if (level >= gameData.maxLevel)
        {
            this.gameData.level = gameData.maxLevel;
        }
        else
        {
            this.gameData.level = level;
        }
    }

    public void SaveData()
    {
        var json = JsonUtility.ToJson(gameData);
        Debug.Log("Save Data: "+json);
        PlayerPrefs.SetString(DATA_KEY, json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        var json = PlayerPrefs.GetString(DATA_KEY,"");
        Debug.Log("Loaded Data: "+json);
        if (!string.IsNullOrEmpty(json))
        {
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            gameData = new GameData(0,1,5);
        }
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
        gameData.level = 1;
        SaveData();
    }
}
