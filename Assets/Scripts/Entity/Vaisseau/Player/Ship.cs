using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Start is called before the first frame update

    //armes.size <= nbrMaxArmes
    public int nbrMaxWeapons;
    public int nbrMaxUpgradeShip;
    public double initialLife;
    protected double life;
    public float speed;
    public int maxShieldLife;
    public List<WeaponPlayer> weapons;
    public List<UpgradeShip> upgradeShip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int numberUpgradeCanAdd(){
        int cpt = 0;
        for(int i = 0; i<upgradeShip.Count; i++){
            cpt+= upgradeShip[i].getWeight();
        }
        return nbrMaxUpgradeShip - cpt;
    }

    public int numberWeaponCanAdd(){
        int cpt = 0;
        for(int i = 0; i<weapons.Count; i++){
            cpt+= weapons[i].gameObject.GetComponent<WeaponPlayer>().getWeight();
        }
        return nbrMaxWeapons - cpt;
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

    public double getLife(){
        return life;
    }
    public void setLife(double s){
        life = s;
    }

    public void setNbrMaxUpgradeShip(int n){
        this.nbrMaxUpgradeShip = n;
    }
    public void setNbrMaxWeapons(int n){
        this.nbrMaxWeapons = n;
    }
    public void addWeapons(WeaponPlayer w){
        weapons.Add(w);
    }
    public void addUpgradeShip(UpgradeShip u){
        upgradeShip.Add(u);
    }

}
