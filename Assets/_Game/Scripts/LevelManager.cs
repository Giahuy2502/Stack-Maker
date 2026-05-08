using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MyNamespace;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] private TileCollection tileCollection;
    [SerializeField] private Transform mapContainer;
    [SerializeField] private int currentLevel = 1;
    
    [SerializeField] private Player player;
    
    [SerializeField] private List<Pool> pools;
    private Dictionary<Vector3Int, (GameObject obj, int tileID)> spawnedTiles = new Dictionary<Vector3Int, (GameObject obj, int tileID)>();
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public static LevelManager Instance { get; private set; }
    private GameManager GameManager => GameManager.Instance;
    private DataManager DataManager => DataManager.Instance;
    public Action onWinGame;
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
        DataManager.OnInit();
        currentLevel = DataManager.Level;
        DataManager.AddScore(-DataManager.Score);
        player.OnDespawn();
        player.OnInit();
    }

    public void LoadLevel(int level)
    {
        string path = Application.dataPath + "/_Game/Levels/Level_" + level + ".json";
        if (!File.Exists(path))
        {
            Debug.LogError("Không tìm thấy file level tại: " + path);
            return;
        }
        string json = File.ReadAllText(path);
        LevelData data = JsonUtility.FromJson<LevelData>(json);
        foreach (TileData tData in data.tiles)
        {
            if (tData.tileID >= 0 && tData.tileID < tileCollection.Tiles.Count)
            {
                GameObject prefab = tileCollection.Tiles[tData.tileID].prefab;
                Vector3 worldPos = tData.position;
                worldPos.y = 0;
                int tileId = tData.tileID;
                Pool pool = pools[tileId];
                var newTile = pool.GetFromPool();
                newTile.transform.position = worldPos;
                newTile.transform.parent = mapContainer;
                newTile.transform.rotation = Quaternion.Euler(tData.rotation);
                Vector3Int gridPos = Vector3Int.FloorToInt(tData.position);
                if (!spawnedTiles.ContainsKey(gridPos))
                {
                    spawnedTiles.Add(gridPos, (newTile, tData.tileID));
                }
                    
            }
        }
        Debug.Log($"<color=yellow>Đã Load thành công: {data.levelName}</color>");
    }

    public void OnPlay()
    {
        GameManager.ChangeState(GameState.Playing);
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
    }

    public void OnContinue()
    {
        Time.timeScale = 1f;
    }

    public void OnDespawn()
    {
        ClearCurrentMap();
        GameManager.ChangeState(GameState.Start);
    }

    public void OnWin()
    {
        onWinGame?.Invoke();
    }

    public void OnLose()
    {
        OnPause();
    }

    public void OnRestart()
    {
        OnDespawn();
        LoadLevel(DataManager.Level);
        OnInit();
        OnPlay();
    }

    public void OnNext()
    {
        OnDespawn();
        DataManager.SetLevel(currentLevel+1); // lưu level mới
        LoadLevel(DataManager.Level);
        DataManager.SaveData();
        OnInit();
        OnPlay();
    }
    
    public void ClearCurrentMap()
    {
        foreach (var item in spawnedTiles)
        {
            int tileID = item.Value.tileID;
            pools[tileID].ReturnToPool(item.Value.obj);
        }
        spawnedTiles.Clear();
    }
    
}
