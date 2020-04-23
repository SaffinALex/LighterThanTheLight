using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOnde : Upgrade
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
    void StartUpgrade(Onde onde){

    }
//A chaque update mettre à jour les caractéristiques.
    void UpdateUpgrade(Onde onde){
        
    }
//Avant la suppression de l'Upgrade.
    void EndUpgrade(Onde onde){
        
        Destroy(this);
    }
}
