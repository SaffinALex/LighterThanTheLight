using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

  // Dictionary for player -> InputSystem
  // JSON representation
  /*
    {
      0 : [Input1, Input2],
      1 : [Input2, Input3]
    }
  */
  private static Dictionary<int, List<InputSystem> > dicoInputSystem = new Dictionary<int, List<InputSystem> >();

  /*
    Return an input by the name and the player value
  */
  public static float GetInput(string inputName, int player = 0){
    if(!PlayerExist(0)){
      InputManager.Subscribe((Resources.Load("Prefabs/InputDefault/InputKeyboard") as GameObject).GetComponent<KeyboardInputSystem>());
    }
    if(!PlayerExist(player)) return 0;
    float value = 0;
    //Get the max value distance from 0
    for(int i = 0; i < dicoInputSystem[player].Count; i++){
      float valueGet = dicoInputSystem[player][i].GetInput(inputName);
      if(System.Math.Abs(valueGet) > System.Math.Abs(value)) value = valueGet;
    }
    return value;
  }

  private static bool PlayerExist(int player = 0){
    return dicoInputSystem.ContainsKey(player);
  }

  private static void initPlayer(int player){
    if(!PlayerExist(player)) dicoInputSystem.Add(player, new List<InputSystem>());
  }

  public static bool Subscribe(InputSystem s, int player = 0){
    initPlayer(player);
    dicoInputSystem[player].Add(s);
    return true;
  }

  public static bool Unsubscribe(InputSystem s, int player = 0){
    if(!PlayerExist(player)) return false;
    int count = dicoInputSystem[player].Count;
    dicoInputSystem[player].Remove(s);
    return count > dicoInputSystem[player].Count;
  }
}
