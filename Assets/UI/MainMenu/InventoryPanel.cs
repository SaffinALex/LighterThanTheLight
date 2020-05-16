using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    private PlayerManager playerManager;

    public GameObject weaponSlotContainer;
    public GameObject weaponUpgradeSlotContainer;
    public GameObject shipUpgradeSlotContainer;
    public GameObject dashUpgradeSlotContainer;
    public GameObject ondeUpgradeSlotContainer;

    public GameObject upgradeObjectPrefab;

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
            go.transform.SetParent(shipUpgradeSlotContainer.transform, false);
            shipUpgradeSlots.Add(go);
        }
        for (int i = 0; i < playerManager.getOndeMaxUpgradeAmount(); i++)
        {
            GameObject go = Instantiate(ondeUpgradeSlotPrefab);
            go.transform.SetParent(ondeUpgradeSlotContainer.transform, false);
            dashUpgradeSlots.Add(go);
        }
        for (int i = 0; i < playerManager.getDashMaxUpgradeAmount(); i++)
        {
            GameObject go = Instantiate(dashUpgradeSlotPrefab);
            go.transform.SetParent(dashUpgradeSlotContainer.transform, false);
            ondeUpgradeSlots.Add(go);
        }
        for (int i = 0; i < playerManager.getMaxWeaponsAmount(); i++)
        {
            GameObject go = Instantiate(weaponSlotPrefab);
            go.transform.SetParent(weaponSlotContainer.transform, false);
            weaponSlots.Add(go);
        }
        feedInventoryUI();
    }
     
    public void feedInventoryUI()
    {
        this.clearInventoryUI();
        int i = 0;
        foreach (UpgradeShip up in playerManager.getShipUpgrades())
        {
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
            GameObject go = Instantiate(upgradeObjectPrefab);
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

        if (playerManager.getWeapons().Count != 0)
        {
            i = 0;
            foreach (UpgradeWeapon up in playerManager.getWeapons()[0].upgradeWeapons)
            {
                GameObject go = Instantiate(upgradeObjectPrefab);
                GameObject upGO = new GameObject();
                upGO.name = "UpgradeContainer";
                UnityEditorInternal.ComponentUtility.CopyComponent(up);
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(upGO);
                upGO.transform.SetParent(go.transform);
                go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
                go.GetComponent<DraggableObject>().upgrade = upGO;

                go.transform.SetParent(weaponUpgradeSlots[i].transform);
                go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                i++;
            }
        }
        
    }

    public void clearInventoryUI(){
        foreach (GameObject go in weaponSlots)
            foreach (GameObject child in go.transform)
                Destroy(child);
        foreach (GameObject go in weaponUpgradeSlots)
            foreach (GameObject child in go.transform)
                Destroy(child);
        foreach (GameObject go in shipUpgradeSlots)
            foreach (GameObject child in go.transform)
                Destroy(child);
        foreach (GameObject go in dashUpgradeSlots)
            foreach (GameObject child in go.transform)
                Destroy(child);
        foreach (GameObject go in ondeUpgradeSlots)
            foreach (GameObject child in go.transform)
                Destroy(child);
    }
}
