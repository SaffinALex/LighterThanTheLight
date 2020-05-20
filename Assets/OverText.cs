using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverText : MonoBehaviour
{
    public Text textName;
    public Text textDesc;
    public Text textPrice;

    void Awake()
    {
        this.gameObject.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
    }

    public void show(string name, string desc, int price)
    {
        textName.text = name;
        textDesc.text = desc;
        textPrice.text = price.ToString();

        this.gameObject.SetActive(true);
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }

    public void setPos(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition = pos;
    }
}
