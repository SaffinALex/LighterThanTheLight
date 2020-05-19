using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class RandomRewardsEvent : Event
{
    [SerializeField] protected float minPrice;
    [SerializeField] protected float maxPrice;
    //Fonction qui sera appelé lorsque l'event débute, permet l'initialisation
    protected override void BeginEvent(){
        Debug.Log(GetScore() + " " + minPrice + " " + maxPrice);
        if(minPrice > maxPrice) maxPrice = minPrice;
        Vector2 positionSpawn = new Vector2(0, EnnemiesBorder.size.y / 2 - 1);
        List<GameObject> collectionRewards = new List<GameObject>();
        ReadOnlyCollection<GameObject> allRewards = App.ressourcesLoader.getUpgrades();
        ReadOnlyCollection<GameObject> allWeaponsRewards = App.ressourcesLoader.getWeaponRewards();
        for(int i = 0; i < allRewards.Count; i++){
            var price = allRewards[i].GetComponent<Upgrade>().getPrice();
            Debug.Log(price);
            if(price <= maxPrice && price >= minPrice) collectionRewards.Add(allRewards[i]);
        }
        for(int i = 0; i < allWeaponsRewards.Count; i++){
            var price = allWeaponsRewards[i].GetComponent<WeaponPlayer>().getPrice();
            if (price <= maxPrice && price >= minPrice) collectionRewards.Add(allWeaponsRewards[i]);
        }

        if(collectionRewards.Count > 0){
            int rand = Random.Range(0, collectionRewards.Count);
            var o = Instantiate(collectionRewards[rand]);
            o.transform.position = positionSpawn;
        }
    }

    //Fonction qui sera appelé lorsque l'event fonctionne
    protected override void UpdateEvent(){

    }

    //Fonction qui sera appelé lorsque l'event fonctionne
    public override float GetScore(){
        return ((minPrice + maxPrice) / 2) * 0.3f;
    }
}
