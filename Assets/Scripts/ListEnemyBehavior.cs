using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEnemyBehavior : MonoBehaviour
{
    public List<GameObject> listEnemy = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Place le Boss dans la liste en dernière position
    public void moveBoss()
    {
        GameObject g = null;
        for (int i = 0; i < listEnemy.Count; i++)
        {
            if (listEnemy[i].GetComponentInChildren<EntitySpaceShipBehavior>().getType().Equals("Boss"))
            {
                g = listEnemy[i];
                listEnemy.RemoveAt(i);
            }
        }
        listEnemy.Add(g);
    }
}
