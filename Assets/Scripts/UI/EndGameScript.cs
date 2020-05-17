using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class EndGameScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestscoreText;
    public TMP_InputField nameInput;
    public TextMeshProUGUI context;

    public string no_name_input;
    public string name_input_rdy;

    private bool dataSent = false;

    private bool ready;
    void Start()
    {
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
        int score = App.playerManager.getScore();
        bool topScore = (ScoreManager.GetPositionScore(score) != -1);

        scoreText.text = score + " points";
        bestscoreText.gameObject.SetActive(topScore);
        nameInput.text = "AAA";
        ready = true;
        dataSent = false;
        Debug.Log(name_input_rdy);
        transform.Find("Content").Find("Text (TMP) (1)").GetComponent<Text>().text = name_input_rdy.ToString();
    }

    public void modifCheck()
    {
        nameInput.text = nameInput.text.ToUpper();
        if (nameInput.text.Length == nameInput.characterLimit)
        {
            ready = true;
            transform.Find("Content").Find("Text (TMP) (1)").GetComponent<Text>().text = name_input_rdy;
        }
        else
        {
            ready = false;
            transform.Find("Content").Find("Text (TMP) (1)").GetComponent<Text>().text = no_name_input;
        }
    }

    public void exitPanel()
    {
        if (!dataSent)
        {
            ScoreManager.SaveScore(nameInput.text, App.playerManager.getScore());
            dataSent = true;
        }
    }
}
