using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingPanelManager : MonoBehaviour
{
    private void Start() {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
    private string sceneToLoad;
    public bool startSceneLoad(string sceneName){
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
            return false;

        sceneToLoad = sceneName;
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncOperation());
        return true;
    }

    IEnumerator LoadAsyncOperation(){
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneToLoad,LoadSceneMode.Single);
        while(!gameLevel.isDone)
        {
            GetComponentInChildren<LoadingWheel>().updateLoadingWheel(gameLevel.progress);

            yield return new WaitForEndOfFrame();
        }
    }
}
