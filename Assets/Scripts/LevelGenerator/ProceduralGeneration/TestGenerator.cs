using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Hello");
        // foreach (KeyValuePair<string, List<Event>> entry in App.ALL_EVENTS) {
        //     Debug.Log(entry.Key + " -> " + entry.Value + " - Size : " + entry.Value.Count);
        //     for(int i = 0; i < entry.Value.Count; i++){
        //         Debug.Log(i + " : " + entry.Value[i].GetScore());
        //     }
        // }

        List<string> levelFlow = new List<string>();
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        Debug.Log("Demande de génération d'un level de taille " + levelFlow.Count + " & Score = " + 6 * levelFlow.Count);
        GeneratorLGI.GenerateLevel(6 * levelFlow.Count, 3, levelFlow);
        GeneratorLGI.GenerateLevel(6 * levelFlow.Count, 3, levelFlow);
        GeneratorLGI.GenerateLevel(6 * levelFlow.Count, 3, levelFlow);
        GeneratorLGI.GenerateLevel(6 * levelFlow.Count, 3, levelFlow);
        GeneratorLGI.GenerateLevel(6 * levelFlow.Count, 3, levelFlow);
        GeneratorLGI.GenerateLevel(6 * levelFlow.Count, 3, levelFlow);
    }
}
