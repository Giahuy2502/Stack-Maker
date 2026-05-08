using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameData
{
    public int score;
    public int level;
    public int maxLevel;

    public GameData()
    {
        
    }
    public GameData(int score, int level, int maxLevel)
    {
        this.score = score;
        this.level = level;
        this.maxLevel = maxLevel;
    }
}
