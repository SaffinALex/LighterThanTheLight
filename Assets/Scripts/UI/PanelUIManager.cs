using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelUIManager : MonoBehaviour
{
    private GameObject currentPanel;
    private GameObject background;
    public GameObject indexPanel;
    public GameObject scorePanel;
    public GameObject endLevelPanel;
    public GameObject endGamePanel;
    public string startSceneName;

    public string endLevelSceneName;

    public string endGameSceneName;
    public bool pauseKeyBeingPressed;

    private static PanelUIManager instance = null;
    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        pauseKeyBeingPressed = false;
        background = transform.GetChild(0).gameObject;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        currentPanel = null;
    }
    private void Update() {
        if(pauseKeyBeingPressed){
            if(Input.GetKeyUp("p")){
                pauseKeyBeingPressed = false;
            }
        }else{
            if(currentPanel != indexPanel && Input.GetKeyDown("p")){
                pauseKeyBeingPressed = true;
                OpenIndexMenu();
            }
            else if(currentPanel == indexPanel && Input.GetKeyDown("p")){
                pauseKeyBeingPressed = true;
                CloseIndexMenu();
            }
        }
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

    public void ToggleEndLevelPanel(){
        if(endLevelPanel != null && currentPanel != endLevelPanel){
            GoTo(endLevelPanel);
            Time.timeScale = 0;
        }
        else if(currentPanel == endLevelPanel){
            Time.timeScale = 1;
            GoToStartMenu();
        }
    }

    public void ToggleEndGamePanel(){
        if(endGamePanel != null && currentPanel != endGamePanel){
            GoTo(endGamePanel);
            endLevelPanel.GetComponent<EndGameScript>().initPanel();
            Time.timeScale = 0;
        }
        else if(currentPanel == endGamePanel){
            Time.timeScale = 1;
            GoToStartMenu();
            endLevelPanel.GetComponent<EndGameScript>().exitPanel();
        }
    }

    public void CloseIndexMenu(){
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        currentPanel = null;
        Time.timeScale = 1;
    }

    public void OpenScorePanel()
    {
        if (scorePanel != null)
        {
            GoTo(scorePanel);
            scorePanel.GetComponent<ScoresPanel>().UpdateLeaderBoard();
            Time.timeScale = 0;
        }
        else
            Debug.Log("PanelUIManager : scorePanel is NULL");
    }

    public void GoToStartMenu(){
        if(startSceneName == null)
            Debug.Log("PanelUIManager : Scene name for StartMenu is NULL");
        else{
            int returnStatus = GameObject.Find("LoadingPanel").GetComponent<LoadingPanelManager>().startSceneLoad(startSceneName);
            if (returnStatus == -1)
                Debug.Log("PanelUIManager : Scene name give doesn't match to any In-Build Scenes");
            else if(returnStatus == 0)
                Time.timeScale = 1;
            else
                Debug.Log("PanelUIManager : Scene already loading");
        }
    }

    public void GoToEndLevelMenu(){
        if(endLevelSceneName == null)
            Debug.Log("PanelUIManager : Scene name for EndLevelMenu is NULL");
        else{
            int returnStatus = GameObject.Find("LoadingPanel").GetComponent<LoadingPanelManager>().startSceneLoad(endLevelSceneName);
            if (returnStatus == -1)
                Debug.Log("PanelUIManager : Scene name give doesn't match to any In-Build Scenes");
            else if(returnStatus == 0)
                Time.timeScale = 1;
            else
                Debug.Log("PanelUIManager : Scene already loading");
        }
    }

    public void GoToEndGameMenu(){
        if(endGameSceneName == null)
            Debug.Log("PanelUIManager : Scene name for EndGameMenu is NULL");
        else{
            int returnStatus = GameObject.Find("LoadingPanel").GetComponent<LoadingPanelManager>().startSceneLoad(endGameSceneName);
            if (returnStatus == -1)
                Debug.Log("PanelUIManager : Scene name give doesn't match to any In-Build Scenes");
            else if(returnStatus == 0)
                Time.timeScale = 1;
            else
                Debug.Log("PanelUIManager : Scene already loading");
        }
    }

    public static PanelUIManager GetPanelUI(){
        if(instance == null){
            Debug.LogError("Aucune instance de LevelUI présente dans la scene");
            return null;
        }
        else
            return instance;
    }
}
