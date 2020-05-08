using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenTitleText : MonoBehaviour
{

    public float startPos = -339.5f;
    public float endPos = -75f;
    public float travelDelay = 2.0f;

    private float travelTimer;

    private bool goToStart;
    private bool isTravelling;

    private RectTransform m_RectTransform;
    // Start is called before the first frame update
    void Start(){
        m_RectTransform = GetComponent<RectTransform>();

        Vector3 position = m_RectTransform.anchoredPosition;
        position.y = startPos;
        m_RectTransform.anchoredPosition = position;

        goToStart = true;
        isTravelling = false;
    }

    // Update is called once per frame
    void Update(){
        if(isTravelling){
            travelTimer += Time.deltaTime;

            if(travelTimer<=travelDelay){
                Vector3 position = m_RectTransform.anchoredPosition;
                if(goToStart)
                    position.y = (endPos - (Mathf.Abs(Mathf.Abs(startPos) - Mathf.Abs(endPos)) * (travelTimer/ travelDelay)));
                else
                    position.y = (startPos + (Mathf.Abs(Mathf.Abs(startPos) - Mathf.Abs(endPos)) * (travelTimer/ travelDelay)));
                m_RectTransform.anchoredPosition = position;
            }
            else
                isTravelling = false;
        }
    }

    public void toggleTitleText(){
        isTravelling = true;
        goToStart = !goToStart;
        travelTimer = 0;
    }
}
