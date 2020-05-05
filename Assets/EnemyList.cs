using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<ListEnemyBehavior> list = new List<ListEnemyBehavior>();
    private int difficulLevel;
    private List<GameObjectBotBehaviour> listEnemy = new List<GameObjectBotBehaviour>();

    public int DifficulLevel { get => difficulLevel; set => difficulLevel = value; }
    public List<GameObjectBotBehaviour> ListEnemy { get => listEnemy; set => listEnemy = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CallList();
    }

    public void CallList()
    {
        switch (DifficulLevel)
        {
            case 0:
                ListEnemy = list[0].listEnemy;
                break;
            case 1:
                ListEnemy = list[1].listEnemy;
                break;
            case 2:
                ListEnemy = list[2].listEnemy;
                break;
        };
    }
}
