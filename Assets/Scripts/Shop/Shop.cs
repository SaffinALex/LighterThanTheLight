using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Shop
{
    [SerializeField] protected List<ItemWeapon> itemsWeapons; //Liste des armes du joueur
    [SerializeField] protected List<ItemUpgrade> itemsUpgrades; //Liste des upgrades du joueur

    protected bool open = false;
    protected bool available = false;
    //Evénement permettant de signaler la fin du travail d'un nodeElement
    protected UnityEvent eventCloseShop = new UnityEvent(); //Permet de signaler que le shop est fermé

    //Permet d'obtenir l'évènement du shop qui se ferme
    public UnityEvent GetEventCloseShop(){ return eventCloseShop; } 
    
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

    public void AddWeaponItem(ItemWeapon weaponItem){
        itemsWeapons.Add(weaponItem);
    }

    public void AddUpgradeItem(ItemUpgrade upgradeItem) {
        itemsUpgrades.Add(upgradeItem);
    }

    public void CloseShop(){
        eventCloseShop.Invoke();
    }
}
