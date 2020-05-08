using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : WaveEvent
{
    public GameObject boss;
    public float timer;
    public float t;

    protected override void BeginEvent()
    {
        base.BeginEvent();
        Debug.Log(boss);
        initializeListEnemies();
        timer = 1;
        t = 0;
    }

    protected override void UpdateEvent()
    {
        if (list.Count == 0)
        {
            goBoss();
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
            if (currWait <= 0)
            {
                LevelUIEventManager.GetLevelUI().TriggerWarning("Attention Boss en approche !");
                goBoss();
            }
        }
    }


    //Initialisation des listes d'ennemies et de la difficulté de l'event
    
    new public void initializeListEnemies()
    {
        boss = ListEnemies[ListEnemies.Count - 1];
        Debug.Log(boss);
    }


    //Spawn du Boss
    public void goBoss()
    {
        Debug.Log("C'est l'heure du BOSS");
        Instantiate(boss, listVector3[Random.Range(0, listVector3.Count)], Quaternion.identity);

        if (t >= timer)
            this.End();
        else
            t += Time.deltaTime;
    }
}
