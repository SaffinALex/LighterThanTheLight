using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(KeyboardInputSystem))]
public class App : MonoBehaviour
{
    public MusicManager sfxObject;
    public static MusicManager sfx;

    public void Awake(){
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        InputManager.Subscribe(GetComponent<KeyboardInputSystem>());

        DontDestroyOnLoad(sfxObject.gameObject);
        sfx = sfxObject;

        sfx.PlaySound("TestSound", 3);

        SceneManager.LoadScene(1);
    }
}
