using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputSystem : InputSystem
{

  public int player = 0;

  void Start() {
    InputManager.Subscribe(this, player);
    Debug.Log("coucou");
  }

  // Update is called once per frame
  void Update() {

  }
}
