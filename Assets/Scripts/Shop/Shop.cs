using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] protected List<ItemWeapon> itemsWeapons; //Liste des armes du joueur
    [SerializeField] protected List<ItemUpgrade> itemsUpgrades; //Liste des upgrades du joueur

    protected bool open = false;
    protected bool available = false;
    
    void Start() {
        
    }
    
    /**
     * Permet de savoir si tous les items du shop ont été achetés
     * @returns {bool} true si vrai, false sinon
     */
    protected bool AllItemsBuyed(){
        foreach (ItemWeapon item in itemsWeapons) {
            if(item.IsAvailable()) return false;
        }
        foreach (ItemUpgrade item in itemsUpgrades) {
            if (item.IsAvailable()) return false;
        }
        return true;
    }

}
