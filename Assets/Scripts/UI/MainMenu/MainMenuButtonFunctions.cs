using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonFunctions : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("TestInLevelUI", LoadSceneMode.Single);
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
