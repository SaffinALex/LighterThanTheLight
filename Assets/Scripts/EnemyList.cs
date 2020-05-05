using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<ListEnemyBehavior> list = new List<ListEnemyBehavior>();
    private int difficultLevel;
    private List<GameObject> listEnemy = new List<GameObject>();

    public int DifficultLevel { get => difficultLevel; set => difficultLevel = value; }
    public List<GameObject> ListEnemy { get => listEnemy; set => listEnemy = value; }

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
        switch (DifficultLevel)
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
