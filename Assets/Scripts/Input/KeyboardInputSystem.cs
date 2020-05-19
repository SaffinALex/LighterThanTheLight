using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputSystem : InputSystem
{
  [Header("Movement Inputs")]
  public KeyCode up;
  public KeyCode down;
  public KeyCode left;
  public KeyCode right;

  [Header("Interaction Inputs")]
  public KeyCode dash;
  public KeyCode onde;
  public KeyCode pause;

  new protected void Start() {
    base.Start();
    InputManager.Subscribe(this, player);
  }

  // Update is called once per frame
  void Update() {
    SetInputValue("horizontal", correctInput(left) && correctInput(right) ? getInput(left) > 0 ? - getInput(left) : getInput(right) > 0 ? getInput(right) : 0 : Input.GetAxis ("Horizontal"));
    SetInputValue("vertical", correctInput(up) && correctInput(down) ? getInput(down) > 0 ? - getInput(down) : getInput(up) > 0 ? getInput(up) : 0 : Input.GetAxis ("Vertical"));

    SetInputValue("up", correctInput(up) ? getInput(up) : GetInput("vertical") > 0 ? GetInput("vertical") : 0);
    SetInputValue("down", correctInput(down) ? getInput(down) : GetInput("vertical") < 0 ? - GetInput("vertical") : 0);
    SetInputValue("left", correctInput(left) ? getInput(left) : GetInput("horizontal") < 0 ? - GetInput("horizontal") : 0);
    SetInputValue("right", correctInput(right) ? getInput(right) : GetInput("horizontal") > 0 ? GetInput("horizontal") : 0);

    SetInputValue("dash", correctInput(dash) ? getInput(dash) : GetInput("Fire1"));
    SetInputValue("onde", correctInput(onde) ? getInput(onde) : GetInput("Jump"));
    SetInputValue("pause", correctInput(pause) ? getInput(pause) : GetInput("Cancel"));

    /*
    foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode))){
        if (Input.GetKey(kcode)) Debug.Log("KeyCode down: " + kcode);
    }
    */

  }

  bool correctInput(KeyCode key){
    return key != KeyCode.None;
  }

  float getInput(KeyCode key){
    return Input.GetKey(key) ? 1 : 0;
  }
}
