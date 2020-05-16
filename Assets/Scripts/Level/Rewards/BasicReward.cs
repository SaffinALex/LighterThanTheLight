using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * -> Score obligatoire
 * -> Vie           0.05
 * -> Bouclier      0.01 
 * -> Argent        0.8
 * -> Rien          1 - (Vie + Bouclier + Argent)
 */
public class BasicReward : DropReward {

    static float lifeProbability = 0.02f;
    static float shieldProbability = 0.01f;
    static float moneyProbability = 0.6f;
    static float nothingProbability = 1f - (lifeProbability + shieldProbability + moneyProbability);

    static protected GameObject modelMoney;
    static protected GameObject modelLife;
    static protected GameObject modelShield;

    static List<float> allProbability = new List<float>() { lifeProbability, shieldProbability, moneyProbability, nothingProbability };

    void Awake(){
        modelMoney = Resources.Load("Prefabs/Rewards/Money") as GameObject;
        modelLife = Resources.Load("Prefabs/Rewards/Life") as GameObject;
        modelShield = Resources.Load("Prefabs/Rewards/Shield") as GameObject;
    }

    void Start(){
        // DropAReward();
    }

    protected override void DropAReward(){
        Debug.Log(modelMoney);
        int rewardIndex = GenerateRandomIndex();
        if (rewardIndex == 0) Instantiate(modelLife, transform.position, Quaternion.identity);
        else if (rewardIndex == 1) Instantiate(modelShield, transform.position, Quaternion.identity);
        else if (rewardIndex == 2) Instantiate(modelMoney, transform.position, Quaternion.identity);
        else if (rewardIndex == 3) Debug.Log("Rien...");
    }

    protected int GenerateRandomIndex(){
        float rand = Random.Range(0.0f, 1.0f);
        float totalProba = 0f;
        for (int i = 0; i < allProbability.Count; i++) {
            if (rand > totalProba && rand <= totalProba + allProbability[i]) return i;
            totalProba += allProbability[i];
        }
        return 0;
    }
}
