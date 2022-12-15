using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    List<ScriptableUnits> units;

    public HeroBase SelectedHero { get; private set;}
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        units = Resources.LoadAll<ScriptableUnits>("Units").ToList();
    }

    public void SpawnHeroes()
    {
        var heroCount = 1;

        for(int i=0; i<heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<HeroBase>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnedTile = GridManager.Instance.GetSpawnTile<BaseTile>(Faction.Hero);

            randomSpawnedTile.SetUnit(spawnedHero);
        }

        GridSystemGameManager.Instance.ChangeGameState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<EnemyBase>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnedTile = GridManager.Instance.GetSpawnTile<BaseTile>(Faction.Enemy);

            randomSpawnedTile.SetUnit(spawnedEnemy);
        }

        GridSystemGameManager.Instance.ChangeGameState(GameState.HeroesTurn);
    }

    private T GetRandomUnit<T>(Faction faction) where T : UnitBase
    {
        return (T) units.Where(u => u.faction == faction).OrderBy(x => Random.value).First().unitPrefab;
    }


    public void SetSelectedHero(HeroBase hero)
    {
        SelectedHero = hero;

        GridMenuManager.Instance.ShowSelectedHero(hero);
    }
}
