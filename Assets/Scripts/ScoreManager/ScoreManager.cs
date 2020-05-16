using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

[SerializeField]
public class ScoreManager
{
    [System.Serializable]
    public struct ScoreElement {
        public string name;
        public float score;
        public ScoreElement(string name, float score){
            this.name = name;
            this.score = score;
        }
    }

    static readonly protected int maxScoreStores = 5;
    static readonly protected string fileStore = "bestsScores.save";

    [SerializeField]
    static protected List<ScoreElement> bestScores;

    /**
     * Permet de récupérer la liste des meilleurs scores
     * @return List<ScoreElement>
     */
    static public List<ScoreElement> GetBestsScores(){
        if(bestScores == null) LoadBestScores();
        return bestScores;
    }

    /**
     * Permet de sauvegarder un score selon un nom et un score donné
     * L'algorithme se charge de checker si c'est un nouveau meilleur score et l'enregistre
     * @return {int} la position du nouveau meilleur score, -1 si ce n'est pas un meilleur score
     */
    static public int SaveScore(String name, float score){
        if (bestScores == null) LoadBestScores();
        //1 - Vérification si le score est un nouveau meilleur score
        int index = 0; //On place l'index du nouveau score au meilleur 
        for(index = 0; index < bestScores.Count; index++){
            if(bestScores[index].score < score) break;
        }
        //2 - Si oui alors on l'enregistre et on update les meilleurs 
        if(index < maxScoreStores){
            bestScores.Insert(index, new ScoreElement(name, score));
            if(bestScores.Count > maxScoreStores) bestScores.RemoveAt(bestScores.Count - 1);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + fileStore);
            bf.Serialize(file, bestScores);
            file.Close();
            return index;
        }
        return -1;
        
    }

    static public void DeleteAllScores(){
        if (File.Exists(Application.persistentDataPath + fileStore))
        {
            File.Delete(Application.persistentDataPath + fileStore);
        }
    }

    static protected void LoadBestScores(){
        if (File.Exists(Application.persistentDataPath + fileStore)){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileStore, FileMode.Open);
            bestScores = (List<ScoreElement>) bf.Deserialize(file);
            file.Close();
        }else{
            bestScores = new List<ScoreElement>();
        }
    }
}
