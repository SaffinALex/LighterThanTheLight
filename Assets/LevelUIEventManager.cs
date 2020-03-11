using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIEventManager : MonoBehaviour
{
    private HealthBarControl healthBar;
    private CooldownBarControl dashCdBar;

    void Start() {
        healthBar = transform.Find("HealthBar").GetComponent<HealthBarControl>();
        dashCdBar = transform.Find("DashCooldownBar").GetComponent<CooldownBarControl>();

        dashCdBar.setMaxCd(GameObject.Find("playerShip").GetComponent<Dash>().attenteDash);
        dashCdBar.setMaxNbCharges(1);
        dashCdBar.FillCharges();
    }

    public void TriggerPlayerHealthChange(int health, int maxHealth){
        healthBar.setHealth(health);
        healthBar.setHealthPercent(health,maxHealth);
    }

    public void TriggerPlayerDash(){
        dashCdBar.consumeCharge();
    }
}
