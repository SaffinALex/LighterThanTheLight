using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDash : Upgrade
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
    void StartUpgrade(Dash d){

    }
//A chaque update mettre à jour les caractéristiques.
    void UpdateUpgrade(Dash d){
        
    }
//Avant la suppression de l'Upgrade.
    void EndUpgrade(Dash d){
        
        Destroy(this);
    }

}
