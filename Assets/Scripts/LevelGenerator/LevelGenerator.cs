using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    protected float timeTransitionEnd = 5f;
    protected float timerTransitionEnd = 0f;
    
    protected bool levelEnded = false;

    protected LevelGeneratorInfo levelGeneratorInfo;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Début du niveau !");
        PlayerShip playerShip = Instantiate(App.playerShip);
        playerShip.transform.position = Vector3.zero;
        playerShip.gameObject.name = "playerShip";
        
        levelGeneratorInfo = App.GetLevelGenerator();

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
