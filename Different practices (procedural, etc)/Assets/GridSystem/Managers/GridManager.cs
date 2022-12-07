using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public static GridManager Instance;

    [SerializeField]
    int height;
    [SerializeField]
    int width;
    [SerializeField] BaseTile mountainTilePrefab;
    [SerializeField] BaseTile grassTilePrefab;

    Dictionary<Vector2, BaseTile> tilesDict;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void GenerateGrid()
    {
        tilesDict = new Dictionary<Vector2, BaseTile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
              
                var randomTile = Random.Range(0, 6) > 2 ? grassTilePrefab : mountainTilePrefab; 
                var tile = Instantiate(randomTile, new Vector2(x, y), Quaternion.identity, this.transform);


                tile.Init(x,y);
                tilesDict.Add(new Vector2(x, y), tile);


            }
        }

        Camera.main.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);


        GridSystemGameManager.Instance.ChangeGameState(GameState.SpawnHeroes);
    }

    public T GetSpawnTile<T>(Faction faction) where T : BaseTile
    {
        if(faction == Faction.Hero)
        {
            return (T) tilesDict.Where(t => t.Key.x < width / 2 && t.Value.WalkableCheck).OrderBy(x => Random.value).First().Value;
        }
        else if(faction==Faction.Enemy)
        {
            return (T)tilesDict.Where(t => t.Key.x > width / 2 && t.Value.WalkableCheck).OrderBy(x => Random.value).First().Value;
        }
        return null;
    }

 


    public BaseTile OnTileSelected(Vector2 position)
    {
        if(tilesDict.TryGetValue(position, out BaseTile tileSelected))
        {
            return tileSelected;
        }

        return null;


    }

}
