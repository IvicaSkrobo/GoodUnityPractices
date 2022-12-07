using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    int height;
    [SerializeField]
    int width;
    [SerializeField] TilePrefab tilePrefab;


    Dictionary<Vector2, TilePrefab> tileDictionary;

    public void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tileDictionary = new Dictionary<Vector2, TilePrefab>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = Instantiate(tilePrefab, new Vector2(x, y), Quaternion.identity, this.transform);

                bool isOffset = (x % 2 == 0 && y % 2 == 1 || x % 2 == 1 && y % 2 == 0);

                tile.Init(isOffset);
                tileDictionary.Add(new Vector2(x, y), tile);


            }
        }

        Camera.main.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);

    }

    public TilePrefab OnTileSelected(Vector2 position)
    {
        if(tileDictionary.TryGetValue(position, out TilePrefab tileSelected))
        {
            return tileSelected;
        }

        return null;


    }

}
