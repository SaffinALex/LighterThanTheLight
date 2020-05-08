using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingPanelManager : MonoBehaviour
{
    private bool isLoadingScene;
    private void Start() {
        isLoadingScene = false;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
    private string sceneToLoad;
    public int startSceneLoad(string sceneName){
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
            return -1;
        if (isLoadingScene)
            return -2;

        sceneToLoad = sceneName;
        isLoadingScene = true;
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncOperation());
        return 0;
    }

    IEnumerator LoadAsyncOperation(){
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneToLoad,LoadSceneMode.Single);
        if(gameLevel.isDone)
            isLoadingScene = false;
        while(!gameLevel.isDone){
            GetComponentInChildren<LoadingWheel>().updateLoadingWheel(gameLevel.progress);

            yield return new WaitForEndOfFrame();
        }
    }
}
