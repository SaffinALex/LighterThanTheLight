using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    public float initialRecoveryDash;
    public float InitialDistance;
    public float initialSpeed;
    private float speed;
    private float distance;
    private float recoveryDash;

    public List<UpgradeDash> upgradeDashes;
    public int nbrUpgradeMax;
    private bool canDash;

    void Start()
    {
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int numberUpgradeCanAdd(){
        int cpt = 0;
        for(int i = 0; i<upgradeDashes.Count; i++){
            cpt+= upgradeDashes[i].getWeight();
        }
        return nbrUpgradeMax - cpt;
    }

    public float runDash(float axisX){
        StartCoroutine("IntervalleDash");
        if(axisX < 0)
            return -speed * distance;
        else
            return speed * distance;
    }

    IEnumerator IntervalleDash(){
        canDash = false;
        yield return new WaitForSeconds(recoveryDash);
        canDash = true;
    }

    public bool getCanDash(){
        return canDash;
    }

    public void addUpgradeDashes(UpgradeDash u){
        upgradeDashes.Add(u);
    }

    public float getDistance(){
        return distance;
    }
    public float getRecoveryDash(){
        return recoveryDash;
    }
    public float getSpeed(){
        return speed;
    }

    public void setSpeed(float s){
        speed = getSpeed() + s;
    }
    public void setDistance(float s){
        distance = getDistance() + s;
    }
    public void setRecoveryDash(float s){
        recoveryDash = getRecoveryDash() + s;
    }

    public void initialize(){
        speed = initialSpeed;
        recoveryDash = initialRecoveryDash;
        distance = InitialDistance;
        for(int i=0; i<upgradeDashes.Count; i++){
            upgradeDashes[i].StartUpgrade(this);
        }

    }

}
