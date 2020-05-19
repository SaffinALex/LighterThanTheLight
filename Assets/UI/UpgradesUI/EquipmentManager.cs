using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private InventoryPanel inventoryPanel;
    [SerializeField] private ShopPanel shopPanel;
    public Shop currentShop;

    private static EquipmentManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        inventoryPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
        instance = this;
    }

    public void reloadInventoryPanel()
    {
        inventoryPanel.feedInventoryUI();
    }

    public void reloadShopPanel()
    {
        shopPanel.feedShopPanel();
    }

    public static EquipmentManager GetEquipmentUI()
    {
        if (instance == null)
        {
            Debug.LogError("Aucune instance de EquipmentManager présente dans la scene");
            return null;
        }
        else
            return instance;
    }

    public int getCurrentSelectedWeaponIndex()
    {
        return inventoryPanel.currentSelectedWeapon;
    }

    public void openShop(Shop shop)
    {
        inventoryPanel.gameObject.SetActive(true);
        shopPanel.gameObject.SetActive(true);
        currentShop = shop;
        inventoryPanel.initPanel();
        shopPanel.initShopPanel(currentShop);
    }

    public void closeShop()
    {
        inventoryPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);

        currentShop.CloseShop();
    } 
}
