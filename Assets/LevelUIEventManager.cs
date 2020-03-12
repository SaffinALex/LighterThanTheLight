using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIEventManager : MonoBehaviour
{
    private HealthBarControl healthBar;
    private CooldownBarControl dashCdBar;
    private CooldownBarControl bombCdBar;

    void Start() {
        healthBar = transform.Find("HealthBar").GetComponent<HealthBarControl>();
        dashCdBar = transform.Find("DashCooldownBar").GetComponent<CooldownBarControl>();
        bombCdBar = transform.Find("BombCooldownBar").GetComponent<CooldownBarControl>();

        dashCdBar.setMaxCd(GameObject.Find("playerShip").GetComponent<Dash>().attenteDash);
        dashCdBar.setMaxNbCharges(1);
        dashCdBar.FillCharges();

        bombCdBar.setMaxCd(GameObject.Find("playerShip").GetComponent<Dash>().attenteDash);
        bombCdBar.setMaxNbCharges(1);
        bombCdBar.FillCharges();
    }

    public void TriggerPlayerHealthChange(int health, int maxHealth){
        healthBar.setHealth(health);
        healthBar.setHealthPercent(health,maxHealth);
    }

    public void TriggerPlayerDash(){
        dashCdBar.consumeCharge();
    }

    public void TriggerPlayerBomb()
    {
        bombCdBar.consumeCharge();
    }
}
