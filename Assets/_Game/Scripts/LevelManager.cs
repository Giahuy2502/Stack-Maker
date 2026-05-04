using System.Collections;
using System.Collections.Generic;
using System.IO;
using MyNamespace;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] private TileCollection tileCollection;
    [SerializeField] private Transform mapContainer;
    [SerializeField] private int currentLevel = 1;
    
    [SerializeField] private Player player;
    private Dictionary<Vector3Int, GameObject> spawnedTiles = new Dictionary<Vector3Int, GameObject>();
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public static LevelManager Instance { get; private set; }
    private GameManager manager => GameManager.Instance;
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
                var newTile = Instantiate(prefab, worldPos, Quaternion.Euler(tData.rotation), mapContainer);
                Vector3Int gridPos = Vector3Int.FloorToInt(tData.position); 
                if (!spawnedTiles.ContainsKey(gridPos)) 
                    spawnedTiles.Add(gridPos, newTile);
            }
        }
        Debug.Log($"<color=yellow>Đã Load thành công: {data.levelName}</color>");
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
        // xoa map hien tai
        ClearCurrentMap();
    }

    public void OnWin()
    {
        
    }

    public void OnLose()
    {
        
    }

    public void OnRestart()
    {
        OnDespawn();
        LoadLevel(currentLevel);
        manager.ChangeState(GameState.Playing);
        OnInit();
        OnPlay();
    }

    public void OnNext()
    {
        OnDespawn();
        LoadLevel(currentLevel);
        manager.ChangeState(GameState.Playing);
        OnInit();
        OnPlay();
    }
    
    public void ClearCurrentMap()
    {
        foreach (var item in spawnedTiles)
        {
            Destroy(item.Value);
        }
        spawnedTiles.Clear();
    }
}
