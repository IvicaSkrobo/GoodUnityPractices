using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Unit",menuName ="Scriptable Unit")]
public class ScriptableUnits : ScriptableObject
{
    public string unitName;
    public Faction faction;
    public UnitBase unitPrefab;
}

public enum Faction
{
    Hero =0,
    Enemy = 1
}