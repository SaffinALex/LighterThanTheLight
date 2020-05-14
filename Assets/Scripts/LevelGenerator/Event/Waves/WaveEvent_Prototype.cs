using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEvent_Prototype : Event
{
    [SerializeField] protected float maxTimeWave = 0f;
    //Représente l'apparition d'un ennemi
    [System.Serializable]
    public struct BlockWaveElement {
        public float timeAppear;
        public BlockWave block;
    }

    [SerializeField] protected List<BlockWaveElement> waveBlocksElements;
    protected float currentTime;
    protected List<BlockWave> allBlockWave;

    //Fonction qui sera appelé lorsque l'event débute, permet l'initialisation
    protected override void BeginEvent(){
        Debug.Log("SCORE : " + GetScore());
        App.GetEnemyList().ListEnemy.GetType();

        currentTime = 0;
        allBlockWave = new List<BlockWave>();
        for(int i = 0; i < waveBlocksElements.Count; i++){
            BlockWave e = Instantiate(waveBlocksElements[i].block, new Vector3(), Quaternion.identity);
            allBlockWave.Add(e);
            e.gameObject.SetActive(false);
        }
    }

    //Fonction qui sera appelé lorsque l'event fonctionne
    protected override void UpdateEvent(){
        currentTime += Time.deltaTime;

        bool allDead = true;

        for (int i = 0; i < allBlockWave.Count; i++) {
            if (allBlockWave[i] != null) {
                allDead = false;
                if(waveBlocksElements[i].timeAppear < currentTime && !allBlockWave[i].gameObject.activeSelf){
                        allBlockWave[i].gameObject.SetActive(true);
                }
            }
        }

        //Tous les ennemis sont mort on s'arrête
        if(allDead) End();

        if(currentTime > maxTimeWave && maxTimeWave > 0){
            for (int i = 0; i < allBlockWave.Count; i++) {
                if (allBlockWave[i] != null) {
                    allBlockWave[i].GoAway();
                }
            }
        }
    }

    // Permet de récupérer le score
    public override float GetScore(){
        float score = 0f;
        for(int i = 0; i < waveBlocksElements.Count; i++){
            score += waveBlocksElements[i].block.GetScore();
        }
        return score;
    }
}
