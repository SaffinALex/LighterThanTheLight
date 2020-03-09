using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIEventManager : MonoBehaviour
{
    public void TriggerPlayerHealthChange(int health, int maxHealth){
        GameObject healthGO = GameObject.Find("HealthBar");
        HealthBarControl healthBar = healthGO.GetComponent<HealthBarControl>();
        healthBar.setHealth(health);
        healthBar.setHealthPercent(health,maxHealth);
    }
}
