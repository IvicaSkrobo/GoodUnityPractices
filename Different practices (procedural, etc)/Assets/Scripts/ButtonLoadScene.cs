using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (Button))]
public class ButtonLoadScene : MonoBehaviour
{
    [SerializeField]
    int levelToLoad;

    private void Start() => GetComponent<Button>().onClick.AddListener(()=> LoadSceneLvl(levelToLoad));

   

    public void LoadSceneLvl(int scene)
    {
        if (LevelManager.Instance != null)
        LevelManager.Instance.LoadSceneInt(scene);
    }
}
