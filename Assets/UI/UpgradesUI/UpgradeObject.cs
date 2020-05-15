using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeObject : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject upgrade;
    private RectTransform rect;
    private CanvasGroup canvasG;
    private Vector2 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        canvasG = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

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
        //Debug.Log("OnDrag");
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public bool UpgradeIsOfType<ComponentType>() where ComponentType : UnityEngine.Component
    {
        return upgrade.GetComponent<ComponentType>() != null;
    }

    public GameObject GetUpgrade(){
        return upgrade;
    }

    public void ResetPos()
    {
        if(originalPos != null)
            rect.anchoredPosition = originalPos;
    }
}
