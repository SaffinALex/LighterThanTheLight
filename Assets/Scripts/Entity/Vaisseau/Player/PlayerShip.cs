using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerShip : Ship
{
    public GameObject wave;
    public Dash dash;
    public int initialShieldLife;
    private int shieldLife;
    public float initialRecoveryTime;
    private float recoveryTime;
    public int waveDamage;
    public int waveNumber;
    public float waveRecovery;
    public float waveRadius;

    //IsInvincible = Recovery Time pour éviter de se faire enchainer trop violemment.
    private bool isInvincible;
    private bool canShoot;
    public Animator animator;
    public GameObject bullet;
    public Rigidbody2D myRigidBody;
    private float posx;
    private bool canShootWave;
    private float width;
    private Transform oldPosition;
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
        canShootWave = true;
        canShoot = true;
        dash.initialize();
        for(int i=0; i<weapons.Count; i++){
            weapons[i].Initialize();
        }
        for(int i=0; i<upgradeShip.Count; i++){
            upgradeShip[i].StartUpgrade(this);
            if(shieldLife > maxShieldLife) shieldLife = maxShieldLife;
        }
        totalLife = (int)getLife();

        LevelUIEventManager.GetLevelUI().TriggerPlayerHealthChange((int) getLife(),(int)totalLife,getShieldLife());
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (getLife() <= 0){
            Destroy(this.gameObject);
            PanelUIManager.GetPanelUI().ToggleEndGamePanel();
        }
        //On update les timer des weapons
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].updateTimer();
        }
        //Ne pas sortir de l'écran
        Vector3 change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetKey("v")){
            for(int i=0; i<weapons.Count; i++){
                weapons[i].shoot(transform);
            }
        }
        //Onde tire
   /*     if(canShootWave && Input.GetKey("o") && waveNumber > 0){
            StartCoroutine("ShootWave");
            waveNumber --;
            GameObject waveBullet = Instantiate(wave, transform.position, Quaternion.identity);
            waveBullet.GetComponent<CircleCollider2D>().radius = waveRadius; 
            LevelUIEventManager.GetLevelUI().TriggerPlayerBomb();
        } */
        //Le Dash se fait seulement sur X.
        if(dash.getCanDash() && Input.GetKeyDown("k") && !isInvincible && change.x != 0){
            Debug.Log("Dash");
            posx = dash.runDash(change.x);
            LevelUIEventManager.GetLevelUI().TriggerPlayerDash();

            if(change.x < 0)
                StartCoroutine("FlippingLeft");
            if(change.x > 0 )
                StartCoroutine("FlippingRight");
        }
        else{
            //Si il Dash il ne peut pas bouger et (attaquer?)
            if(animator.GetBool("isFlipLeft") || animator.GetBool("isFlipRight")){
                myRigidBody.MovePosition(transform.position + new Vector3(posx,0f,0f)*speed * Time.deltaTime);
            }

            //Si il ne Dash pas il peut bouger
            else{
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
        
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy") ){ 
            Debug.Log("touché");
            getDamage(10);
        }
        if(col.CompareTag("UpgradeDash") ){ 
           this.gameObject.GetComponent<Inventory>().addUpgradeInventory(col.gameObject.GetComponent<UpgradeDash>());
        }
        if(col.CompareTag("UpgradeWeapon") ){ 
           this.gameObject.GetComponent<Inventory>().addUpgradeInventory(col.gameObject.GetComponent<UpgradeWeapon>());
        }
        if(col.CompareTag("UpgradeShip") ){ 
           this.gameObject.GetComponent<Inventory>().addUpgradeInventory(col.gameObject.GetComponent<UpgradeShip>());
        }
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

    private IEnumerator FlippingRight(){
        animator.SetBool("isFlipRight", true);
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
        animator.SetBool("isFlipRight", false);
    }

    private IEnumerator FlippingLeft(){
        animator.SetBool("isFlipLeft", true);
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
        animator.SetBool("isFlipLeft", false);
    }

    private IEnumerator ShootWave(){
        canShootWave = false;
        yield return new WaitForSeconds(waveRecovery);
        canShootWave = true;
    }
}
