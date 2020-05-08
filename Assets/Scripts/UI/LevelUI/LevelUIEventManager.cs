using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIEventManager : MonoBehaviour
{
    private HealthBarControl healthBar;
    private CooldownBarControl dashCdBar;
    private CooldownBarControl bombCdBar;
    private BossWarning bossWarn;

    private static LevelUIEventManager instance = null;

    void Start() {
        instance = this;
        healthBar = transform.Find("HealthBar").GetComponent<HealthBarControl>();
        dashCdBar = transform.Find("DashCooldownBar").GetComponent<CooldownBarControl>();
        bombCdBar = transform.Find("BombCooldownBar").GetComponent<CooldownBarControl>();
        bossWarn = GetComponentInChildren<BossWarning>();
        if(GameObject.Find("playerShip") != null){
            dashCdBar.setMaxCd(GameObject.Find("playerShip").GetComponent<Dash>().getRecoveryDash());
            dashCdBar.setMaxNbCharges(1);
            dashCdBar.FillCharges();

            bombCdBar.setMaxCd(GameObject.Find("playerShip").GetComponent<PlayerShip>().waveRecovery);
            bombCdBar.setMaxNbCharges(1);
            bombCdBar.FillCharges();
        }
    }

    public void TriggerPlayerHealthChange(int health, int maxHealth, int nbShields){
        healthBar.setHealth(health);
        healthBar.setShields(nbShields);
        healthBar.setHealthPercent(health,maxHealth);
    }

    public void TriggerPlayerDash(){
        dashCdBar.consumeCharge();
    }

    public void TriggerPlayerBomb(){
        bombCdBar.consumeCharge();
    }

    public void TriggerBossWarning(){
        bossWarn.show("Boss en Approche");
    }

    public void TriggerWarning(string text, float lifetimeDuration = -1, float showDelay = -1, float hideDelay = -1){
        bossWarn.show(text, lifetimeDuration, showDelay, hideDelay);
    }

    public static LevelUIEventManager GetLevelUI(){
        if(instance == null){
            Debug.LogError("Aucune instance de LevelUI présente dans la scene");
            return null;
        }
        else
            return instance;
    }
}
