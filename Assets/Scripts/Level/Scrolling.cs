using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int time;
    public GameObject level;
    private float timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < time){
            transform.position = new Vector3(transform.position.x, transform.position.y-speed*Time.deltaTime, transform.position.z);
            timer += Time.deltaTime;
        }
        if(timer > time - 5 && level.GetComponent<EnemyManager>().enabled){
            //Lancer l'alerte Boss
            level.GetComponent<EnemyManager>().enabled = false;
        }
    }
}
