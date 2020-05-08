using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
[RequireComponent (typeof(TextMeshProUGUI))]
public class MenuButton : MonoBehaviour
{
    public float hoveredOffset = 2.0f;
    public float transitionDelay = 0.5f;

    private float transitionSpeed;
    public bool isHovered;
    private Vector2 startPosX;
    private RectTransform m_RectTransform;
        
    void Start() {
        m_RectTransform = GetComponent<RectTransform>();
        isHovered = false;
        startPosX = m_RectTransform.anchoredPosition;
        transitionSpeed = hoveredOffset / transitionDelay;
    }

    void Update() {
        if(isHovered){
            if(m_RectTransform.anchoredPosition != (startPosX + new Vector2(hoveredOffset,0))){
                Vector2 position = m_RectTransform.anchoredPosition;
                position.x = position.x + (transitionSpeed*Time.deltaTime);
                if(position.x > (startPosX + new Vector2(hoveredOffset,0)).x)
                    position.x = (startPosX + new Vector2(hoveredOffset,0)).x;
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