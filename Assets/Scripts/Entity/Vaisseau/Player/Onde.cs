using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onde : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public float radius;
    public float timeBeforeExplosion;
    public float timeReload;

    public int initialRecharge;
    private int currentRecharge;

    protected float timerBeforeExplosion;
    protected float timerReload;

    public List<UpgradeOnde> upgradeOndes;
    public int nbrUpgradeMax;

    protected CircleCollider2D colliderOnde;

    protected float timeExplosion = 0.5f;
    protected float timerExplosion = 0f;

    [SerializeField] protected ParticleSystem awakeParticle;
    [SerializeField] protected ParticleSystem explodeParticle;


    public enum StatesOndes { sleep, awake, explode, ended };
    //Machine à état
    // sleep -> au repos
    // awake -> commence le chargement de l'onde
    // explode -> l'onde explose
    // ended -> l'onde a terminé son travail
    // Reviens à la state sleep lorsque le timer de reload est terminé
    protected StatesOndes stateOnde = StatesOndes.sleep;

    void Start()
    {
        currentRecharge = initialRecharge;
        timerReload = timeReload;
        timerBeforeExplosion = timeBeforeExplosion;
        colliderOnde = GetComponent<CircleCollider2D>();
        colliderOnde.enabled = false;
        colliderOnde.radius = radius;
        awakeParticle.Stop();
        explodeParticle.Stop();
        SetInfoParticle();
        LevelUIEventManager.GetLevelUI().InitOndeCdBar(initialRecharge, timeReload);
    }

    void SetInfoParticle(){
        var mainAwake = awakeParticle.main;
        mainAwake.duration = timeBeforeExplosion;
        mainAwake.loop = false;
        var shapeAwake = awakeParticle.shape;
        shapeAwake.radius = radius;
    }

    public void initialize()
    {
        for (int i = 0; i < upgradeOndes.Count; i++)
        {
            if (upgradeOndes[i] != null)
                upgradeOndes[i].StartUpgrade(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timerReload < timeReload){
            timerReload += Time.deltaTime;
            if(timerReload >= timeReload && currentRecharge < initialRecharge){
                currentRecharge++;
                if (currentRecharge > initialRecharge) currentRecharge = initialRecharge;
                timerReload = 0f;
            }
        }
        if(timerBeforeExplosion < timeBeforeExplosion) timerBeforeExplosion += Time.deltaTime;
        if (timerExplosion < timeExplosion) timerExplosion += Time.deltaTime;
        if(stateOnde == StatesOndes.explode && timerExplosion >= timeExplosion){
            stateOnde = StatesOndes.ended;
            colliderOnde.enabled = false;
        }
        if(stateOnde == StatesOndes.awake && timerBeforeExplosion >= timeBeforeExplosion){
            stateOnde = StatesOndes.explode;
            colliderOnde.enabled = true;
            explodeParticle.Play();
            App.sfx.PlayEffect("ShootOnde", 0.5f);
            timerExplosion = 0f;
        }
        if(currentRecharge > 0 && stateOnde == StatesOndes.ended){
            Debug.Log("GO SLEEP" + timerReload + " / " + timeReload + " / " + stateOnde);
            stateOnde = StatesOndes.sleep;
        }
    }

    public void RunOnde(){
        if(stateOnde == StatesOndes.sleep){
            currentRecharge--;
            if (timerReload >= timeReload) timerReload = 0;
            stateOnde = StatesOndes.awake;
            timerBeforeExplosion = 0f;
            awakeParticle.Play();
            explodeParticle.Stop();
            LevelUIEventManager.GetLevelUI().TriggerPlayerBomb();
            App.sfx.PlayEffect("ReloadOnde", 0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy")){
            col.GetComponent<EntitySpaceShipBehavior>().getDamage(damage);
        }
    }

    public void IncreaseDamage(float d)
    {
        damage += d;
    }

    public void IncreaseNbRecharge(int r)
    {
        initialRecharge += r;
    }

    public void DecreaseTimeReload(float t)
    {
        timeReload += t;
    }

    public void IncreaseRadius(float r)
    {
        radius += r;
    }
}
