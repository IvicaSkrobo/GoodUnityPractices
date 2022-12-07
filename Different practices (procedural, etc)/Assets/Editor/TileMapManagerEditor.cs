using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilemapSaveManager))]
public class TileMapManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (TilemapSaveManager) target;

        if(GUILayout.Button("Save Map"))
        {
            script.SaveMap();
        }

        if (GUILayout.Button("Load Map"))
        {
            script.LoadMap();
        }

        if (GUILayout.Button("Clear Map"))
        {
            script.ClearMap();
        }
    }
}
