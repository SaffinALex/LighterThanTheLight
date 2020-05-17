using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonFunctions : MonoBehaviour
{
    public string playSceneName;
    public void PlayGame(){
        if (App.loadingManager.startSceneLoad(playSceneName) == -1)
            Debug.Log("MainMenuButtonFunctions : Play Scene name given doesn't match to any In-Build Scenes");
    }

    public void ShowScores(){
        print("Main menu button : Scores");
    }

    public void ShowOptions(){
        print("Main menu button : Options");
    }

    public void QuitGame(){
        print("Main menu button : Quit");
        Application.Quit();
    }
}
