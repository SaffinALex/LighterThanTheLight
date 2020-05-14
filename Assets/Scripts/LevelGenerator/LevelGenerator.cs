using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public EnemyList enemyList = new EnemyList();
    protected float timeTransitionEnd = 5f;
    protected float timerTransitionEnd = 0f;
    
    protected bool levelEnded = false;

    protected LevelGeneratorInfo levelGeneratorInfo;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Début du niveau !");
        levelGeneratorInfo = App.GetLevelGenerator();
        App.SetEnemyList(enemyList);

        //Debut du niveau
        levelGeneratorInfo = GameObject.Instantiate(levelGeneratorInfo).GetComponent<LevelGeneratorInfo>();
        levelGeneratorInfo.Difficulty = App.GetDifficulty();
        levelGeneratorInfo.StartLevel();
        levelGeneratorInfo.GetEventEnd().AddListener(LevelEnd);
    }

    // Update is called once per frame
    void Update()
    {
        if(levelEnded){
            timerTransitionEnd += Time.deltaTime;
            if(timerTransitionEnd > timeTransitionEnd){
                App.EndLevel();
            }
        }   
    }

    void LevelEnd(){
        levelEnded = true;
        Debug.Log("LE NIVEAU EST TERMINE");
    }
}
