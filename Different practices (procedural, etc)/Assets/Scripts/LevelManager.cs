using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;


    [SerializeField]
    float speed = 3f;
    
    [SerializeField]
    GameObject loadingScreen;

    [SerializeField]
    Image progressBar;

    float target = 0f;
    AsyncOperation currentScene;

    public void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, Time.deltaTime * speed);
        if(progressBar.fillAmount==1 && currentScene!=null)
        {
            currentScene.allowSceneActivation = true;

            currentScene = null;
            loadingScreen.SetActive(false);
            //instead do an animation here for the loading screen
        }
    }
    public void LoadSceneInt(int sceneIndex)
    {
        if (loadingScreen.activeInHierarchy)
        { return; }
        target = 0;

        StartCoroutine(LoadScene(sceneIndex));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
     
        loadingScreen.SetActive(true);
        progressBar.fillAmount = 0;
        currentScene = SceneManager.LoadSceneAsync(sceneIndex);
        currentScene.allowSceneActivation = false;
        while(currentScene.progress<0.9f)
        {
            target = currentScene.progress;
            yield return new WaitForEndOfFrame();
        }

        target = 1f;

    }

    
}
