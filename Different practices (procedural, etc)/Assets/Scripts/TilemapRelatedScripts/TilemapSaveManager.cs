using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSaveManager : MonoBehaviour
{
    [SerializeField]  Tilemap rectMap;  // it can be done with multiple maps
    [SerializeField] int levelIndex;

    public void SaveMap() {
        var newLevel = ScriptableObject.CreateInstance<ScriptableTileMapLevel>();

        newLevel.levelIndex = levelIndex;
        newLevel.name = $"Level {levelIndex}";

        newLevel.groundTiles = GetTilesFromMap(rectMap).ToList();

        ScriptableObjectUtility.SaveLevelFile(newLevel);

        //local function yields each tile, and then a linq to list creates a list of the tiles
        IEnumerable<SavedTile> GetTilesFromMap(Tilemap map)
        {
            // it takes all the positions and then we have to check if the position has tile
            foreach (var pos in map.cellBounds.allPositionsWithin)
            {
                if (map.HasTile(pos))
                {
                    var levelTile = map.GetTile<GameRuleTile>(pos);
                    yield return new SavedTile()
                    {
                        position = pos,
                        Tile = levelTile
                    };
                }
            }
        }

    }

    public void LoadMap() 
    {
        var level = Resources.Load<ScriptableTileMapLevel>($"Level { levelIndex}");
        if(level == null)
        {
            Debug.LogError($"Level { levelIndex} doesnt exist");
        }

        ClearMap();

        foreach (var savedTile in level.groundTiles)
        {
            switch (savedTile.Tile.tiletype)
            {
                case TileType.GrassTile:
                    rectMap.SetTile(savedTile.position, savedTile.Tile);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(); 
            }
        }

    }

    public void ClearMap() {

        rectMap.ClearAllTiles();


    }
}


#if UNITY_EDITOR

public static class ScriptableObjectUtility
{
    public static void SaveLevelFile(ScriptableTileMapLevel level)
    {
        AssetDatabase.CreateAsset(level, $"Assets/Resources/{level.name}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

#endif

//instead of scriptable object 
//create this new level struct
// serialize it and send the string to the database
public struct Level
{
        public int levelIndex;
   public List<SavedTile> groundTiles;


    public string Serialize()
    {
        var builder = new StringBuilder();

        builder.Append("g[");


            foreach (var groundTile in groundTiles)
            {
                builder.Append($"{(int)groundTile.Tile.tiletype}({groundTile.position.x},{groundTile.position.y})");
            }
        builder.Append("]");

        return builder.ToString();
    }
}