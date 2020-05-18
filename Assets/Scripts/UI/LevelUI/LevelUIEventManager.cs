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

    void Awake(){
        instance = this;
        healthBar = transform.Find("HealthBar").GetComponent<HealthBarControl>();
        dashCdBar = transform.Find("DashCooldownBar").GetComponent<CooldownBarControl>();
        bombCdBar = transform.Find("BombCooldownBar").GetComponent<CooldownBarControl>();
        bossWarn = GetComponentInChildren<BossWarning>();
    }

    //Dash nb charge max, temps cd
    public void InitDashCdBar(int nbMaxCharges, float tempsCd)
    {
        dashCdBar.setMaxCd(tempsCd);
        dashCdBar.setMaxNbCharges(nbMaxCharges);
        dashCdBar.FillCharges();
    }

    public void InitOndeCdBar(int nbMaxCharges, float tempsCd)
    {
        bombCdBar.setMaxCd(tempsCd);
        bombCdBar.setMaxNbCharges(nbMaxCharges);
        bombCdBar.FillCharges();
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
