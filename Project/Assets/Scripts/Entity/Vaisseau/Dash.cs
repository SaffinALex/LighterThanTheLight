using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    public float attenteDash;
    public double distance;
    public double vitesse;

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
        //Si Dash && canDash
        
        //Dash et StartCoroutine("IntervalleDash");
    }

    IEnumerator IntervalleDash(){
        canDash = false;
        yield return new WaitForSeconds(attenteDash);
        canDash = true;
    }
}
