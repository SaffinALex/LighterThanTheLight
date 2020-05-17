using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerManager {
    private Inventory inventory;

    protected float money; //L'argent Global du joueur
    protected int score; //Score Global du joueur

    protected PlayerShip player; // le joueur
    protected Onde onde; //L'onde du joueur
    protected Dash dash; //Le dash du joueur

    public PlayerManager(PlayerShip player)
    {
        inventory = new Inventory();

        money = 0;
        score = 0;

        this.player = player;
        this.onde = player.wave;
        this.dash = player.dash;

        List<UpgradeShip> bufferShipUpgrades = new List<UpgradeShip>(new UpgradeShip[player.nbrMaxUpgradeShip]);
        for (int i = 0; i < player.upgradeShip.Count; i++)
            bufferShipUpgrades[i] = player.upgradeShip[i];
        player.upgradeShip = bufferShipUpgrades;

        List<UpgradeOnde> bufferOndeUpgrades = new List<UpgradeOnde>(new UpgradeOnde[onde.nbrUpgradeMax]);
        for (int i = 0; i < onde.upgradeOndes.Count; i++)
            bufferOndeUpgrades[i] = onde.upgradeOndes[i];
        onde.upgradeOndes = bufferOndeUpgrades;

        List<UpgradeDash> bufferDashUpgrades = new List<UpgradeDash>(new UpgradeDash[dash.nbrUpgradeMax]);
        for (int i = 0; i < dash.upgradeDashes.Count; i++)
            bufferDashUpgrades[i] = dash.upgradeDashes[i];
        dash.upgradeDashes = bufferDashUpgrades;

        List<WeaponPlayer> bufferWeapons = new List<WeaponPlayer>(new WeaponPlayer[player.nbrMaxWeapons]);
        for (int i = 0; i < player.weapons.Count; i++)
        {
            bufferWeapons[i] = player.weapons[i];
        }
        player.weapons = bufferWeapons;

        int c = 0;
        for (int i = 0; i < player.weapons.Count; i++)
        {
            WeaponPlayer wp = player.weapons[i];
            if (wp != null)
            {
                c++;
                this.extendListSize(i);
            }
        }
        if (c == 0)
            setWeapon(0, App.baseWeapon);
    }

    public void extendListSize(int weaponIndex)
    {
        List<UpgradeWeapon> bufferWeapons = new List<UpgradeWeapon>(new UpgradeWeapon[player.weapons[weaponIndex].nbrMaxUpgrade]);
        for (int i = 0; i < player.weapons[weaponIndex].upgradeWeapons.Count; i++)
        {
            bufferWeapons[i] = player.weapons[weaponIndex].upgradeWeapons[i];
        }
        player.weapons[weaponIndex].upgradeWeapons = bufferWeapons;
    }

    /* ------------------------------------------
     *          Inventaire
     * ------------------------------------------
     */
    public Inventory getInventory() { return inventory; }
    private void flushItemInventory() { inventory.flushItemInventory(); }
    private List<WeaponPlayer> getWeaponInventory() { return inventory.getWeapons(); }
    public void endOfLevelRoutine()
    {
        this.addMoney(inventory.getMoney());
        inventory.setMoney(0);
        this.addScore(inventory.getScore());
        inventory.setScore(0);
    }

    /* ------------------------------------------
     *          Score et Monnaie
     * ------------------------------------------
     */
    /**
     * Permet d'accéder au score global du joueur
     */
    public int getScore() { return score; }

    /**
     * Permet d'ajouter des points au score global
     * @params {int} score le score qu'on veut rajouter
     */
    public void addScore(int score){
        score = score < 0 ? 0 : score;
        this.score += score;
    }

    /**
     * Permet d'accéder á la somme contenue dans le "porte-monnaie" global du joueur
     */
    public float getMoney() { return money; }

    /**
     * Permet d'ajouter de la monnaie au "porte-monnaie" global
     * @params {float} sum la qt de monnaie qu'on veut rajouter
     */
    public void addMoney(float sum)
    {
        sum = sum < 0 ? 0 : sum;
        this.money += sum;
    }

    /**
     * Permet d'enlever de la monnaie au "porte-monnaie" global
     * @params {float} sum la qt de monnaie qu'on veut enlever
     */
    private void subMoney(float sum)
    {
        sum = sum < 0 ? 0 : sum;
        this.money -= sum;
    }

    /* ------------------------------------------
     *          Armes Et Equipements
     * ------------------------------------------
     */
     //Access nb de slot utilisable (selon les poids des objets équipés)
    public int getCurrentlyUsableWeaponsSlot(){
        int currentWeaponsAmount = 0;
        int currentWeaponsWeightSum = 0;
        foreach (WeaponPlayer wp in this.player.weapons)
            if (wp != null)
            {
                currentWeaponsAmount++;
                currentWeaponsWeightSum += wp.weight;
            }

        return getMaxWeaponsAmount() - (currentWeaponsWeightSum - currentWeaponsAmount);
    }

    public int getWeaponCurrentlyUsableUpgradeSlot(int weaponIndex)
    {
        if (!(0 <= weaponIndex && weaponIndex < this.player.nbrMaxWeapons))  //Si on est pas dans l'intervalle possible des "slots" d'arme
            return 0;

        int currentUpgradeAmount = 0;
        int currentUpgradeWeightSum = 0;
        foreach (UpgradeWeapon up in this.player.weapons[weaponIndex].upgradeWeapons)
            if (up != null)
            {
                currentUpgradeAmount++;
                currentUpgradeWeightSum += up.weight;
            }

        return getWeaponMaxUpgradeAmount(weaponIndex) - (currentUpgradeWeightSum - currentUpgradeAmount);
    }

    public int getShipCurrentlyUsableUpgradeSlot()
    {
        int currentUpgradeAmount = 0;
        int currentUpgradeWeightSum = 0;
        foreach (UpgradeShip up in this.player.upgradeShip)
            if (up != null)
            {
                currentUpgradeAmount++;
                currentUpgradeWeightSum += up.weight;
            }

        return getShipMaxUpgradeAmount() - (currentUpgradeWeightSum - currentUpgradeAmount);
    }

    public int getDashCurrentlyUsableUpgradeSlot()
    {
        int currentUpgradeAmount = 0;
        int currentUpgradeWeightSum = 0;
        foreach (UpgradeDash up in this.dash.upgradeDashes)
            if (up != null)
            {
                currentUpgradeAmount++;
                currentUpgradeWeightSum += up.weight;
            }

        return getShipMaxUpgradeAmount() - (currentUpgradeWeightSum - currentUpgradeAmount);
    }

    public int getOndeCurrentlyUsableUpgradeSlot()
    {
        int currentUpgradeAmount = 0;
        int currentUpgradeWeightSum = 0;
        foreach (UpgradeOnde up in this.onde.upgradeOndes)
            if (up != null)
            {
                currentUpgradeAmount++;
                currentUpgradeWeightSum += up.weight;
            }

        return getShipMaxUpgradeAmount() - (currentUpgradeWeightSum - currentUpgradeAmount);
    }


    /**
    * Permet d'accéder au nombre maximal d'armes équipables
    */
    public int getMaxWeaponsAmount() => this.player.nbrMaxWeapons;

    /**
    * Permet d'accéder au nombre maximal d'amélioration équipables pour une arme
    * @params {int} index de l'arme dans la list des armes équipées
    * @return -1 si index erroné
    */
    public int getWeaponMaxUpgradeAmount(int weaponIndex)
    {
        if (!(0 <= weaponIndex && weaponIndex < this.player.nbrMaxWeapons))  //Si on est pas dans l'intervalle possible des "slots" d'arme
            return -1;
        return this.player.weapons[weaponIndex].nbrMaxUpgrade;
    }

    /**
    * Permet d'accéder au nombre maximal d'armes équipables
    */
    public int getDashMaxUpgradeAmount() => this.dash.nbrUpgradeMax;

    /**
    * Permet d'accéder au nombre maximal d'armes équipables
    */
    public int getOndeMaxUpgradeAmount() => this.onde.nbrUpgradeMax;

    /**
    * Permet d'accéder au nombre maximal d'armes équipables
    */
    public int getShipMaxUpgradeAmount() => this.player.nbrMaxUpgradeShip;

    /**
    * Permet d'accéder á la liste des armes équipées
    */
    public ReadOnlyCollection<WeaponPlayer> getWeapons() => this.player.weapons.AsReadOnly();

    /**
    * Permet d'accéder á la liste des améliorations d'une arme actuellement équipée
    * @params {int} index de l'arme dans la list des armes équipées
    * @return null si index erroné
    */
    public ReadOnlyCollection<UpgradeWeapon> getWeaponUpgrades(int weaponIndex)
    {
        if (!(0 <= weaponIndex && weaponIndex < this.player.nbrMaxWeapons))  //Si on est pas dans l'intervalle possible des "slots" d'arme
            return null;
        return this.player.weapons[weaponIndex].upgradeWeapons.AsReadOnly();
    }

    /**
    * Permet d'accéder á la liste des améliorations de dash équipées
    */
    public ReadOnlyCollection<UpgradeDash> getDashUpgrades() => this.dash.upgradeDashes.AsReadOnly();

    /**
    * Permet d'accéder á la liste des améliorations d'onde équipées
    */
    public ReadOnlyCollection<UpgradeOnde> getOndeUpgrades() => this.onde.upgradeOndes.AsReadOnly();

    /**
    * Permet d'accéder á la liste des améliorations de vaisseau équipées
    */
    public ReadOnlyCollection<UpgradeShip> getShipUpgrades() => this.player.upgradeShip.AsReadOnly();

    /**
    * Permet d'interchanger la position de 2 armes équipées
    * @params {int,int} index des armes équipées a swap
    * @return false si swap annulé
    */
    public bool swapWeapons(int weapon1Index, int weapon2Index)
    {
        //Si on est pas dans l'intervalle possible des "slots" d'arme pour un des index
        if (!(0 <= weapon1Index && weapon1Index < this.player.nbrMaxWeapons && 0 <= weapon2Index && weapon2Index < this.player.nbrMaxWeapons))  
            return false;

        //Swap
        WeaponPlayer buffer = player.weapons[weapon1Index];
        player.weapons[weapon1Index] = player.weapons[weapon2Index];
        player.weapons[weapon2Index] = buffer;

        return true; //Tout s'est bien passé
    }

    /**
    * Permet d'interchanger la position de 2 amélioration d'une arme équipée
    * @params {int,int} index de l'arme équipée, index des upgrades a swap
    * @return false si swap annulé
    */
    public bool swapWeaponUpgrades(int weaponIndex, int upgrade1Index, int upgrade2Index)
    {
        //Si on est pas dans l'intervalle possible des "slots" d'arme pour l'index d'arme
        if (!(0 <= weaponIndex && weaponIndex < this.player.nbrMaxWeapons))  
            return false;

        WeaponPlayer wp = this.player.weapons[weaponIndex];
        //Si on est pas dans l'intervalle possible des améliorations de l'arme pour un des index d'upgrade
        if (!(0 <= upgrade1Index && upgrade1Index < wp.nbrMaxUpgrade && 0 <= upgrade2Index && upgrade2Index < wp.nbrMaxUpgrade))
            return false;

        //Swap
        List<UpgradeWeapon> upgrades = wp.upgradeWeapons;
        UpgradeWeapon buffer = upgrades[upgrade1Index];
        upgrades[upgrade1Index] = upgrades[upgrade2Index];
        upgrades[upgrade2Index] = buffer;

        return true; //Tout s'est bien passé
    }

    /**
    * Permet d'interchanger la position de 2 améliorations de dash équipées
    * @params {int,int} index des améliorations équipées a swap
    * @return false si swap annulé
    */
    public bool swapDashUpgrades(int upgrade1Index, int upgrade2Index)
    {
        //Si on est pas dans l'intervalle possible des améliorations de dash pour un des index
        if (!(0 <= upgrade1Index && upgrade1Index < this.dash.nbrUpgradeMax && 0 <= upgrade2Index && upgrade2Index < this.dash.nbrUpgradeMax))
            return false;

        //Swap
        List<UpgradeDash> upgrades = dash.upgradeDashes;
        UpgradeDash buffer = upgrades[upgrade1Index];
        upgrades[upgrade1Index] = upgrades[upgrade2Index];
        upgrades[upgrade2Index] = buffer;

        return true; //Tout s'est bien passé
    }

    /**
    * Permet d'interchanger la position de 2 améliorations d'onde équipées
    * @params {int,int} index des améliorations équipées a swap
    * @return false si swap annulé
    */
    public bool swapOndeUpgrades(int upgrade1Index, int upgrade2Index)
    {
        //Si on est pas dans l'intervalle possible des améliorations d'onde pour un des index
        if (!(0 <= upgrade1Index && upgrade1Index < this.onde.nbrUpgradeMax && 0 <= upgrade2Index && upgrade2Index < this.onde.nbrUpgradeMax))
            return false;

        //Swap
        List<UpgradeOnde> upgrades = onde.upgradeOndes;
        UpgradeOnde buffer = upgrades[upgrade1Index];
        upgrades[upgrade1Index] = upgrades[upgrade2Index];
        upgrades[upgrade2Index] = buffer;

        return true; //Tout s'est bien passé
    }

    /**
    * Permet d'interchanger la position de 2 améliorations de vaisseau équipées
    * @params {int,int} index des améliorations équipées a swap
    * @return false si swap annulé
    */
    public bool swapShipUpgrades(int upgrade1Index, int upgrade2Index)
    {
        //Si on est pas dans l'intervalle possible des améliorations de vaisseau pour un des index
        if (!(0 <= upgrade1Index && upgrade1Index < this.player.nbrMaxUpgradeShip && 0 <= upgrade2Index && upgrade2Index < this.player.nbrMaxUpgradeShip))
            return false;

        //Swap
        List<UpgradeShip> upgrades = player.upgradeShip;
        UpgradeShip buffer = upgrades[upgrade1Index];
        upgrades[upgrade1Index] = upgrades[upgrade2Index];
        upgrades[upgrade2Index] = buffer;

        return true; //Tout s'est bien passé
    }

    /**
    * Permet de definir l'arme a un index donné
    * @params {int,UpgradeWeapon} index de l'arme, weapon a set
    * @return false si set annulé
    */
    public bool setWeapon(int weaponIndex, WeaponPlayer weapon)
    {
        //Si on est pas dans l'intervalle possible des "slots" d'arme
        if (!(0 <= weaponIndex && weaponIndex < this.player.nbrMaxWeapons))
            return false;

        this.player.weapons[weaponIndex] = weapon;

        this.extendListSize(weaponIndex);
        return true; //Tout s'est bien passé
    }

    /**
    * Permet de definir l'amélioration d'arme a un index donné pour une arme donnée
    * @params {int,int,UpgradeWeapon} index de l'arme, index de l'upgrade, upgrade a set
    * @return false si set annulé
    */
    public bool setWeaponUpgrade(int weaponIndex, int upgradeIndex, UpgradeWeapon upgrade)
    {
        //Si on est pas dans l'intervalle possible des "slots" d'arme
        if (!(0 <= weaponIndex && weaponIndex < this.player.nbrMaxWeapons))
            return false;

        WeaponPlayer wp = this.player.weapons[weaponIndex];
        //Si on est pas dans l'intervalle possible des "slots" d'amélioration de l'arme
        if (!(0 <= upgradeIndex && upgradeIndex < wp.nbrMaxUpgrade))
            return false;

        wp.upgradeWeapons[upgradeIndex] = upgrade;
        return true; //Tout s'est bien passé
    }

    /**
    * Permet de definir l'amélioration de dash a un index donné
    * @params {int,int,UpgradeDash} index de l'upgrade, upgrade a set
    * @return false si set annulé
    */
    public bool setDashUpgrade(int upgradeIndex, UpgradeDash upgrade)
    {
        //Si on est pas dans l'intervalle possible des "slots" d'amélioration de dash
        if (!(0 <= upgradeIndex && upgradeIndex < this.dash.nbrUpgradeMax))
            return false;

        this.dash.upgradeDashes[upgradeIndex] = upgrade;
        return true; //Tout s'est bien passé
    }

    /**
    * Permet de definir l'amélioration d'onde a un index donné
    * @params {int,int,UpgradeOnde} index de l'upgrade, upgrade a set
    * @return false si set annulé
    */
    public bool setOndeUpgrade(int upgradeIndex, UpgradeOnde upgrade)
    {
        //Si on est pas dans l'intervalle possible des "slots" d'amélioration d'onde
        if (!(0 <= upgradeIndex && upgradeIndex < this.onde.nbrUpgradeMax))
            return false;

        this.onde.upgradeOndes[upgradeIndex] = upgrade;
        return true; //Tout s'est bien passé
    }

    /**
    * Permet de definir l'amélioration de vaisseau a un index donné
    * @params {int,int,UpgradeShip} index de l'upgrade, upgrade a set
    * @return false si set annulé
    */
    public bool setShipUpgrade(int upgradeIndex, UpgradeShip upgrade)
    {
        //Si on est pas dans l'intervalle possible des "slots" d'amélioration de vaisseau
        if (!(0 <= upgradeIndex && upgradeIndex < this.player.nbrMaxUpgradeShip))
            return false;

        this.player.upgradeShip[upgradeIndex] = upgrade;
        return true; //Tout s'est bien passé
    }

    /*
     * 
     * TEMP
     * 
     */

    /**
     * Fonction permettant de s'avoir si on peut aceheter un item
     * @params {Item} item l'item qu'on veut acheter
     * @returns {bool} Renvois vrai si l'item peut être acheté
     */
    /*public bool CanBuyItem(Item item)
    {
        return item.IsAvailable() && item.GetPrice() <= money;
    }*/

    /**
     * Fonction permettant d'update l'argent du joueur selon l'item 
     * @params {Item} item
     */
    /*public void BuyItem(Item item)
    {
        money -= item.GetPrice();
    }*/

    /**
     * Permet d'acheter une arme
     * @params {ItemWeapon} item
     * @returns {bool} retourne true si l'arme a été acheté, false si echec
     */
    /*public bool BuyWeapon(ItemWeapon item)
    {
        //1 - Check si on peut acheter l'item
        if (!CanBuyItem(item)) return false;
        //2 - Check si la place est disponible dans l'inventaire
        if (weapons.Count + 1 <= weaponInventoryMax) return false;
        //3 - On achète l'item
        BuyItem(item);
        WeaponPlayer weaponPlayer = item.ObtainItem(); //On rend l'item invalide
        weapons.Add(weaponPlayer); //On ajoute l'item à la liste

        return true;
    }*/

    /**
     * Permet d'acheter une Upgrade
     * @params {ItemUpgrade} item
     * @returns {bool} retourne true si l'upgrade a été acheté, false si echec
     */
    /*public bool BuyUpgrade(ItemUpgrade item)
    {
        //1 - Check si on peut acheter l'item
        if (!CanBuyItem(item)) return false;
        //3 - On achète l'item
        BuyItem(item);
        Upgrade weaponPlayer = item.ObtainItem(); //On rend l'item invalide
        upgrades.Add(weaponPlayer); //On ajoute l'item à la liste

        return true;
    }*/
}
