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
            go.transform.SetParent(shipUpgradeSlotContainer.transform, false);
            go.GetComponent<InventorySlot>().setItemIndex(i);
            shipUpgradeSlots.Add(go);
        }
        for (int i = 0; i < playerManager.getOndeMaxUpgradeAmount(); i++)
        {
            GameObject go = Instantiate(ondeUpgradeSlotPrefab);
            go.transform.SetParent(ondeUpgradeSlotContainer.transform, false);
            go.GetComponent<InventorySlot>().setItemIndex(i);
            ondeUpgradeSlots.Add(go);
        }
        for (int i = 0; i < playerManager.getDashMaxUpgradeAmount(); i++)
        {
            GameObject go = Instantiate(dashUpgradeSlotPrefab);
            go.transform.SetParent(dashUpgradeSlotContainer.transform, false);
            go.GetComponent<InventorySlot>().setItemIndex(i);
            dashUpgradeSlots.Add(go);
        }
        for (int i = 0; i < playerManager.getMaxWeaponsAmount(); i++)
        {
            GameObject go = Instantiate(weaponSlotPrefab);
            go.transform.SetParent(weaponSlotContainer.transform, false);
            go.GetComponent<InventorySlot>().setItemIndex(i);
            weaponSlots.Add(go);

            go = Instantiate(weaponSelectorPrefab);
            go.transform.SetParent(weaponSelectorContainer.transform, false);
            go.GetComponentInChildren<Text>().text = (i+1).ToString();
            int index = new int();
            index = i;
            go.GetComponent<Button>().onClick.AddListener(delegate {
                Debug.Log(currentSelectedWeapon);
                currentSelectedWeapon = index;
                if (playerManager.getWeapons()[index] != null)
                {
                    feedInventoryUI();
                }
            });
        }
        feedInventoryUI();
    }
     
    public void feedInventoryUI()
    {
        this.clearInventoryUI();
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

            go.transform.SetParent(weaponSlots[i].transform);
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            i++;
        }

        for (int j = 0; j < playerManager.getWeapons().Count; j++){
            Debug.Log(j);
            if (playerManager.getWeapons()[j] != null){
                Debug.Log("Weapon trouvé");
                currentSelectedWeapon = j;
            }
        }
        selectWeapon(currentSelectedWeapon);
    }

    public void selectWeapon(int index)
    {
        //Debug.Log("Check weapon valide");
        if (currentSelectedWeapon == -1 || playerManager.getWeapons()[index] == null)
            return;

        //Debug.Log("Boucle upgrades");
        for (int j = 0; j < playerManager.getWeapons()[index].upgradeWeapons.Count; j++)
        {
            Debug.Log(j);
            //weaponUpgradeSlots.Clear();
            GameObject slot = Instantiate(weaponUpgradeSlotPrefab);
            slot.transform.SetParent(weaponUpgradeSlotContainer.transform, false);
            slot.GetComponent<InventorySlot>().setItemIndex(j);
            slot.GetComponent<UpgradeWeaponSlot>().WeaponIndex = index;
            weaponUpgradeSlots.Add(slot);

            //Debug.Log("Test upgradevalide");
            UpgradeWeapon up = playerManager.getWeapons()[index].upgradeWeapons[j];
            if (up == null){
                //Debug.Log("ça pue");
                continue;
            }
            //Debug.Log("c'est bon");

            GameObject go = Instantiate(upgradeObjectPrefab);
            GameObject upGO = new GameObject();
            upGO.name = "UpgradeContainer";
            UnityEditorInternal.ComponentUtility.CopyComponent(up);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(upGO);
            upGO.transform.SetParent(go.transform);
            //Debug.Log("on rattache l'upgrade");
            go.GetComponent<DraggableObject>().canvas = gameObject.transform.parent.GetComponent<Canvas>();
            go.GetComponent<DraggableObject>().upgrade = upGO;
            go.transform.SetParent(slot.transform);
            //Debug.Log("on rattache l'upgrade object");

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
    }
}
