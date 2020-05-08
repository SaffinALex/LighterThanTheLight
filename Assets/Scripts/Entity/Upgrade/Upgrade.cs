using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string nom;
    public string description;
    public Sprite icone;
    public int price;
    public int weight;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getWeight(){
        return weight;
    }

    public int getPrice(){
        return price;
    }
}
