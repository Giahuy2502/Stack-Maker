using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] private TileCollection tileCollection;
    [SerializeField] private Transform mapContainer;
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
