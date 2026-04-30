using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Editor : MonoBehaviour
{
    [SerializeField] private TileCollection tileCollection;
    [SerializeField] private Transform mapContainer;
    [SerializeField] private Grid grid;
    [SerializeField] private string currentLevelName = "1";
    private Dictionary<Vector3Int, GameObject> spawnedTiles = new Dictionary<Vector3Int, GameObject>();
    private int currentTileIndex = -1;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentTileIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentTileIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentTileIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentTileIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) currentTileIndex = -1;
        
        if (Input.GetMouseButton(0)) 
        {
            if (currentTileIndex != -1)
            {
                SpawnTile(currentTileIndex);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.R)) RotateTile(90f);
                if (Input.GetKeyDown(KeyCode.L)) RotateTile(-90f);
            }
        }
        if (Input.GetMouseButton(1))
        {
            RemoveTile();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveMap(currentLevelName);
        }
    }
    private void SpawnTile(int index)
    {
        if (tileCollection == null || index >= tileCollection.Tiles.Count) return;
        Vector3Int gridPos = GetMouseGridPosition();
        if (spawnedTiles.ContainsKey(gridPos))
        {
            if(spawnedTiles[gridPos].name == tileCollection.Tiles[index].prefab.name + "(Clone)") 
                return; 
            Destroy(spawnedTiles[gridPos]);
            spawnedTiles.Remove(gridPos);
        }
        Vector3 spawnWorldPos = grid.GetCellCenterWorld(gridPos);
        spawnWorldPos.y = 0;
        GameObject prefab = tileCollection.Tiles[index].prefab;
        GameObject newTile = Instantiate(prefab, spawnWorldPos, Quaternion.identity, mapContainer);
        spawnedTiles.Add(gridPos, newTile);
    }
    private void RemoveTile()
    {
        Vector3Int gridPos = GetMouseGridPosition();
        if (spawnedTiles.ContainsKey(gridPos))
        {
            Destroy(spawnedTiles[gridPos]);
            spawnedTiles.Remove(gridPos);
        }
    }
    private void RotateTile(float angle)
    {
        Vector3Int gridPos = GetMouseGridPosition();
        if (spawnedTiles.ContainsKey(gridPos))
        {
            GameObject target = spawnedTiles[gridPos];
            target.transform.Rotate(Vector3.up, angle);
        }
    }
    private Vector3Int GetMouseGridPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 worldPoint = ray.GetPoint(enter);
            return grid.WorldToCell(worldPoint);
        }
        return Vector3Int.zero;
    }
    public void SaveMap(string levelName)
    {
        LevelData levelData = new LevelData();
        levelData.levelName = "Level_"+levelName;

        foreach (var item in spawnedTiles)
        {
            TileData tData = new TileData();
            tData.position = item.Value.transform.position;
            tData.rotation = item.Value.transform.eulerAngles;
            tData.tileID = GetTileID(item.Value); 
            levelData.tiles.Add(tData);
        }

        string json = JsonUtility.ToJson(levelData, true);
        string path = Application.dataPath + "/_Game/Levels/" + levelData.levelName + ".json";
    
        File.WriteAllText(path, json);
        Debug.Log("Đã lưu Level tại: " + path);
    }
    private int GetTileID(GameObject instance)
    {
        string instanceName = instance.name.Replace("(Clone)", "").Trim();
        for (int i = 0; i < tileCollection.Tiles.Count; i++)
        {
            if (tileCollection.Tiles[i].prefab.name == instanceName)
                return i;
        }
        return -1;
    }
    
    public void LoadLevel(string levelName)
    {
        string path = Application.dataPath + "/_Game/Levels/Level_" + levelName + ".json";
        if (!File.Exists(path))
        {
            Debug.LogError("Không tìm thấy file level tại: " + path);
            return;
        }
        string json = File.ReadAllText(path);
        LevelData data = JsonUtility.FromJson<LevelData>(json);
        ClearCurrentMap();
        foreach (TileData tData in data.tiles)
        {
            if (tData.tileID >= 0 && tData.tileID < tileCollection.Tiles.Count)
            {
                GameObject prefab = tileCollection.Tiles[tData.tileID].prefab;
                Vector3 worldPos = tData.position;
                worldPos.y = 0;
                GameObject newTile = Instantiate(prefab, worldPos, Quaternion.Euler(tData.rotation), mapContainer);
                Vector3Int gridPos = Vector3Int.FloorToInt(tData.position); 
                if (!spawnedTiles.ContainsKey(gridPos)) 
                    spawnedTiles.Add(gridPos, newTile);
            }
        }
        Debug.Log($"<color=yellow>Đã Load thành công: {data.levelName}</color>");
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
