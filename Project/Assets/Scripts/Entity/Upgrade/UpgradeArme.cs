using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArme : Upgrade
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
    void StartUpgrade(Arme arme){

    }
//A chaque update mettre à jour les caractéristiques.
    void UpdateUpgrade(Arme arme){
        
    }
//Avant la suppression de l'Upgrade.
    void EndUpgrade(Arme arme){
        
        Destroy(this);
    }
}
