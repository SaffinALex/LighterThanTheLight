using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPanel : MonoBehaviour
{
    public GameObject lootObjectPrefab;
    public GameObject lootSlotsPrefab;
    public GameObject lootSlotsContainer;
    private List<GameObject> lootSlots = new List<GameObject>();

    public void initLootPanel()
    {
        List<WeaponPlayer> weapons = App.playerManager.getInventory().getWeapons();
        List<Upgrade> upgrades = App.playerManager.getInventory().getUpgrades();
        clearAllInLootPanel();

        lootSlots = new List<GameObject>();

        for (int i = 0; i < weapons.Count + upgrades.Count; i++)
        {
            GameObject go = Instantiate(lootSlotsPrefab);
            go.transform.SetParent(lootSlotsContainer.transform, false);
            lootSlots.Add(go);
        }
        feedLootPanel();
    }

    public void feedLootPanel()
    {
        this.clearLootPanel();

        List<WeaponPlayer> weapons = App.playerManager.getInventory().getWeapons();
        List<Upgrade> upgrades = App.playerManager.getInventory().getUpgrades();

        for (int i = 0; i < weapons.Count; i++)
        {
            GameObject go = Instantiate(lootObjectPrefab);
            GameObject upGO = weapons[i].gameObject;
            upGO.transform.SetParent(go.transform);
            go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
            go.GetComponent<DraggableObject>().upgrade = upGO;

            go.transform.SetParent(lootSlots[i].transform);
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        for (int i = 0; i < upgrades.Count; i++)
        {
            GameObject go = Instantiate(lootObjectPrefab);
            GameObject upGO = upgrades[i].gameObject;
            upGO.transform.SetParent(go.transform);
            go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
            go.GetComponent<DraggableObject>().upgrade = upGO;

            go.transform.SetParent(lootSlots[i + weapons.Count].transform);
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    public void clearLootPanel()
    {
        foreach (GameObject go in lootSlots)
            foreach (Transform child in go.transform)
                if (child != null)
                    Destroy(child.gameObject);
    }

    public void clearAllInLootPanel()
    {
        foreach (GameObject go in lootSlots)
            if (go != null)
            {
                foreach (Transform child in go.transform)
                    if (child != null)
                        Destroy(child.gameObject);
                Destroy(go);
            }
    }
}
