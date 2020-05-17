using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    private PlayerManager playerManager;

    public GameObject weaponSlotContainer;
    public GameObject weaponUpgradeSlotContainer;
    public GameObject shipUpgradeSlotContainer;
    public GameObject dashUpgradeSlotContainer;
    public GameObject ondeUpgradeSlotContainer;

    public GameObject upgradeObjectPrefab;

    public GameObject weaponSelectorPrefab;
    public GameObject weaponSelectorContainer;


    public GameObject weaponSlotPrefab;
    public GameObject weaponUpgradeSlotPrefab;
    public GameObject shipUpgradeSlotPrefab;
    public GameObject dashUpgradeSlotPrefab;
    public GameObject ondeUpgradeSlotPrefab;

    private List<GameObject> weaponSlots;
    private List<GameObject> weaponUpgradeSlots;
    private List<GameObject> shipUpgradeSlots;
    private List<GameObject> dashUpgradeSlots;
    private List<GameObject> ondeUpgradeSlots;

    public int currentSelectedWeapon = -1;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = App.playerManager;
        weaponSlots = new List<GameObject>();
        weaponUpgradeSlots = new List<GameObject>();
        shipUpgradeSlots = new List<GameObject>();
        dashUpgradeSlots = new List<GameObject>();
        ondeUpgradeSlots = new List<GameObject>();

        for (int i = 0; i < playerManager.getShipMaxUpgradeAmount(); i++){
            GameObject go = Instantiate(shipUpgradeSlotPrefab);
            if (i < playerManager.getShipCurrentlyUsableUpgradeSlot() - 1)
                go.GetComponent<InventorySlot>().SetActive(true);
            go.transform.SetParent(shipUpgradeSlotContainer.transform, false);
            go.GetComponent<InventorySlot>().setItemIndex(i);
            shipUpgradeSlots.Add(go);
        }
        for (int i = 0; i < playerManager.getOndeMaxUpgradeAmount(); i++)
        {
            GameObject go = Instantiate(ondeUpgradeSlotPrefab);
            if (i < playerManager.getOndeCurrentlyUsableUpgradeSlot() - 1)
                go.GetComponent<InventorySlot>().SetActive(true);
            go.transform.SetParent(ondeUpgradeSlotContainer.transform, false);
            go.GetComponent<InventorySlot>().setItemIndex(i);
            ondeUpgradeSlots.Add(go);
        }
        for (int i = 0; i < playerManager.getDashMaxUpgradeAmount(); i++)
        {
            GameObject go = Instantiate(dashUpgradeSlotPrefab);
            if (i < playerManager.getDashCurrentlyUsableUpgradeSlot() - 1)
                go.GetComponent<InventorySlot>().SetActive(true);
            go.transform.SetParent(dashUpgradeSlotContainer.transform, false);
            go.GetComponent<InventorySlot>().setItemIndex(i);
            dashUpgradeSlots.Add(go);
        }
        for (int i = 0; i < playerManager.getMaxWeaponsAmount(); i++)
        {
            GameObject go = Instantiate(weaponSlotPrefab);
            Debug.Log(playerManager.getCurrentlyUsableWeaponsSlot() - 1);
            if (i < playerManager.getCurrentlyUsableWeaponsSlot() - 1)
                go.GetComponent<InventorySlot>().SetActive(true);
            go.transform.SetParent(weaponSlotContainer.transform, false);
            go.GetComponent<InventorySlot>().setItemIndex(i);
            weaponSlots.Add(go);
        }
        feedInventoryUI();
    }
     
    public void feedInventoryUI()
    {
        this.clearInventoryUI();

        for (int j = 0; j < playerManager.getShipMaxUpgradeAmount(); j++)
        {
            if (j < playerManager.getShipCurrentlyUsableUpgradeSlot() - 1)
                shipUpgradeSlots[j].GetComponent<InventorySlot>().SetActive(true);
        }
        for (int j = 0; j < playerManager.getOndeMaxUpgradeAmount(); j++)
        {
            if (j < playerManager.getOndeCurrentlyUsableUpgradeSlot() - 1)
                ondeUpgradeSlots[j].GetComponent<InventorySlot>().SetActive(true);
        }
        for (int j = 0; j < playerManager.getDashMaxUpgradeAmount(); j++)
        {
            if (j < playerManager.getDashCurrentlyUsableUpgradeSlot() - 1)
                dashUpgradeSlots[j].GetComponent<InventorySlot>().SetActive(true);
        }
        for (int j = 0; j < playerManager.getMaxWeaponsAmount(); j++)
        {
            if (j < playerManager.getCurrentlyUsableWeaponsSlot() - 1)
                weaponSlots[j].GetComponent<InventorySlot>().SetActive(true);
        }


        int i = 0;
        foreach (UpgradeShip up in playerManager.getShipUpgrades())
        {
            if (up == null)
            {
                i++;
                continue;
            }
            GameObject go = Instantiate(upgradeObjectPrefab);
            GameObject upGO = new GameObject();
            upGO.name = "UpgradeContainer";
            UnityEditorInternal.ComponentUtility.CopyComponent(up);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(upGO);
            upGO.transform.SetParent(go.transform);
            go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
            go.GetComponent<DraggableObject>().upgrade = upGO;

            go.transform.SetParent(shipUpgradeSlots[i].transform);
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            i ++;
        }

        i = 0;
        foreach (UpgradeOnde up in playerManager.getOndeUpgrades())
        {
            if (up == null)
            {
                i++;
                continue;
            }
            GameObject go = Instantiate(upgradeObjectPrefab);
            GameObject upGO = new GameObject();
            upGO.name = "UpgradeContainer";
            UnityEditorInternal.ComponentUtility.CopyComponent(up);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(upGO);
            upGO.transform.SetParent(go.transform);
            go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
            go.GetComponent<DraggableObject>().upgrade = upGO;

            go.transform.SetParent(ondeUpgradeSlots[i].transform);
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            i++;
        }

        i = 0;
        foreach (UpgradeDash up in playerManager.getDashUpgrades())
        {
            if (up == null)
            {
                i++;
                continue;
            }
            GameObject go = Instantiate(upgradeObjectPrefab);
            GameObject upGO = new GameObject();
            upGO.name = "UpgradeContainer";
            UnityEditorInternal.ComponentUtility.CopyComponent(up);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(upGO);
            upGO.transform.SetParent(go.transform);
            go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
            go.GetComponent<DraggableObject>().upgrade = upGO;
            

            go.transform.SetParent(dashUpgradeSlots[i].transform);
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            i++;
        }

        i = 0;
        foreach (WeaponPlayer up in playerManager.getWeapons())
        {
            GameObject go = Instantiate(weaponSelectorPrefab);
            go.transform.SetParent(weaponSelectorContainer.transform, false);
            go.GetComponentInChildren<Text>().text = (i + 1).ToString();
            go.GetComponent<Button>().interactable = false;
            int index = new int();
            index = i;

            if (playerManager.getWeapons()[index] != null)
            {
                go.GetComponent<Button>().interactable = true;
            }

            go.GetComponent<Button>().onClick.AddListener(delegate {
                currentSelectedWeapon = index;
                if (playerManager.getWeapons()[index] != null)
                {
                    feedInventoryUI();
                }
            });
            
            if (up == null)
            {
                i++;
                continue;
            }

            go = Instantiate(upgradeObjectPrefab);
            GameObject upGO = new GameObject();
            upGO.name = "UpgradeContainer";
            UnityEditorInternal.ComponentUtility.CopyComponent(up);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(upGO);
            upGO.transform.SetParent(go.transform);
            go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
            go.GetComponent<DraggableObject>().upgrade = upGO;

            go.transform.SetParent(weaponSlots[i].transform);
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            i++;
        }

        for (int j = 0; j < playerManager.getWeapons().Count; j++){
            if (playerManager.getWeapons()[j] != null){
                currentSelectedWeapon = j;
            }
        }
        selectWeapon(currentSelectedWeapon);
    }

    public void selectWeapon(int index)
    {
        if (currentSelectedWeapon == -1 || playerManager.getWeapons()[index] == null)
            return;

        for (int j = 0; j < playerManager.getWeapons()[index].upgradeWeapons.Count; j++)
        {
            GameObject slot = Instantiate(weaponUpgradeSlotPrefab);
            if (j < playerManager.getCurrentlyUsableWeaponsSlot() - 1)
                slot.GetComponent<InventorySlot>().SetActive(true);
            slot.transform.SetParent(weaponUpgradeSlotContainer.transform, false);
            slot.GetComponent<InventorySlot>().setItemIndex(j);
            slot.GetComponent<UpgradeWeaponSlot>().WeaponIndex = index;
            weaponUpgradeSlots.Add(slot);

            UpgradeWeapon up = playerManager.getWeapons()[index].upgradeWeapons[j];
            if (up == null){
                continue;
            }

            GameObject go = Instantiate(upgradeObjectPrefab);
            GameObject upGO = new GameObject();
            upGO.name = "UpgradeContainer";
            UnityEditorInternal.ComponentUtility.CopyComponent(up);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(upGO);
            upGO.transform.SetParent(go.transform);
            go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
            go.GetComponent<DraggableObject>().upgrade = upGO;
            go.transform.SetParent(slot.transform);

            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    public void clearInventoryUI(){
        foreach (GameObject go in weaponSlots)
            foreach (Transform child in go.transform)
                if (child != null)
                    Destroy(child.gameObject);
        foreach (GameObject go in weaponUpgradeSlots)
        {
            if (go != null)
            {
                foreach (Transform child in go.transform)
                {
                    if (child != null)
                        Destroy(child.gameObject);
                }
                Destroy(go);
            }
        }
        foreach (GameObject go in shipUpgradeSlots)
            foreach (Transform child in go.transform)
                if (child != null)
                    Destroy(child.gameObject);
        foreach (GameObject go in dashUpgradeSlots)
            foreach (Transform child in go.transform)
                if (child != null)
                    Destroy(child.gameObject);
        foreach (GameObject go in ondeUpgradeSlots)
            foreach (Transform child in go.transform)
                if (child != null)
                    Destroy(child.gameObject);

        foreach (Transform child in weaponSelectorContainer.transform)
            if (child != null)
                Destroy(child.gameObject);
    }
}
