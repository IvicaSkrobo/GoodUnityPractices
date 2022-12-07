using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Rule Tile", menuName = "Tiles/Rule Tile")]
public class GameRuleTile : RuleTile
{

    public Color color;
    public bool Walkable;
    public TileType tiletype;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
 
        base.GetTileData(position, tilemap, ref tileData);

        /*tileData.color = color;
        tileData.flags = TileFlags.LockColor;*/ //bugs up my tiles


    }

}


public enum TileType
{
    GrassTile = 0,
    WaterTile =1,
    LavaTile = 2


}
