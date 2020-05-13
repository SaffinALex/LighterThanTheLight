using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWave : MonoBehaviour
{
    public enum PositionSpawner { Left, Right, TopLeft, TopRight, TopCenter, PlayerX }
    public enum EnemyType { BotBasic, BotCircle, BotFollow, BotRandom, BotThrow }

    [System.Serializable]
    public struct EnemyElement {
        public float timeAppear;
        public EnemyType enemyType;
        public int enemyDifficulty;
        public Vector2 positionAppear;
        public PositionSpawner spawnPosition;
        public float angleAppear;

        private GameObject prefab;
        private bool instancied;
    }

    [SerializeField] protected List<EnemyElement> enemiesWave;
    protected float currentTime = 0;
    protected List<EntitySpaceShipBehavior> allEnemy;
    protected bool ended = false;

    void Start(){
        ended = false;
        currentTime = 0;
        allEnemy = new List<EntitySpaceShipBehavior>();
        for (int i = 0; i < enemiesWave.Count; i++) {
            Vector2 positionEnemy = new Vector2(enemiesWave[i].positionAppear.x, enemiesWave[i].positionAppear.y);

            if (enemiesWave[i].spawnPosition == PositionSpawner.Left) positionEnemy += new Vector2(-EnnemiesBorder.size.x / 2, 0);
            else if (enemiesWave[i].spawnPosition == PositionSpawner.Right) positionEnemy += new Vector2(EnnemiesBorder.size.x / 2, 0);
            else if (enemiesWave[i].spawnPosition == PositionSpawner.TopLeft) positionEnemy += new Vector2(- EnnemiesBorder.size.x / 2, EnnemiesBorder.size.y / 2);
            else if (enemiesWave[i].spawnPosition == PositionSpawner.TopRight) positionEnemy += new Vector2(EnnemiesBorder.size.x / 2, EnnemiesBorder.size.y / 2);
            else if (enemiesWave[i].spawnPosition == PositionSpawner.TopCenter) positionEnemy += new Vector2(0, EnnemiesBorder.size.y / 2);
            else if (enemiesWave[i].spawnPosition == PositionSpawner.PlayerX) positionEnemy += new Vector2(0, EnnemiesBorder.size.y / 2);

            GameObject e = Instantiate(App.GetEnemyList().getEnemy(enemiesWave[i].enemyType.ToString("g"), enemiesWave[i].enemyDifficulty), positionEnemy, Quaternion.AngleAxis(enemiesWave[i].angleAppear, Vector3.forward));
            allEnemy.Add(e.GetComponentInChildren<EntitySpaceShipBehavior>());
            e.SetActive(false);
        }
    }

    void Update(){
        currentTime += Time.deltaTime;
        bool allDead = true;
        for (int i = 0; i < allEnemy.Count; i++) {
            if (allEnemy[i] != null) {
                allDead = false;
                if (enemiesWave[i].timeAppear < currentTime && !allEnemy[i].gameObject.activeInHierarchy) {
                    allEnemy[i].gameObject.transform.parent.gameObject.SetActive(true);
                }
            }
        }

        //Tous les ennemis sont mort on s'arrête
        if (allDead) Destroy(gameObject);
    }

    //Fonction qui sera appelé lorsque l'event fonctionne
    public float GetScore() {
        float score = 0f;
        for(int i = 0; i < enemiesWave.Count; i++){
            score += App.GetEnemyList().getEnemy(enemiesWave[i].enemyType.ToString("g"), enemiesWave[i].enemyDifficulty).GetComponentInChildren<EntitySpaceShipBehavior>().getScore();
        }
        return score;
    }
}
