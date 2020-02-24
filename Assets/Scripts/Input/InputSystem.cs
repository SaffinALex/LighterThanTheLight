using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {
  protected Dictionary<string, float> inputValues = new Dictionary<string, float>();

  private bool InputExist(string inputName){
    return inputValues.ContainsKey(inputName);
  }

  public float GetInput(string inputName){
    if(!InputExist(inputName)) return 0;
    return inputValues[inputName];
  }
}
