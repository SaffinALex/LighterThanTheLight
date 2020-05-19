using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelScript : MonoBehaviour
{
    public GameObject textObject;
    public float TimeBeforeActivating;
    public float timer;
    private int currentTimeShown;
    private string textToShow;
    private bool activated;
    
    void Update()
    {
        if (timer < TimeBeforeActivating)
        {
            timer += Time.unscaledDeltaTime;
            if (timer >= TimeBeforeActivating)
                textObject.GetComponent<TextMeshProUGUI>().text = textToShow;
            else if (currentTimeShown != ((int)Mathf.Ceil(TimeBeforeActivating - timer)))
            {
                currentTimeShown = (int)Mathf.Ceil(TimeBeforeActivating - timer);
                textObject.GetComponent<TextMeshProUGUI>().text = currentTimeShown.ToString();
            }
        }
        else
            if (Input.anyKey && !activated)
            {
                GetComponent<Button>().onClick.Invoke();
                activated = true;
            }
    }

    public void initPanel()
    {
        int score = App.playerManager.getInventory().getScore();
        transform.Find("Content").Find("score").GetComponent<TextMeshProUGUI>().text = "Score : " + score.ToString();
        activated = false;
        textToShow = textObject.GetComponent<TextMeshProUGUI>().text;
        timer = 0;
        currentTimeShown = (int)TimeBeforeActivating;
        textObject.GetComponent<TextMeshProUGUI>().text = currentTimeShown.ToString();
    }
}
