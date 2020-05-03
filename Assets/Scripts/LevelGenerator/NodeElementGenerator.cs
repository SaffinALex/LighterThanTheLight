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
        App.SetLevelGenerator(levelGeneratorInfo);
        SceneManager.LoadScene(2);
    }
}
