using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeLevel : NodeElement
{
    LevelGeneratorInfo levelGeneratorInfo;
    bool HaveBegin = false;
    void Start() {
        levelGeneratorInfo = gameObject.AddComponent<LevelGeneratorInfo>();
        Debug.Log("Je suis un NodeLevel");
    }
    
    public override void Begin() {
        if(!HaveBegin){
            HaveBegin = true;

            float moyenne = 0f;
            for (int i = 0; i < App.ALL_EVENTS["Wave"].Count; i++)
            {
                Debug.Log(App.ALL_EVENTS["Wave"][i].GetScore());
                moyenne += App.ALL_EVENTS["Wave"][i].GetScore();
            }
            moyenne /= App.ALL_EVENTS["Wave"].Count;
            Debug.Log("Wanted Score " + moyenne * 3);
            levelGeneratorInfo.events = GeneratorLGI.GenerateLevel(moyenne * 3, 10, new List<string>() { "Wave", "Wave", "Wave" });
            App.SetLevelGenerator(levelGeneratorInfo);

            App.StartLevel();
        }
        this.End();
    }
}
