using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControler : MonoBehaviour
{
    public int numberBot;
    public List<GameObject> bots;
    public GameObject w;
    public GameObject control;
    // Start is called before the first frame update
    public void Start(){
       // Instantiate(control, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void LoadBot(){
        for(int i = 0; i<numberBot; i++){
            GameObject s = Instantiate(bots[Random.Range(0,bots.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            s.transform.GetChild(0).tag="Enemy"; 
            s.SetActive(false);
            s.transform.GetChild(0).GetComponent<EntitySpaceShipBehavior>().weapon = w;
         //  Debug.Log("o");
            GetComponent<EnemyManager>().addBot(s);
        }
    }


    public void addBot(GameObject o){
        bots.Add(o);
    }
}
