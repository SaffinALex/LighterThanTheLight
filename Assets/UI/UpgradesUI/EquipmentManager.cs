using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private InventoryPanel inventoryPanel;
    [SerializeField] private ShopPanel shopPanel;
    [SerializeField] private LootPanel lootPanel;
    public OverText overText;
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

    public void reloadLootPanel()
    {
        lootPanel.initLootPanel();
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
        Time.timeScale = 0;
        inventoryPanel.gameObject.SetActive(true);
        shopPanel.gameObject.SetActive(true);
        currentShop = shop;
        inventoryPanel.initPanel();
        shopPanel.initShopPanel(currentShop);
    }

    public void closeShop()
    {
        Time.timeScale = 1;
        inventoryPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);

        currentShop.CloseShop();
    }

    public void openLoot()
    {
        Time.timeScale = 0;
        inventoryPanel.gameObject.SetActive(true);
        lootPanel.gameObject.SetActive(true);
        inventoryPanel.initPanel();
        lootPanel.initLootPanel();
    }

    public void closeLoot()
    {
        Time.timeScale = 1;
        inventoryPanel.gameObject.SetActive(false);
        lootPanel.gameObject.SetActive(false);

        App.playerManager.getInventory().flushItemInventory();
    }

    public GameObject GetOverText()
    {
        return Instantiate(overText.gameObject);
    }
}
