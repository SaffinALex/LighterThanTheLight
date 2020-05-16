using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<ListEnemyBehavior> list = new List<ListEnemyBehavior>();
    private int difficultLevel;
    private List<GameObject> listEnemy = new List<GameObject>();
    private List<GameObject> listBoss = new List<GameObject>();

    public int DifficultLevel { get => difficultLevel; set => difficultLevel = value; }
    public List<GameObject> ListEnemy { get => listEnemy; set => listEnemy = value; }
    public List<GameObject> ListBoss { get => listBoss; set => listBoss = value; }

    // Start is called before the first frame update
    void Start()
    {
        DifficultLevel = App.GetDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        CallListEnemies();
        CallListBoss();
    }

    public List<GameObject> CallListEnemies()
    {
        ListEnemy = list[DifficultLevel].listEnemy;
        for (int i = 0; i<ListEnemy.Count; i++)
        {
            ListEnemy[i].GetComponentInChildren<EntitySpaceShipBehavior>().difficult = DifficultLevel;
        }
        return ListEnemy;
    }

    public GameObject getEnemy(string s, int difficulty)
    {
        DifficultLevel = App.GetDifficulty();
        difficulty = difficulty < 0 ? DifficultLevel : difficulty;
        ListEnemy = list[difficulty].listEnemy;
        GameObject enemy = null;
        for (int i = 0; i < ListEnemy.Count; i++) {
            if (ListEnemy[i].GetComponentInChildren<EntitySpaceShipBehavior>().getType().Equals(s)) {
                enemy = ListEnemy[i];
            }
        }
        return enemy;
    }

    public List<GameObject> CallListBoss()
    {
        ListBoss = list[DifficultLevel].listBoss;
        for (int i = 0; i < ListBoss.Count; i++)
        {
            ListBoss[i].GetComponentInChildren<EntitySpaceShipBehavior>().difficult = DifficultLevel;
        }
        return ListBoss;
    }
}
