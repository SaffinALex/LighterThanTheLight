using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> bots;
    public List<Transform> spawnners;
    public float timeAppear;
    private bool canAppear;
    void Start()
    {
        canAppear = true;
        GetComponent<LevelControler>().LoadBot();
    }

    // Update is called once per frame
    void Update()
    {
        if(canAppear){
            GameObject s = bots[Random.Range(0,bots.Count)];
            if(!s.activeSelf){
                s.transform.gameObject.transform.position = new Vector3(0,0,0);
                s.transform.GetChild(0).gameObject.transform.position = new Vector3(spawnners[0].position.x, spawnners[0].position.y, spawnners[0].position.z);
                s.SetActive(true);
                s.transform.GetChild(0).GetComponent<EntitySpaceShipBehavior>().initialize(/*new Weapon(),etc... */);
                StartCoroutine("TimerEnemy");
            }
        }
    }

    IEnumerator TimerEnemy(){
        canAppear = false;
        yield return new WaitForSeconds(timeAppear);
        canAppear = true;
    }


    public void addBot(GameObject o){
        bots.Add(o);
    }
}
