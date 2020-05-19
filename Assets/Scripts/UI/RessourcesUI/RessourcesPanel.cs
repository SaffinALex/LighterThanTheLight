using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class RessourcesPanel : MonoBehaviour
{

    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textScore;

    [SerializeField] float money = 0f;
    [SerializeField] float score = 0f;

    public void SetMoney(float money){
        this.money = money;
        textMoney.text = this.money.ToString();
    }

    public void SetScore(float score){
        this.score = score;
        textScore.text = this.score.ToString();
    }

    public void Reset(){
        SetMoney(0);
        SetScore(0);
    }
}
