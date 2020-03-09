using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredForEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Enemy")){
            
            col.gameObject.GetComponent<EntitySpaceShipBehavior>().initialize();
            col.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
