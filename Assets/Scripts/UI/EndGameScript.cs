using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class EndGameScript : MonoBehaviour
{
    protected UnityEvent eventEnd = new UnityEvent();
    public UnityEvent GetEventEnd() { return eventEnd; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestscoreText;
    public TMP_InputField nameInput;
    public TextMeshProUGUI context;

    public string no_name_input;
    public string name_input_rdy;

    private bool ready;
    void Start()
    {
        ready = false;
        /*textToShow = textObject.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(textObject.GetComponent<TextMeshProUGUI>().text);
        timer = 0;
        currentTimeShown = (int)TimeBeforeActivating;
        textObject.GetComponent<TextMeshProUGUI>().text = currentTimeShown.ToString();*/
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) && ready)
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }

    public void initPanel()
    {
        int score = App.playerManager.getInventory().getScore();
        bool topScore = (ScoreManager.GetPositionScore(score) != -1);

        scoreText.text = score + " points";
        bestscoreText.gameObject.SetActive(topScore);
        nameInput.text = "AAA";
        ready = true;
        context.text = name_input_rdy;
    }

    public void modifCheck()
    {
        nameInput.text = nameInput.text.ToUpper();
        if (nameInput.text.Length == nameInput.characterLimit)
        {
            ready = true;
            context.text = name_input_rdy;
        }
        else
        {
            ready = false;
            context.text = no_name_input;
        }
    }

    public void exitPanel()
    {
        ScoreManager.SaveScore(nameInput.text, App.playerManager.getInventory().getScore());
        eventEnd.Invoke();
    }
}
