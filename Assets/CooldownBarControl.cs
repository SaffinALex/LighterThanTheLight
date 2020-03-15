using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBarControl : MonoBehaviour
{
    public int nbCharges = 1;
    public int currentNbCharges;
    public float cooldownDuration = 5;
    public float currentCooldownDuration = 5; 
    private CooldownChargeLoader chargesBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentNbCharges = nbCharges;
        currentCooldownDuration = cooldownDuration;
        chargesBar = this.transform.Find("ChargesContainer").GetComponent<CooldownChargeLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
//        print(currentNbCharges + "/" + nbCharges);
        if(currentNbCharges < nbCharges){
            currentCooldownDuration += Time.deltaTime;
            if(currentCooldownDuration >= cooldownDuration){
                print("yo");
                gainCharge();
                currentCooldownDuration = 0;
            }
        }
        else
            currentCooldownDuration = cooldownDuration;

        this.transform.Find("Slider").GetComponent<Slider>().value = currentCooldownDuration/cooldownDuration;
    }

    public void consumeCharge(){
        if(currentNbCharges>0){
            currentNbCharges--;
            if(currentCooldownDuration >= cooldownDuration)
                currentCooldownDuration = 0;
            chargesBar.ConsumeCharge();
        }
    }

    public void gainCharge(){
        if(currentNbCharges<nbCharges){
            currentNbCharges++;
            chargesBar.GainCharge();
        }
    }

    public void FillCharges(){
        currentNbCharges = nbCharges;
    }

    public void setMaxNbCharges(int MaxNbCharges){
        nbCharges = MaxNbCharges;
        this.transform.Find("ChargesContainer").GetComponent<CooldownChargeLoader>().SetBaseCharges(nbCharges);
    }

    public void setMaxCd(float cdMax){
        cooldownDuration = cdMax;
        currentCooldownDuration = cooldownDuration;
    }
}
