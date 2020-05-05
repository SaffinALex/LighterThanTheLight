using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Start is called before the first frame update

    //armes.size <= nbrMaxArmes
    public int nbrMaxWeapons;
    public int nbrMaxUpgradeShip;
    public double life;
    public float speed;
    public int maxShieldLife;
    public List<GameObject> weapons;
    public List<UpgradeShip> upgradeShip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getNbrMaxUpgradeShip(){
        return nbrMaxUpgradeShip;
    }
    public int getNbrMaxWeapons(){
        return nbrMaxWeapons;
    }

    public void setMaxShieldLife(int n){
        this.maxShieldLife = n;
    }
    public int getMaxShieldLife(){
        return maxShieldLife;
    }

    public void setNbrMaxUpgradeShip(int n){
        this.nbrMaxUpgradeShip = n;
    }
    public void setNbrMaxWeapons(int n){
        this.nbrMaxWeapons = n;
    }
    public void addWeapons(GameObject w){
        weapons.Add(w);
    }
    public void addUpgradeShip(UpgradeShip u){
        upgradeShip.Add(u);
    }

}
