/***
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerShipC : Ship
{
    //Onde du player
    public Onde wave;
    //Dash du player
    public Dash dash;

    public int initialShieldLife;
    private int shieldLife;

    public float initialRecoveryTime;
    private float recoveryTime;

    public float waveRecovery;

    //IsInvincible = Recovery Time pour éviter de se faire enchainer trop violemment.
    private bool isInvincible;
    public Animator animator;
    public GameObject bullet;
    public Rigidbody2D myRigidBody;
    private float posx;
    private int totalLife;

    //Define the time to achieve the wanted direction
    public float acceleration = 0.2f;

    //Represent the timer to achieve
    float timerChangeVelocity = 0.0f;
    bool needChangeVelocity = false;
    Vector2 lastVelocity;
    Vector2 lastWantedVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        posx = 0;
        setLife(initialLife);
        shieldLife = initialShieldLife;
        recoveryTime = initialRecoveryTime;
        isInvincible = false;
        dash.initialize();
        wave = Instantiate(wave);
        for(int i=0; i<weapons.Count; i++){
            weapons[i] = Instantiate(weapons[i]);
            weapons[i].transform.parent = transform;
            weapons[i].transform.position = Vector3.zero;
            weapons[i].Initialize();
        }
        for(int i=0; i<upgradeShip.Count; i++){
            upgradeShip[i].StartUpgrade(this);
            if(shieldLife > maxShieldLife) shieldLife = maxShieldLife;
        }
        totalLife = (int)getLife();

        LevelUIEventManager.GetLevelUI().TriggerPlayerHealthChange((int) getLife(),(int)totalLife,getShieldLife());
    }

    void UpdateUI(){

    }

    // Update is called once per frame
    void FixedUpdate(){
        if (getLife() <= 0){
            Destroy(this.gameObject);
            PanelUIManager.GetPanelUI().ToggleEndGamePanel();
        }
        //On update les timer des weapons
        for (int i = 0; i < weapons.Count; i++) {
            weapons[i].updateTimer();
            if (Input.GetKey("space")) {
                weapons[i].shoot(transform);
            }
        }
        
        //Ne pas sortir de l'écran
        Vector3 change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        
        //Onde tire
   if(Input.GetKey("o") && waveNumber > 0){
            StartCoroutine("ShootWave");
            waveNumber --;
            GameObject waveBullet = Instantiate(wave, transform.position, Quaternion.identity);
            waveBullet.GetComponent<CircleCollider2D>().radius = waveRadius; 
            LevelUIEventManager.GetLevelUI().TriggerPlayerBomb();
        }
        //Le Dash se fait seulement sur X.
        if(Input.GetKeyDown("k") && change.x != 0){
            Debug.Log("Dash");
            dash.runDash(change.x);
            LevelUIEventManager.GetLevelUI().TriggerPlayerDash();
        }
        else if(dash){
            //myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);

            timerChangeVelocity += Time.deltaTime;
            Vector2 wantedVelocity = new Vector2(change.x, change.y).normalized * speed;
            Vector2 finalVelocity = wantedVelocity;

            if (wantedVelocity != lastWantedVelocity) {
                timerChangeVelocity = 0.0f;
                needChangeVelocity = true;
                lastVelocity = myRigidBody.velocity;
                lastWantedVelocity = wantedVelocity;
            }

            if (needChangeVelocity) {
                timerChangeVelocity += Time.deltaTime;
                float percentage = timerChangeVelocity / acceleration;
                percentage = percentage > 1 ? 1 : percentage;
                finalVelocity = Vector2.Lerp(lastVelocity, wantedVelocity, timerChangeVelocity / acceleration);

                if(percentage == 1) needChangeVelocity = false;
            }


            myRigidBody.velocity = finalVelocity;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy") ){
            getDamage(10);
        }
        if(col.CompareTag("UpgradeDash") ){ this.gameObject.GetComponent<Inventory>().addUpgradeInventory(col.gameObject.GetComponent<UpgradeDash>()); }
        if(col.CompareTag("UpgradeWeapon") ){ this.gameObject.GetComponent<Inventory>().addUpgradeInventory(col.gameObject.GetComponent<UpgradeWeapon>()); }
        if(col.CompareTag("UpgradeShip") ){ this.gameObject.GetComponent<Inventory>().addUpgradeInventory(col.gameObject.GetComponent<UpgradeShip>()); }
        if(col.CompareTag("Weapon") ){ 
           this.gameObject.GetComponent<Inventory>().addWeaponInventory(col.gameObject.GetComponent<WeaponPlayer>());
           Destroy(col.gameObject);
        }
        if(col.CompareTag("Heart") ){ 
            setLife(getLife() + 5);
            if(getLife() > initialLife){
                setLife(initialLife);
            }
            LevelUIEventManager.GetLevelUI().TriggerPlayerHealthChange((int)getLife(), (int)totalLife, getShieldLife());
            Destroy(col.gameObject);
        }
        if(col.CompareTag("Money") ){ 
            this.gameObject.GetComponent<Inventory>().setMoney(this.gameObject.GetComponent<Inventory>().getMoney() + 5);
            Destroy(col.gameObject);
        }
        if(col.CompareTag("Shield") ){ 
            setShieldLife(getShieldLife() + 1 );
            if(shieldLife > maxShieldLife){
                shieldLife = maxShieldLife;
            }
            Destroy(col.gameObject);
        } 
    }
    private void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("Enemy") ){ 
            Debug.Log("touché");
            getDamage(10);
        }
    }

    public void getDamage(float damage){
        if(!isInvincible){
            if(shieldLife <= 0) setLife(getLife() - damage);
            else shieldLife -= 1;
            LevelUIEventManager.GetLevelUI().TriggerPlayerHealthChange((int) getLife(),(int)totalLife,getShieldLife());
            //Insérer la vie du shield pour l'UI ici.
            Debug.Log("touchéB");
            StartCoroutine("InvincibiltyCount");
        }
    }

    public int getShieldLife(){
        return shieldLife;
    }
    public void setShieldLife(int s){
        shieldLife = s;
    }

    public float getRecoveryTime(){
        return recoveryTime;
    }
    public void setRecoveryTime(float s){
        recoveryTime =  s;
    }

    private IEnumerator InvincibiltyCount(){
        isInvincible = true;
        animator.SetBool("isTouch", true);
        yield return new WaitForSeconds(recoveryTime);
        animator.SetBool("isTouch", false);
        isInvincible = false;
    }
}
*/