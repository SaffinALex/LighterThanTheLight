using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownChargeLoader : MonoBehaviour
{
    //EditorParameters
    public GameObject chargesContainer;
    public GameObject sourceImage;
    public int numberOfCharges = 3;
    public float borderBarOffset = 10;
 
    //ScriptVariables
    public int currentNumberOfCharges;

    //LifeSpan Methods
    void Start()
    {
        loadCharges(this.numberOfCharges);
    }
    void Update()
    {
    }

    //ScriptMethods
    private void loadCharges(int numberOfCharges){
        float width = chargesContainer.GetComponent<RectTransform>().sizeDelta.x + ((borderBarOffset/2)*(currentNumberOfCharges+1)); //Recup largeur
        float height = chargesContainer.GetComponent<RectTransform>().sizeDelta.y; //Recup hauteur
        foreach (Transform child in chargesContainer.transform) { //Nettoie anciennes images de charge
            GameObject.Destroy(child.gameObject);
        }

        GameObject charge;
        for(int i = 0; i < numberOfCharges; i++){ //Pour chaque charges a dessiner
            charge = Instantiate(sourceImage);
            charge.transform.SetParent(chargesContainer.transform);

            charge.GetComponent<RectTransform>().sizeDelta = new Vector2((width/((float)numberOfCharges) ),height);
            Vector2 pos = new Vector2((((float) i) * (width/((float)numberOfCharges))),0);

            if(i != 0)
                pos.x -= (borderBarOffset/2)*((float)i);
            charge.GetComponent<RectTransform>().anchoredPosition  = pos;
            charge.GetComponent<RectTransform>().localScale  = new Vector3(1,1,1);
        }

    }

    //Public
    public void SetBaseCharges(int nbCharges){
        if(nbCharges != this.numberOfCharges){
            this.numberOfCharges = nbCharges;
            this.currentNumberOfCharges = numberOfCharges;
            loadCharges(numberOfCharges);
        }
    }

    public void GainCharge(){
        if(this.currentNumberOfCharges < this.numberOfCharges){
            currentNumberOfCharges++;
            this.transform.GetChild(currentNumberOfCharges-1).GetComponent<ChargeDisplayControl>().showCharge();
        }
    }

    public void ConsumeCharge(){
        if(this.currentNumberOfCharges > 0){
            this.transform.GetChild((currentNumberOfCharges-1)).GetComponent<ChargeDisplayControl>().hideCharge();
            currentNumberOfCharges--;
        }
    }
}
