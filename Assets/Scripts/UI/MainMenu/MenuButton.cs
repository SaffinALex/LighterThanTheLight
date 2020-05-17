using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
[RequireComponent (typeof(TextMeshProUGUI))]
public class MenuButton : MonoBehaviour
{
    public float hoveredSizeOffset = 2.0f;
    public float transitionDelay = 0.5f;

    private float transitionSpeed;
    public bool isHovered;
    private Vector3 startScale;
    private RectTransform m_RectTransform;
        
    void Start() {
        m_RectTransform = GetComponent<RectTransform>();
        isHovered = false;
        startScale = m_RectTransform.localScale;
        transitionSpeed = hoveredSizeOffset / transitionDelay;
    }

    void Update() {
        /*if(isHovered){
            if(m_RectTransform.localScale != (startScale + new Vector3(hoveredSizeOffset, hoveredSizeOffset, hoveredSizeOffset))){
                Vector3 scale = m_RectTransform.localScale;
                scale.x = scale.x + (transitionSpeed*Time.deltaTime);
                if(scale.x > (scale + new Vector2(hoveredSizeOffset, 0)).x)
                    scale.x = (scale + new Vector2(hoveredSizeOffset, 0)).x;
                m_RectTransform.anchoredPosition = position;
            }
        }else{
            if(m_RectTransform.anchoredPosition != startPosX){
                
                Vector2 position = m_RectTransform.anchoredPosition;
                position.x = position.x - (transitionSpeed*Time.deltaTime);
                if(position.x < startPosX.x)
                    position.x = startPosX.x;
                m_RectTransform.anchoredPosition = position;
            }
        }*/
           
    }

    public void mouseEnter(){
        isHovered = true;
    }

    public void mouseExit(){
        isHovered = false;
    }

    void OnDisable()
    {
        isHovered = false;
    }
}