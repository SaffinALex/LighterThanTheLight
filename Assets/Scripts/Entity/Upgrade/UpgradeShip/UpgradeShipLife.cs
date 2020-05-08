using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShipLife : UpgradeShip
{
   public int life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override
    public void StartUpgrade(PlayerShip v){
        v.setLife(life + v.getLife());

    }
//Avant la suppression de l'Upgrade.
    override
    public void EndUpgrade(PlayerShip v){
        Destroy(this);
    }
}
