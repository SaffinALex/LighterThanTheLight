using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    private Shop shop;
    public GameObject shopObjectPrefab;
    public GameObject shopSlotsPrefab;
    public GameObject shopSlotsContainer;
    private List<GameObject> shopSlots = new List<GameObject>();

    public void initShopPanel(Shop shop)
    {
        clearAllInShopPanel();
        this.shop = shop;
        shopSlots = new List<GameObject>();

        for (int i = 0; i < shop.getAllItems().Count; i++)
        {
            GameObject go = Instantiate(shopSlotsPrefab);
            go.transform.SetParent(shopSlotsContainer.transform, false);
            go.GetComponent<InventorySlot>().setItemIndex(i);
            go.GetComponent<ShopSlot>().setShopPanel(this);
            shopSlots.Add(go);
        }
        feedShopPanel();
    }

    public void feedShopPanel()
    {
        this.clearShopPanel();

        List<Item> items = shop.getAllItems();

        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].IsAvailable())
                shopSlots[i].GetComponent<InventorySlot>().SetActive(false);

            GameObject go = Instantiate(shopObjectPrefab);
            GameObject upGO;
            if(items[i] is ItemWeapon)
            {
                upGO = Instantiate((items[i] as ItemWeapon).ObtainItem().gameObject);
                (items[i] as ItemWeapon).RevertBuying();
            }
            else
            {
                upGO = Instantiate((items[i] as ItemUpgrade).ObtainItem().gameObject);
                (items[i] as ItemUpgrade).RevertBuying();
            }
            upGO.transform.SetParent(go.transform);
            go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
            go.GetComponent<DraggableObject>().upgrade = upGO;

            go.transform.SetParent(shopSlots[i].transform);
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    public void clearShopPanel()
    {
        foreach (GameObject go in shopSlots)
            foreach (Transform child in go.transform)
                if (child != null)
                    Destroy(child.gameObject);
    }

    public void clearAllInShopPanel()
    {
        foreach (GameObject go in shopSlots)
            if (go != null)
            {
                foreach (Transform child in go.transform)
                    if (child != null)
                        Destroy(child.gameObject);
                Destroy(go);
            }
    }

    public Item getItem(int itemIndex)
    {
        return shop.getAllItems()[itemIndex];
    }
}
