using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public List<TileData> tiles = new List<TileData>();
}
[System.Serializable]
public class TileData
{
    public Vector3Int position;
    public Vector3 rotation;
    public int tileID;
}