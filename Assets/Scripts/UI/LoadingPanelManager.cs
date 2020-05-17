using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanelManager : MonoBehaviour
{
    public Color fadeColor = new Color(0, 0, 0);
    public float fadeDuration = 1.5f;

    public float loadTime = 2f;
    private float loadTimer;

    private CanvasGroup canvasGrp;
    private bool isLoadingScene;
    private bool isFaded;
    private bool loadingOperationIsOn;
    private string sceneToLoad;

    void Start() {
        canvasGrp = GetComponentInChildren<CanvasGroup>();
        GetComponentInChildren<Image>().color = fadeColor;
        isLoadingScene = false;
        isFaded = false;
        loadingOperationIsOn = false;
        canvasGrp.alpha = 0f;
        canvasGrp.blocksRaycasts = false;
    }

    void Update()
    {
        //Debug.Log(isLoadingScene + " et " + isFaded);
        if (isLoadingScene && !isFaded)
        {
            canvasGrp.alpha += getFadeSpeed() * Time.unscaledDeltaTime;
            if (canvasGrp.alpha >= 1f)
            {
                isFaded = true;
                canvasGrp.alpha = 1f;
                loadTimer = 0;
            }
        }
        else if(!isLoadingScene && isFaded)
        {
            loadTimer += Time.unscaledDeltaTime;
            if (loadTimer >= loadTime)
            {

                Time.timeScale = 1;
                canvasGrp.alpha -= getFadeSpeed() * Time.unscaledDeltaTime;
                if (canvasGrp.alpha <= 0f)
                {
                    isFaded = false;
                    canvasGrp.alpha = 0f;
                    canvasGrp.blocksRaycasts = false;
                }
            }
        }
        else if (isLoadingScene && isFaded)
        {
            if (!loadingOperationIsOn)
            {
                StartCoroutine(LoadAsyncOperation());
                
            }
        }
    }

    private float getFadeSpeed()
    {
        return 1f / fadeDuration;
    }
    public int startSceneLoad(string sceneName){
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
            return -1;
        if (isLoadingScene)
            return -2;

        sceneToLoad = sceneName;
        isLoadingScene = true;
        isFaded = false;
        loadingOperationIsOn = false;
        canvasGrp.blocksRaycasts = true;
        Time.timeScale = 0;
        return 0;
    }

    IEnumerator LoadAsyncOperation(){
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneToLoad,LoadSceneMode.Single);
        gameLevel.completed += OperationOnCompleted;
        while(!gameLevel.isDone){
            yield return new WaitForEndOfFrame();
        }
    }

    private void OperationOnCompleted(AsyncOperation obj)
    {
        Debug.Log("done");
        isLoadingScene = false;
    }
}
