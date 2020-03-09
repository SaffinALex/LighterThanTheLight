using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonFunctions : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("SceneTestAlex", LoadSceneMode.Single);
    }

    public void QuitGame(){
        print("Main menu button : Quit");
        Application.Quit();
    }
}
