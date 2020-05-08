using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEvent : Event
{

    public float wait;
    protected float currWait;
    protected float currWait2;
    protected float currWait3;
    public int nbSpawn;
    public int nbPause;
    protected float factorDiff;
    public int nbEnemies;
    protected List<GameObject> list;
    private List<Vector3> listVector3;

    public float pause = 1.0f;
    private float spawn;
    private float timeP;
    private bool b;

    protected override void BeginEvent()
    {
        listVector3 = new List<Vector3>();
        list = new List<GameObject>();
        b = false;
        currWait = wait;
        currWait2 = 0;
        currWait3 = 0;
        factorDiff = 0;
        List<Vector3> vect3 = App.GetSpawn();
        for(int i=0; i<nbSpawn; i++)
        {
            listVector3.Add(vect3[Random.Range(0, vect3.Count)]);
        }
        timeP = (wait - nbPause) / (nbPause + 1);
        spawn = (wait - 2) / nbEnemies;

        initializeListEnemies();
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
        Difficulty = App.GetDifficulty();
        EnemyList = App.GetEnemyList();
        EnemyList = GameObject.Instantiate(EnemyList).GetComponent<EnemyList>();
        EnemyList.DifficultLevel = Difficulty;

        int nb1 = 60 * nbEnemies / 100;
        ListEnemies = EnemyList.CallList();
        for (int i = 0; i < nb1; i++)
        {
            GameObject g = ListEnemies[Random.Range(0, ListEnemies.Count)];
            list.Add(g);
        }

        EnemyList.DifficultLevel = Difficulty + 1;
        ListEnemies = EnemyList.CallList();
        int nb2 = nbEnemies - nb1;
        for (int i = 0; i < nb2; i++)
        {
            GameObject g = ListEnemies[Random.Range(0, ListEnemies.Count)];
            list.Add(g);
        }
    }


    //Méthode de spawn d'ennemies
    public void spawnEnemy()
    {
        GameObject g = Instantiate(list[0], listVector3[Random.Range(0, listVector3.Count)], Quaternion.identity);
        list.RemoveAt(0);

        /*GameObject g = Instantiate(list[Random.Range(0, list.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        list.Remove(g);*/
    }


    //Calcul du score de l'event
    public void scoreCalcul()
    {
        for (int i = 0; i < list.Count; i++)
        {
            factorDiff += (0.1f + 0.1f * list[i].GetComponentInChildren<EntitySpaceShipBehavior>().Difficult);
        }
        factorDiff += 10f / wait;
        Score = Difficulty + nbSpawn + nbPause + factorDiff;
    }
}
