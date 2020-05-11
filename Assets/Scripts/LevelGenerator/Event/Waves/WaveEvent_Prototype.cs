using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEvent_Prototype : Event
{
    //Représente l'apparition d'un ennemi
    [System.Serializable]
    public struct EnemyElement {
        public float timeAppear;
        public EnemySpaceShip enemy;
        public Vector2 positionAppear;
        public bool relativeLeftCorner;
        public bool relativeRightCorner;
        public float angleAppear;

        private GameObject prefab;
        private bool instancied;
    }

    [SerializeField] protected List<EnemyElement> enemiesWave;
    protected float currentTime;
    protected List<EnemySpaceShip> allEnemy;

    //Fonction qui sera appelé lorsque l'event débute, permet l'initialisation
    protected override void BeginEvent(){
        currentTime = 0;
        allEnemy = new List<EnemySpaceShip>();
        for(int i = 0; i < enemiesWave.Count; i++){
            Vector2 positionEnemy = new Vector2(enemiesWave[i].positionAppear.x + (enemiesWave[i].relativeLeftCorner ? - EnnemiesBorder.size.x / 2 : 0) + (enemiesWave[i].relativeRightCorner ? EnnemiesBorder.size.x / 2 : 0), EnnemiesBorder.size.y / 2 + enemiesWave[i].positionAppear.y);
            EnemySpaceShip e = Instantiate(enemiesWave[i].enemy, positionEnemy, Quaternion.AngleAxis(enemiesWave[i].angleAppear, Vector3.forward));
            allEnemy.Add(e);
            e.gameObject.SetActive(false);
        }
    }

    //Fonction qui sera appelé lorsque l'event fonctionne
    protected override void UpdateEvent(){
        currentTime += Time.deltaTime;

        bool allDead = true;

        for (int i = 0; i < allEnemy.Count; i++) {
            if (allEnemy[i] != null) {
                allDead = false;
                if(enemiesWave[i].timeAppear < currentTime && !allEnemy[i].gameObject.activeSelf){
                        allEnemy[i].gameObject.SetActive(true);
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
