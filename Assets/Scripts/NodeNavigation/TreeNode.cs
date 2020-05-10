using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreeNode : MonoBehaviour
{
    [SerializeField] protected LevelGeneratorInfo levelGeneratorInfo;
    static GameObject treeNode;
    // Start is called before the first frame update
    void Start()
    {
        //Singleton
        if(!treeNode){
            treeNode = gameObject;
        }else{
            Destroy(gameObject);
        }

        List<string> levelFlow = new List<string>();
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        levelFlow.Add("Wave");
        List<Event> levelEvents = GeneratorLGI.GenerateLevel(6 * levelFlow.Count, 3, levelFlow);
        levelGeneratorInfo.events = levelEvents;
        Debug.Log(levelGeneratorInfo);
        App.SetLevelGenerator(levelGeneratorInfo);

        Debug.Log("START SCENE");
        SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
