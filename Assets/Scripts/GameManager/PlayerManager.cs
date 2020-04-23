using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager {
    protected float money = 0f; //L'argent actuelle du joueur
    protected int score = 0; //Score actuel du joueur
    
    protected int weaponInventoryMax = 2; //Le nombre maximum des armes du joueur
    protected List<WeaponPlayer> weapons; //Liste des armes du joueur
    protected List<Upgrade> upgrades; //Liste des upgrades du joueur
    protected Onde onde; //L'onde du player
    protected Dash dash; //Le dash du player

    /**
     * Permet d'ajouter des points au score courant
     * @params {int} score le score qu'on veut rajouter
     */
    public void addScore(int score){
        score = score < 0 ? 0 : score;
        this.score += score;
    }

    /**
     * Permet d'obtenir les armes du joueur
     * @returns {List<WeaponPlayer>}
     */
    public List<WeaponPlayer> GetWeapons(){
        return weapons;
    }

    /**
     * Permet d'obtenir les upgrades du joueur
     * @returns {List<Upgrade>}
     */
    public List<Upgrade> GetUpgrades(){
        return upgrades;
    }

    /**
     * Fonction permettant de s'avoir si on peut aceheter un item
     * @params {Item} item l'item qu'on veut acheter
     * @returns {bool} Renvois vrai si l'item peut être acheté
     */
    public bool CanBuyItem(Item item){
        return item.IsAvailable() && item.GetPrice() <= money;
    }

    /**
     * Fonction permettant d'update l'argent du joueur selon l'item 
     * @params {Item} item
     */
    public void BuyItem(Item item){
        money -= item.GetPrice();
    }

    /**
     * Permet d'acheter une arme
     * @params {ItemWeapon} item
     * @returns {bool} retourne true si l'arme a été acheté, false si echec
     */
    public bool BuyWeapon(ItemWeapon item){
        //1 - Check si on peut acheter l'item
        if(!CanBuyItem(item)) return false;
        //2 - Check si la place est disponible dans l'inventaire
        if(weapons.Count + 1 <= weaponInventoryMax ) return false;
        //3 - On achète l'item
        BuyItem(item);
        WeaponPlayer weaponPlayer = item.ObtainItem(); //On rend l'item invalide
        weapons.Add(weaponPlayer); //On ajoute l'item à la liste

        return true;
    }

    /**
     * Permet d'acheter une Upgrade
     * @params {ItemUpgrade} item
     * @returns {bool} retourne true si l'upgrade a été acheté, false si echec
     */
    public bool BuyUpgrade(ItemUpgrade item){
        //1 - Check si on peut acheter l'item
        if (!CanBuyItem(item)) return false;
        //3 - On achète l'item
        BuyItem(item);
        Upgrade weaponPlayer = item.ObtainItem(); //On rend l'item invalide
        upgrades.Add(weaponPlayer); //On ajoute l'item à la liste

        return true;
    }
}
