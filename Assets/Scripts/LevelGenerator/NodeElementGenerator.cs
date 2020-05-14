using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeElementGenerator : MonoBehaviour
{
    public LevelGeneratorInfo levelGeneratorInfo;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bonjour je suis le node");
        float moyenne = 0f;
        for(int i = 0; i < App.ALL_EVENTS["Wave"].Count; i++){
            Debug.Log(App.ALL_EVENTS["Wave"][i].GetScore());
            moyenne += App.ALL_EVENTS["Wave"][i].GetScore();
        }
        moyenne /= App.ALL_EVENTS["Wave"].Count;
        Debug.Log("Wanted Score " + moyenne * 3);
        levelGeneratorInfo.events = GeneratorLGI.GenerateLevel(moyenne * 3, 10, new List<string>(){"Wave", "Wave", "Wave"});
        App.SetLevelGenerator(levelGeneratorInfo);
        SceneManager.LoadScene(2);
    }
}
