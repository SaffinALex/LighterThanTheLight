using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(KeyboardInputSystem))]
public class App : MonoBehaviour
{
    public static App app;
    public MusicManager sfxObject;
    public static MusicManager sfx;
    public static PlayerManager playerManager = new PlayerManager();

    public void Awake(){
        DontDestroyOnLoad(gameObject);
        app = this;
    }

    void Start(){
        InputManager.Subscribe(GetComponent<KeyboardInputSystem>());

        DontDestroyOnLoad(sfxObject.gameObject);
        sfx = sfxObject;

        sfx.PlaySound("TestSound", 3);

        SceneManager.LoadScene(1);
    }

    public static bool IsInit(){
        return app != null;
    }
}
