using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEvent : Event
{

    public List<GameObject> listEnemies { get; set; }
    public int difficulty { get; set; }
    public EnemyList enemyList { get; set; }
    public float score { get; set; }

    public float wait;
    protected float currWait;
    protected float currWait2;
    protected float currWait3;
    public int nbPause;
    public int nbEnemies;
    protected List<GameObject> list;
    protected List<Vector3> listVector3;

    public float pause = 1.0f;
    protected float spawn;
    protected float timeP;
    protected bool b;

    protected override void BeginEvent()
    {
        listVector3 = new List<Vector3>();
        list = new List<GameObject>();
        b = false;
        currWait = wait;
        currWait2 = 0;
        currWait3 = 0;
        listVector3 = App.GetSpawn();
        timeP = (wait - nbPause) / (nbPause + 1);
        spawn = (wait - pause*nbPause - 1) / nbEnemies;
        score = 0;

        initializeListEnemies();
        //positionBoss();
    }

    protected override void UpdateEvent()
    {

        if (list.Count == 0)
        {
            this.End();
        }
        else
        {
            //Segmentation de l'event
            if (currWait2 >= timeP)
            {
                Debug.Log("Début Pause a : " + currWait2);
                //spawnEnemy();
                b = true;
                currWait2 = 0;
            }

            //Pause
            if (b && currWait2 >= pause)
            {
                Debug.Log("Fin Pause au bout de : " + currWait2);
                b = false;
                currWait2 = 0;
            }
            if (currWait3 >= spawn && b == false)
            {
                spawnEnemy();
                currWait3 = 0;
            }
            currWait -= Time.deltaTime;
            currWait2 += Time.deltaTime;
            currWait3 += Time.deltaTime;
            if (currWait <= 0) this.End();
        }
    }


    //Initialisation des listes d'ennemies et de la difficulté de l'event
    public void initializeListEnemies()
    {
        difficulty = App.GetDifficulty();
        enemyList = App.GetEnemyList();
        enemyList = GameObject.Instantiate(enemyList).GetComponent<EnemyList>();
        enemyList.DifficultLevel = difficulty;

        int nb1 = 60 * nbEnemies / 100;
        listEnemies = enemyList.CallListEnemies();
        for (int i = 0; i < nb1; i++)
        {
            GameObject g = listEnemies[Random.Range(0, listEnemies.Count)];
            list.Add(g);
        }

        enemyList.DifficultLevel = difficulty + 1;
        listEnemies = enemyList.CallListEnemies();
        int nb2 = nbEnemies - nb1;
        for (int i = 0; i < nb2; i++)
        {
            GameObject g = listEnemies[Random.Range(0, listEnemies.Count)];
            list.Add(g);
        }
    }


    //Méthode de spawn d'ennemies
    public void spawnEnemy()
    {
        Instantiate(list[0], listVector3[Random.Range(0, listVector3.Count)], Quaternion.identity);
        list.RemoveAt(0);

        /*GameObject g = Instantiate(list[Random.Range(0, list.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        list.Remove(g);*/
    }


    //Calcul du score de l'event
    public void scoreCalcul()
    {
        score = 0;
        for (int i = 0; i < list.Count; i++)
        {
            score += list[i].GetComponentInChildren<EntitySpaceShipBehavior>().getScore();
        }
    }

    override
    public float GetScore()
    {
        scoreCalcul();
        return score;
    }
}
