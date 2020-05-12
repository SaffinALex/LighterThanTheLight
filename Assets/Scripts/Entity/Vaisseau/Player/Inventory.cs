using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Ship player;
    public int money;
    public int score;
    public List<WeaponPlayer> Weapons;
    public List<UpgradeDash> UpgradeDashes;
    public List<UpgradeShip> UpgradeShip;
    public List<UpgradeWeapon> UpgradeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addUpgradeInventory(UpgradeDash d){
        UpgradeDashes.Add(d);
    }
    public void addUpgradeInventory(UpgradeWeapon d){
        UpgradeWeapon.Add(d);
    }
    public void addUpgradeInventory(UpgradeShip d){
        UpgradeShip.Add(d);
    }

    public void addWeaponInventory(WeaponPlayer d){
        Weapons.Add(d);
    }

    public void equipShipUpgrade(UpgradeShip u){
        int token = player.numberUpgradeCanAdd();
        if(token >= u.getWeight()){
            player.addUpgradeShip(u);
        }
        else{
            //Impossible d'equiper 
        }
    }

    public void equipWeaponUpgrade(WeaponPlayer w, UpgradeWeapon u){
        //On regarde combien de slot disponible sur l'arme
        int token = w.numberUpgradeCanAdd();
        //Si assez de place on met l'arme sinon impossible
        if(token >= u.getWeight()){
            w.addUpgradeWeapon(u);
        }
        else{
            //Impossible d'equiper l'arme
        }
    }

    public void equipDashUpgrade(Dash d, UpgradeDash u){
        int token = d.numberUpgradeCanAdd();
        if(token >= u.getWeight()){
            d.addUpgradeDashes(u);
        }
        else{
            //Impossible d'equiper sur le dash
        }
    }

    public void equipWeapon(WeaponPlayer w){
        int token = player.numberWeaponCanAdd();
        if(token >= w.getWeight()){
            player.addWeapons(w);
        }
        else{
            //Impossible d'equiper 
        }
    }

    //public void deleteUpgrade(Upgrade upgrade, )

    public int getMoney(){
        return money;
    }
    public void setMoney(int m){
        money = m;
    }

    public int getScore(){
        return score;
    }
    public void setScore(int m){
        score = m;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
