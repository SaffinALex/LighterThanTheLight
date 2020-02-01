using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    public float attenteDash;
    public float distance;
    public float vitesse;

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

    public float runDash(float axisX){
        StartCoroutine("IntervalleDash");
        return axisX * distance;
    }

    IEnumerator IntervalleDash(){
        canDash = false;
        yield return new WaitForSeconds(attenteDash);
        canDash = true;
    }

    public bool getCanDash(){
        return canDash;
    }
}
