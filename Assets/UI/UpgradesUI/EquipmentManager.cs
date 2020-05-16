using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private InventoryPanel inventoryPanel;
    [SerializeField] private ShopPanel shopPanel;

    private static EquipmentManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    void Update()
    {
    }

    public void reloadInventoryPanel()
    {
        inventoryPanel.feedInventoryUI();
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
}
