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
        //Charger Bots from level manager
    }

    // Update is called once per frame
    void Update()
    {
        if(canAppear){
            GameObject s = bots[Random.Range(0,bots.Count)];
            if(!s.activeSelf){
                s.transform.GetChild(0).gameObject.transform.position = new Vector3(spawnners[0].position.x, spawnners[0].position.y, spawnners[0].position.z);
                s.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity  = new Vector2(0,0);
                s.SetActive(true);
                s.transform.GetChild(0).GetComponent<EntitySpaceShipDemoBehavior>().initialize();
                StartCoroutine("TimerEnemy");
            }
        }
    }

    IEnumerator TimerEnemy(){
        canAppear = false;
        yield return new WaitForSeconds(timeAppear);
        canAppear = true;
    }
}
