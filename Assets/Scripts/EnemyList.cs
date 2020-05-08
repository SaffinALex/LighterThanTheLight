using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<ListEnemyBehavior> list = new List<ListEnemyBehavior>();
    private float difficultLevel;
    private List<GameObject> listEnemy = new List<GameObject>();

    public float DifficultLevel { get => difficultLevel; set => difficultLevel = value; }
    public List<GameObject> ListEnemy { get => listEnemy; set => listEnemy = value; }

    // Start is called before the first frame update
    void Start()
    {
        list[0].moveBoss();
    }

    // Update is called once per frame
    void Update()
    {
        CallList();
    }

    public List<GameObject> CallList()
    {
        switch (DifficultLevel)
        {
            case 0:
                ListEnemy = list[0].listEnemy;
                for(int i = 0; i<ListEnemy.Count; i++)
                {
                    ListEnemy[i].GetComponentInChildren<EntitySpaceShipBehavior>().Difficult = 0;
                }
                break;
            case 1:
                ListEnemy = list[1].listEnemy;
                for (int i = 0; i < ListEnemy.Count; i++)
                {
                    ListEnemy[i].GetComponentInChildren<EntitySpaceShipBehavior>().Difficult = 1;
                }
                break;
            case 2:
                ListEnemy = list[2].listEnemy;
                for (int i = 0; i < ListEnemy.Count; i++)
                {
                    ListEnemy[i].GetComponentInChildren<EntitySpaceShipBehavior>().Difficult = 2;
                }
                break;
            case 3:
                ListEnemy = list[3].listEnemy;
                for (int i = 0; i < ListEnemy.Count; i++)
                {
                    ListEnemy[i].GetComponentInChildren<EntitySpaceShipBehavior>().Difficult = 3;
                }
                break;
            case 4:
                ListEnemy = list[4].listEnemy;
                for (int i = 0; i < ListEnemy.Count; i++)
                {
                    ListEnemy[i].GetComponentInChildren<EntitySpaceShipBehavior>().Difficult = 4;
                }
                break;
        };
        return ListEnemy;
    }
}
