using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelUIManager : MonoBehaviour
{
    private GameObject currentPanel;
    private GameObject background;
    public GameObject indexPanel;
    public GameObject endLevelPanel;
    public GameObject endGamePanel;
    public string startSceneName;

    public string endLevelSceneName;

    public string endGameSceneName;
    // Start is called before the first frame update
    void Start()
    {
        background = transform.GetChild(0).gameObject;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        currentPanel = null;
    }
    private void Update() {
        if(currentPanel != indexPanel && Input.GetKey("p"))
            OpenIndexMenu();
        else if(currentPanel == indexPanel && Input.GetKey("p"))
            CloseIndexMenu();
    }

    public void GoTo(GameObject target){
        if(currentPanel == null)
            background.SetActive(true);
        else
            currentPanel.SetActive(false);

        target.SetActive(true);
        currentPanel = target;
    }   

    public void OpenIndexMenu(){
        if(indexPanel != null){
            GoTo(indexPanel);
            Time.timeScale = 0;
        }else
            Debug.Log("PanelUIManager : Index Panel is NULL");
    }

    public void OpenEndLevelPanel(){
        if(endLevelPanel != null)
            GoTo(endLevelPanel);
        else
            Debug.Log("PanelUIManager : End Level Panel is NULL");
    }

    public void OpenEndGamePanel(){
        if(endGamePanel != null)
            GoTo(endGamePanel);
        else
            Debug.Log("PanelUIManager : End Game Panel is NULL");
    }

    public void CloseIndexMenu(){
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        currentPanel = null;
        Time.timeScale = 1;
    }

    public void GoToStartMenu(){
        if(startSceneName == null)
            Debug.Log("PanelUIManager : Scene name for StartMenu is NULL");
        else
            if (Application.CanStreamedLevelBeLoaded(startSceneName))
                SceneManager.LoadScene(startSceneName);
            else
                Debug.Log("PanelUIManager : Scene name give doesn't match to any In-Build Scenes");
    }

    public void GoToEndLevelMenu(){
        if(endLevelSceneName == null)
            Debug.Log("PanelUIManager : Scene name for EndLevelMenu is NULL");
        else
            if (Application.CanStreamedLevelBeLoaded(endLevelSceneName))
                SceneManager.LoadScene(endLevelSceneName);
            else
                Debug.Log("PanelUIManager : Scene name give doesn't match to any In-Build Scenes");
    }

    public void GoToEndGameMenu(){
        if(endGameSceneName == null)
            Debug.Log("PanelUIManager : Scene name for EndGameMenu is NULL");
        else
            if (Application.CanStreamedLevelBeLoaded(endGameSceneName))
                SceneManager.LoadScene(endGameSceneName);
            else
                Debug.Log("PanelUIManager : Scene name give doesn't match to any In-Build Scenes");
    }
}
