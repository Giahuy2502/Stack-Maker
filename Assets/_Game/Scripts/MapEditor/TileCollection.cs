using System;
using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;
[CreateAssetMenu(fileName = "TileCollection", menuName = "Editor/Tile Collection")]
public class TileCollection : ScriptableObject
{
    [SerializeField] private List<Tile> tiles = new List<Tile>();
    public List<Tile> Tiles => tiles;
}

[Serializable]
public class Tile
{
    public String name;
    public TitleType type;
    public GameObject prefab;
}