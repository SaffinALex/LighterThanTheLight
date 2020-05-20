using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DraggableObject : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Canvas canvas;
    public GameObject upgrade;
    private RectTransform rect;
    private CanvasGroup canvasG;
    private Vector2 originalPos;

    private bool iconLoaded;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rect = GetComponent<RectTransform>();
        canvasG = GetComponent<CanvasGroup>();
        iconLoaded = false;
        if (!iconLoaded && upgrade != null)
            if(upgrade.GetComponent<Upgrade>() != null)
                GetComponent<Image>().sprite = upgrade.GetComponent<Upgrade>().icone;
            if (upgrade.GetComponent<WeaponPlayer>() != null)
                GetComponent<Image>().sprite = upgrade.GetComponent<WeaponPlayer>().icon;
    }

    // Update is called once per frame
    void Update()
    {
        if (!iconLoaded && upgrade != null && upgrade.GetComponent<Upgrade>() != null)
            GetComponent<Image>().sprite = upgrade.GetComponent<Upgrade>().icone;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");

        canvasG.alpha = 1f;
        canvasG.blocksRaycasts = true;

        rect.anchoredPosition = originalPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasG.alpha = 0.5f;
        canvasG.blocksRaycasts = false;
        originalPos = rect.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / (canvas.scaleFactor * this.transform.parent.gameObject.GetComponent<RectTransform>().localScale.x);
    }

    public bool UpgradeIsOfType<ComponentType>() where ComponentType : UnityEngine.Component
    {
        return upgrade.GetComponent<ComponentType>() != null;
    }

    public bool ThisIsOfType<ComponentType>() where ComponentType : UnityEngine.Component
    {
        return upgrade.GetComponent<ComponentType>() != null;
    }

    public GameObject GetUpgrade()
    {
        return upgrade;
    }

    public void ResetPos()
    {
        if (originalPos != null)
            rect.anchoredPosition = originalPos;
    }

    public InventorySlot getInventorySlotParent()
    {
        return GetComponentInParent<InventorySlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EquipmentManager.GetEquipmentUI().overText.transform.SetParent(this.transform);
        EquipmentManager.GetEquipmentUI().overText.setPos(new Vector2(0, 0));
        WeaponPlayer wp = upgrade.GetComponent<WeaponPlayer>();
        Upgrade up = upgrade.GetComponent<Upgrade>();
        if (wp != null)
            EquipmentManager.GetEquipmentUI().overText.show(wp.name,"",wp.price);
        else if (up != null)
            EquipmentManager.GetEquipmentUI().overText.show(up.nom, up.description, up.price);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EquipmentManager.GetEquipmentUI().overText.hide();
    }
}

