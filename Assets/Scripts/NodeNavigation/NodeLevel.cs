using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeLevel : NodeElement
{
    //Boss events
    static float bossProbability = 0.16f / 3f;
    static float hardBossProbability = 0.0025f / 3f;

    //Special events
    static float specialEventProbability = 0f / 3f;

    //Rare Item events
    static float RareItemProbability = 0.12f / 3f;
    static float SuperRareItemProbability = 0f / 3f;

    static float nothingProbability = 1f - (bossProbability + hardBossProbability + specialEventProbability + RareItemProbability + SuperRareItemProbability);

    static List<float> probabilitySpecialEvents = new List<float>() { bossProbability, hardBossProbability, specialEventProbability, RareItemProbability, SuperRareItemProbability, nothingProbability };

    protected Ui_LevelInfo ui;

    protected int GenerateRandomIndex() {
        float rand = Random.Range(0.0f, 1.0f);
        float totalProba = 0f;
        for (int i = 0; i < probabilitySpecialEvents.Count; i++)
        {
            if (rand > totalProba && rand <= totalProba + probabilitySpecialEvents[i]) return i;
            totalProba += probabilitySpecialEvents[i];
        }
        return 0;
    }

    LevelGeneratorInfo levelGeneratorInfo;
    bool HaveBegin = false;

    public override void InitializeNode(float score = 0){
        GameObject objectValue = new GameObject();
        objectValue.transform.parent = transform;
        objectValue.transform.localPosition = Vector3.zero;
        levelGeneratorInfo = objectValue.AddComponent<LevelGeneratorInfo>();
        scoreDifficulty = score + Random.Range(10, 30); //On augmente le niveau

        float moyenne = 0f;
        MinMax scoresMax = new MinMax();
        for (int i = 0; i < App.ALL_EVENTS["Wave"].Count; i++)
        {
            // Debug.Log(App.ALL_EVENTS["Wave"][i].GetScore());
            scoresMax.AddValue(App.ALL_EVENTS["Wave"][i].GetScore());
            moyenne += App.ALL_EVENTS["Wave"][i].GetScore();
        }
        moyenne /= App.ALL_EVENTS["Wave"].Count;
        // Debug.Log("Moyenne : " + moyenne + " / Min : " + scoresMax.Min + " / Max : " + scoresMax.Max);

        // Debug.Log("SCORE VOULU " + scoreDifficulty);
        // Debug.Log("Score moyenneur " + scoreDifficulty/5);
        if(scoreDifficulty / Node.CONSTANT_WAVE_GEN > scoresMax.Max) App.SetDifficulty(App.GetDifficulty() + 1);
        List<string> levelFlow = new List<string>() { "Wave", "Wave", "Wave" };
        levelGeneratorInfo.events = GeneratorLGI.GenerateLevel(scoreDifficulty, scoresMax.Min, levelFlow);

        List<int> eventAlreadyGet = new List<int>();
        //On lance 3 fois l'aléatoire pour les évènements spéciaux
        int NumberChanceSpecialEvent = 3;
        for(int i = NumberChanceSpecialEvent; i > 0; i--){
            int chanceRandom = GenerateRandomIndex(); //On lance l'aléatoire pour savoir quel évènement peut arriver
            if(!eventAlreadyGet.Contains(chanceRandom)){ //Si l'event n'a pas déjà eu lieu dans l'aléatoire
                if(chanceRandom < probabilitySpecialEvents.Count - 1) eventAlreadyGet.Add(chanceRandom); //On l'ajoute pour éviter de le dupliquer
                //Cas par cas
                if (chanceRandom == 0) {
                    Debug.Log("BOSS !");
                    List<Event> bossEvent = GeneratorLGI.GenerateLevel((scoreDifficulty / 3) * 2.5f, 50, new List<string>() { "Boss" });
                    if (bossEvent.Count > 0) {
                        Debug.Log(levelGeneratorInfo.events.Count - 1);
                        levelGeneratorInfo.events[levelGeneratorInfo.events.Count - 1] = bossEvent[0];
                    }
                }
                else if (chanceRandom == 1) {
                    Debug.Log("BOSS RARE !");
                }
                else if (chanceRandom == 2) {
                    Debug.Log("SPECIAL EVENT !");
                }
                else if (chanceRandom == 3) {
                    Debug.Log("RARE ITEM !");
                    List<Event> rewardEvent = GeneratorLGI.GenerateLevel(scoreDifficulty, 300, new List<string>() { "Reward" });
                    if (rewardEvent.Count > 0)
                    {
                        levelGeneratorInfo.events.Insert(Random.Range(1, levelGeneratorInfo.events.Count - 1), rewardEvent[0]);
                    }
                }
                else if (chanceRandom == 4) {
                    Debug.Log("ULTRA RARE ITEM !");
                }
            }
        }

        if(eventAlreadyGet.Count > 0){
            ui = Instantiate(Resources.Load("Prefabs/UI_3D/InfoVisual") as GameObject).GetComponent<Ui_LevelInfo>();
            ui.SetInfo(eventAlreadyGet);
            ui.transform.parent = transform;
            ui.transform.localPosition = new Vector3(0,2f,0);
            ui.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
    public override void Begin() {
        if(!HaveBegin){
            HaveBegin = true;

            // float moyenne = 0f;
            // for (int i = 0; i < App.ALL_EVENTS["Wave"].Count; i++)
            // {
            //     Debug.Log(App.ALL_EVENTS["Wave"][i].GetScore());
            //     moyenne += App.ALL_EVENTS["Wave"][i].GetScore();
            // }
            // moyenne /= App.ALL_EVENTS["Wave"].Count;
            // Debug.Log("Wanted Score " + moyenne * 3);
            // levelGeneratorInfo.events = GeneratorLGI.GenerateLevel(moyenne * 3, 10, new List<string>() { "Wave", "Wave", "Wave" });
            App.SetLevelGenerator(levelGeneratorInfo);

            // App.StartLevel();
            this.End();
        }
        this.End();
    }
}
