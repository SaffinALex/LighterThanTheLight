using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent_Prototype : Event
{
    [SerializeField] protected float maxTimeWave = 0f;
    [SerializeField] protected bool callGoAway = false;
    //Représente l'apparition d'un ennemi
    [System.Serializable]
    public struct BlockBossElement
    {
        public float timeAppear;
        public BlockBoss block;
    }
    [System.Serializable]
    public struct BlockWaveElement
    {
        public float timeAppear;
        public BlockWave block;
    }

    [SerializeField] protected BlockBossElement bossBlocksElement;
    protected float currentTime;
    protected BlockBoss blockBoss;

    [SerializeField] protected List<BlockWaveElement> waveBlocksElements;
    protected List<BlockWave> allBlockWave;

    //Fonction qui sera appelé lorsque l'event débute, permet l'initialisation
    protected override void BeginEvent()
    {
        LevelUIEventManager.GetLevelUI().TriggerWarning("Boss en Approche !", 5);

        Debug.Log("SCORE : " + GetScore());
        App.GetEnemyList().ListEnemy.GetType();

        currentTime = 0;
        allBlockWave = new List<BlockWave>();

        blockBoss = Instantiate(bossBlocksElement.block, new Vector3(), Quaternion.identity);
        blockBoss.gameObject.SetActive(false);

        for (int i = 0; i < waveBlocksElements.Count; i++)
        {
            BlockWave w = Instantiate(waveBlocksElements[i].block, new Vector3(), Quaternion.identity);
            allBlockWave.Add(w);
            w.gameObject.SetActive(false);
        }
    }

    //Fonction qui sera appelé lorsque l'event fonctionne
    protected override void UpdateEvent()
    {
        currentTime += Time.deltaTime;

        //LevelUIEventManager.GetLevelUI().TriggerWarning("Boss en Approche !", 5);

        bool allDead = true;

        if (blockBoss != null)
        {
            allDead = false;
            if (bossBlocksElement.timeAppear < currentTime && !blockBoss.gameObject.activeSelf)
            {
                blockBoss.gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < allBlockWave.Count; i++)
        {
            if (allBlockWave[i] != null)
            {
                allDead = false;
                if (waveBlocksElements[i].timeAppear < currentTime && !allBlockWave[i].gameObject.activeSelf)
                {
                    allBlockWave[i].gameObject.SetActive(true);
                }
            }
        }

        //Tous les ennemis sont mort on s'arrête
        if (allDead) End();

        if (currentTime > maxTimeWave && maxTimeWave > 0 && callGoAway)
        {
            if (blockBoss != null)
            {
                blockBoss.GoAway();
            }

            for (int i = 0; i < allBlockWave.Count; i++)
            {
                if (allBlockWave[i] != null)
                {
                    allBlockWave[i].GoAway();
                }
            }
        }
    }

    // Permet de récupérer le score
    public override float GetScore()
    {
        float score = 0f;
        score += bossBlocksElement.block.GetScore();

        for (int i = 0; i < waveBlocksElements.Count; i++)
        {
            score += waveBlocksElements[i].block.GetScore();
        }

        return score;
    }
}
