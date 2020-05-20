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
    public RessourcesPanel ressourcePanel;
    public GameObject optionsPanel;
    public GameObject endLevelPanel;
    public GameObject endGamePanel;
    public string startSceneName;

    public string endLevelSceneName;

    public string endGameSceneName;
    public bool pauseKeyBeingPressed;

    public bool inGame = false;
    public bool inPauseMenu = false;

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
        DontDestroyOnLoad(gameObject);
    }
    private void Update() {
        if(pauseKeyBeingPressed){
            if(Input.GetKeyUp("escape")){
                pauseKeyBeingPressed = false;
            }
        }else
        {
            if (inGame && currentPanel == null && Input.GetKeyDown("escape")){
                pauseKeyBeingPressed = true;
                ToggleIndexPanel();
            }
            else if(inGame && currentPanel == indexPanel && Input.GetKeyDown("escape"))
            {
                pauseKeyBeingPressed = true;
                ToggleIndexPanel();
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

    public void ToggleIndexPanel()
    {
        Debug.Log("try to close");
        if (indexPanel != null && currentPanel == null)
        {
            OpenIndexMenu();
            inPauseMenu = true;
            Time.timeScale = 0;
        }
        else if (currentPanel == indexPanel)
        {
            Debug.Log("close");
            Time.timeScale = 1;
            inPauseMenu = false;
            CloseIndexMenu();
        }
    }

    public void ToggleEndLevelPanel(){
        if(endLevelPanel != null && currentPanel != endLevelPanel)
        {
            endLevelPanel.GetComponent<EndLevelScript>().initPanel();
            GoTo(endLevelPanel);
            Time.timeScale = 0;
        }
        else if(currentPanel == endLevelPanel){
            Time.timeScale = 1;
            App.CloseLevel();
            GoToEndLevelMenu();
            CloseIndexMenu();
        }
    }

    public void ToggleEndGamePanel(){

        if(endGamePanel != null && currentPanel != endGamePanel)
        {
            endGamePanel.GetComponent<EndGameScript>().initPanel();
            GoTo(endGamePanel);
            Time.timeScale = 0;
        }
        else if(currentPanel == endGamePanel)
        {
            Time.timeScale = 1;
            endGamePanel.GetComponent<EndGameScript>().exitPanel();
            App.CloseGame();
            GoToStartMenu();
            CloseIndexMenu();
        }
    }

    public void CloseIndexMenu()
    {
        Debug.Log("close");
        currentPanel = null;
        foreach (Transform child in transform)
        {
            if(child.gameObject != ressourcePanel.gameObject)
                child.gameObject.SetActive(false);
        }
        inPauseMenu = false;
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

    public void CloseOptionMenu() {
        currentPanel = null;
        foreach (Transform child in transform)
            if (child.gameObject != ressourcePanel.gameObject)
                child.gameObject.SetActive(false);
        if (inPauseMenu)
            ToggleIndexPanel();
        Time.timeScale = 1;
    }

    public void GoToStartMenu(){
        if(startSceneName == null)
            Debug.Log("PanelUIManager : Scene name for StartMenu is NULL");
        else{
            int returnStatus = App.loadingManager.startSceneLoad(startSceneName);
            CloseIndexMenu();
            App.CloseGame();
            if (returnStatus == -1)
                Debug.Log("PanelUIManager : Scene name give doesn't match to any In-Build Scenes");
            else
                Debug.Log("PanelUIManager : Scene already loading");
        }
    }

    public void GoToEndLevelMenu(){
        if(endLevelSceneName == null)
            Debug.Log("PanelUIManager : Scene name for EndLevelMenu is NULL");
        else{
            int returnStatus = App.loadingManager.startSceneLoad(endLevelSceneName);
            CloseIndexMenu();
            if (returnStatus == -1)
                Debug.Log("PanelUIManager : Scene name give doesn't match to any In-Build Scenes");
            else
                Debug.Log("PanelUIManager : Scene already loading");
        }
    }

    public void GoToEndGameMenu(){
        if(endGameSceneName == null)
            Debug.Log("PanelUIManager : Scene name for EndGameMenu is NULL");
        else{
            int returnStatus = App.loadingManager.startSceneLoad(endGameSceneName);
            CloseIndexMenu();
            if (returnStatus == -1)
                Debug.Log("PanelUIManager : Scene name give doesn't match to any In-Build Scenes");
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
