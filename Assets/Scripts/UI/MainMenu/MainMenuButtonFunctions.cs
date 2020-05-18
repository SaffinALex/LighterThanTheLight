using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonFunctions : MonoBehaviour
{
    private PanelUIManager panelUI;
    public string playSceneName;

    void Start()
    {
        panelUI = PanelUIManager.GetPanelUI();
    }
    public void PlayGame(){
        if (App.loadingManager.startSceneLoad(playSceneName) == -1)
            Debug.Log("MainMenuButtonFunctions : Play Scene name given doesn't match to any In-Build Scenes");
    }

    public void ShowScores(){
        panelUI.OpenScorePanel();
    }

    public void ShowOptions(){
        panelUI.GoTo(panelUI.optionsPanel);
    }

    public void QuitGame(){
        print("Main menu button : Quit");
        Application.Quit();
    }
}
