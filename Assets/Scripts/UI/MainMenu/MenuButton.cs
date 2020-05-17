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
        if(isHovered){
            if(m_RectTransform.localScale != (startScale + new Vector3(hoveredSizeOffset, hoveredSizeOffset, hoveredSizeOffset))){
                Vector3 scale = m_RectTransform.localScale;
                scale.x = scale.x + (transitionSpeed*Time.deltaTime);
                if(scale.x > (startScale + new Vector3(hoveredSizeOffset, hoveredSizeOffset, hoveredSizeOffset)).x)
                    scale.x = (startScale + new Vector3(hoveredSizeOffset, hoveredSizeOffset, hoveredSizeOffset)).x;
                scale.y = scale.y + (transitionSpeed * Time.deltaTime);
                if (scale.y > (startScale + new Vector3(hoveredSizeOffset, hoveredSizeOffset, hoveredSizeOffset)).y)
                    scale.y = (startScale + new Vector3(hoveredSizeOffset, hoveredSizeOffset, hoveredSizeOffset)).y;
                m_RectTransform.localScale = scale;
            }
        }else{
            if(m_RectTransform.localScale != startScale)
            {
                Vector3 scale = m_RectTransform.localScale;
                scale.x = scale.x - (transitionSpeed*Time.deltaTime);
                if(scale.x < startScale.x)
                    scale.x = startScale.x;
                scale.y = scale.y - (transitionSpeed * Time.deltaTime);
                if (scale.y < startScale.y)
                    scale.y = startScale.y;
                m_RectTransform.localScale = scale;
            }
        }
           
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