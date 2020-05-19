using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class NodeShop : NodeElement
{
    private Shop shop;
    protected readonly int MaxArticles = 3;

    protected bool accessNode;

    public override void InitializeNode(float score = 0) {
        Debug.Log(MaxArticles);
        accessNode = true;
        scoreDifficulty = score;
        shop = new Shop();
        ReadOnlyCollection<GameObject> allUpgrades = App.ressourcesLoader.getUpgrades();
        ReadOnlyCollection<GameObject> allWeapons = App.ressourcesLoader.getWeapons();
        // Debug.Log("Nombres d'upgrades totales " + allUpgrades.Count);

        for(int i = 0; i < MaxArticles; i++){
            bool isAWeapon = Random.Range(0,3) == 0; //1 / 3 chance
            if(isAWeapon){
                GameObject go = Instantiate(allWeapons[Random.Range(0, allWeapons.Count)]);
                foreach (var component in go.GetComponents<Component>())
                {
                    Debug.Log(component);
                }
                //Debug.Log(go.GetComponent<WeaponPlayer>());
                shop.AddWeaponItem(new ItemWeapon(go.GetComponent<WeaponPlayer>() ));
            }else{
                shop.AddUpgradeItem(new ItemUpgrade( allUpgrades[Random.Range(0, allUpgrades.Count)].GetComponent<Upgrade>() ));
            }
        }

        shop.GetEventCloseShop().AddListener(shopClose);
    }

    protected void shopClose(){
        accessNode = false;
        this.End();
    }

    public override void Begin(){
        if(accessNode){
            EquipmentManager.GetEquipmentUI().openShop(shop);
        }
        return;
    }
}
