using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class NodeShop : NodeElement
{
    private Shop shop;
    protected readonly int MaxArticles;

    public override void InitializeNode(float score = 0) {
        scoreDifficulty = score;
        ReadOnlyCollection<GameObject> allUpgrades = App.ressourcesLoader.getUpgrades();
        ReadOnlyCollection<GameObject> allWeapons = App.ressourcesLoader.getWeapons();
        Debug.Log("Nombres d'upgrades totales " + allUpgrades.Count);

        for(int i = 0; i < MaxArticles; i++){
            bool isAWeapon = Random.Range(0,3) == 0; //1 / 3 chance
            if(isAWeapon){
                shop.AddWeaponItem(new ItemWeapon( allWeapons[Random.Range(0, allWeapons.Count)].GetComponent<WeaponPlayer>() ));
            }else{
                shop.AddUpgradeItem(new ItemUpgrade( allUpgrades[Random.Range(0, allUpgrades.Count)].GetComponent<Upgrade>() ));
            }
        }
    }

    public override void Begin(){
        this.End();
        return;
    }
}
