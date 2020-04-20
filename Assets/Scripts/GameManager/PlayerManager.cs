using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    protected float money = 0f;
    protected int score = 0;
    
    protected int weaponInventoryMax = 2; //Max 2 armes
    protected List<WeaponPlayer> weapons;
    protected List<Upgrade> upgrades;

    public void addScore(int score){
        score = score < 0 ? 0 : score;
        this.score += score;
    }

    /**
     * Fonction permettant d'acheter un item
     * @params {Item} item l'item qu'on veut acheter
     * @returns {bool} Renvois vrai si l'item a été acheté
     */
    public bool buyItem(Item item){
        if(item.isAvailable() && item.getPrice() <= money){
            item.obtainItem();
            if(item is ItemWeapon){
                //Obtain Weapon
            }else if(item is ItemUpgrade){
                //Obtain Upgrade
            }else{
                //OMG FAKE ITEM
            }
            return true;
        }
        return false;
    }
}
