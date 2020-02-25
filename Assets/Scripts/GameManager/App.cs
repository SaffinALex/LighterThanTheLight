using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyboardInputSystem))]
public class App : MonoBehaviour
{
    public void Awake(){
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        InputManager.Subscribe(GetComponent<KeyboardInputSystem>());
    }
}
