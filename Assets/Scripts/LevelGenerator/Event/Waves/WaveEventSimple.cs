using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEventSimple : Event
{
    public List<GameObject> listEnemies { get; set; }
    public int difficulty { get; set; }
    public EnemyList enemyList { get; set; }
    public float score { get; set; }

    public float time;
    protected float currWait;
    protected List<GameObject> listRandom;
    protected List<GameObject> listCross;
    protected List<Vector2> listVector2;
    public const int nbEnemyRandom = 4;
    public const int nbEnemyCross = 2;

    public float frequence = 1;
    private float spawn;

    protected override void BeginEvent()
    {
        listVector2 = new List<Vector2>();
        listRandom = new List<GameObject>();
        listCross = new List<GameObject>();
        currWait = 0;
        score = 0;
        spawn = 0;

    initializeListEnemies();
        //positionBoss();
    }

    protected override void UpdateEvent()
    {
        if (listRandom.Count == 0 && listCross.Count == 0)
        {
            this.End();
        }
        else
        {
            if(spawn >= frequence)
            {
                spawnEnemy();
                spawn = 0;
            }

            currWait += Time.deltaTime;
            spawn += Time.deltaTime;
            if (currWait >= time) this.End();
        }
    }

    //Initialisation des listes d'ennemies et de la difficulté de l'event
    public void initializeListEnemies()
    {
        difficulty = App.GetDifficulty();
        enemyList = App.GetEnemyList();
        //enemyList = GameObject.Instantiate(enemyList).GetComponent<EnemyList>();
        enemyList.DifficultLevel = difficulty;

        listEnemies = enemyList.CallListEnemies();
        for (int j = 0; j < listEnemies.Count; j++)
        {
            GameObject g = listEnemies[j];
            if (g.GetComponentInChildren<EntitySpaceShipBehavior>().getType().Equals("BotRandom"))
            {
                for (int i = 0; i < nbEnemyRandom; i++)
                {
                    listRandom.Add(g);
                }
            }
            if (g.GetComponentInChildren<EntitySpaceShipBehavior>().getType().Equals("BotCross"))
            {
                for (int i = 0; i < nbEnemyCross; i++)
                {
                    listCross.Add(g);
                }
            }
        }

        listVector2.Add(new Vector2(0, EnnemiesBorder.size.y/2));
        listVector2.Add(new Vector2(2, EnnemiesBorder.size.y / 2));
        listVector2.Add(new Vector2(-2, EnnemiesBorder.size.y / 2));
        listVector2.Add(new Vector2(0, EnnemiesBorder.size.y / 2 - 2));
        listVector2.Add(new Vector2(EnnemiesBorder.size.x/2, EnnemiesBorder.size.y / 2 - 2));
        listVector2.Add(new Vector2(-EnnemiesBorder.size.x/2, EnnemiesBorder.size.y / 2 - 2));
    }

    //Méthode de spawn d'ennemies
    public void spawnEnemy()
    {
        Instantiate(listRandom[0], listVector2[0], Quaternion.identity);
        listRandom.RemoveAt(0);
        listVector2.RemoveAt(0);
    }

    //Calcul du score de l'event
    public void scoreCalcul()
    {
        score = 0;
        for (int i = 0; i < listRandom.Count; i++)
        {
            score += listRandom[i].GetComponentInChildren<EntitySpaceShipBehavior>().getScore();
        }
    }

    public override float GetScore()
    {
        scoreCalcul();
        return score;
    }
}
