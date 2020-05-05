using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShip : Upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = icone;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//A l'obtention de l'Upgrade
    public void StartUpgrade(PlayerShip v){

    }
//Avant la suppression de l'Upgrade.
    void EndUpgrade(PlayerShip v){
        
        Destroy(this);
    }
}
