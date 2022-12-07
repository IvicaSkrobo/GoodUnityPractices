using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptableTileMapLevel : ScriptableObject
{
    public int levelIndex;
    public List<SavedTile> groundTiles;
   // public List<SavedTile> unitTiles;
}

[Serializable]
public class SavedTile
{
    public Vector3Int position;
    public GameRuleTile Tile;
}
