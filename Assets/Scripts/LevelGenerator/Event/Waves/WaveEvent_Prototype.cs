using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEvent_Prototype : Event
{
    //Représente l'apparition d'un ennemi
    [System.Serializable]
    public struct BlockWaveElement {
        public float timeAppear;
        public BlockWave block;
    }

    [SerializeField] protected List<BlockWaveElement> enemiesWave;
    protected float currentTime;
    protected List<BlockWave> allBlockWave;

    //Fonction qui sera appelé lorsque l'event débute, permet l'initialisation
    protected override void BeginEvent(){

        App.GetEnemyList().ListEnemy.GetType();

        currentTime = 0;
        allBlockWave = new List<BlockWave>();
        for(int i = 0; i < enemiesWave.Count; i++){
            BlockWave e = Instantiate(enemiesWave[i].block, new Vector3(), Quaternion.identity);
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
                if(enemiesWave[i].timeAppear < currentTime && !allBlockWave[i].gameObject.activeSelf){
                        allBlockWave[i].gameObject.SetActive(true);
                }
            }
        }

        //Tous les ennemis sont mort on s'arrête
        if(allDead) End();
    }

    //Fonction qui sera appelé lorsque l'event fonctionne
    public override float GetScore(){
        return 0;
    }
}
