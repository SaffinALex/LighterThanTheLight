using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeDash : Upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<SpriteRenderer>())
            GetComponent<SpriteRenderer>().sprite = icone;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//A l'obtention de l'Upgrade
    public abstract void StartUpgrade(Dash d);
//A chaque update mettre à jour les caractéristiques.
    public void EndUpgrade(Dash d){
        Destroy(this);
    }

}
