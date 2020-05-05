using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerShip : Ship
{
    public GameObject wave;
    public Dash dash;
    public int shieldLife;
    public int recoveryTime;
    public int waveDamage;
    public int waveNumber;
    public float shootRecovery;
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
    
    // Start is called before the first frame update
    void Start()
    {
        posx = 0;
        isInvincible = false;
        canShootWave = true;
        canShoot = true;
        for(int i=0; i<weapons.Count; i++){
            weapons[i].gameObject.GetComponent<WeaponPlayer>().Initialize();
        }
        for(int i=0; i<upgradeShip.Count; i++){
            upgradeShip[i].StartUpgrade(this);
            if(shieldLife > maxShieldLife) shieldLife = maxShieldLife;
        }

    }

    // Update is called once per frame
    void FixedUpdate(){
        if (life <= 0){
            Destroy(this.gameObject);
            GameObject.Find("PanelUI").GetComponent<PanelUIManager>().ToggleEndGamePanel();
        }
        //Ne pas sortir de l'écran
        Vector3 change = Vector3.zero;
        change.x = Input.GetAxis("Horizontal");
        change.y = Input.GetAxis("Vertical");
        if(canShoot && Input.GetKey("v")){
            for(int i=0; i<weapons.Count; i++){
                weapons[i].gameObject.GetComponent<WeaponPlayer>().shoot(transform);
            }
            StartCoroutine("Shoot");
        }
        //Onde tire
        if(canShootWave && Input.GetKey("o") && waveNumber > 0){
            StartCoroutine("ShootWave");
            waveNumber --;
            GameObject waveBullet = Instantiate(wave, transform.position, Quaternion.identity);
            waveBullet.GetComponent<CircleCollider2D>().radius = waveRadius; 
            
        }
        //Le Dash se fait seulement sur X.
        if(dash.getCanDash() && Input.GetKeyDown("k") && !isInvincible && change.x != 0){
            Debug.Log("Dash");
            posx = dash.runDash(change.x);
            GameObject.Find("LevelUI").GetComponent<LevelUIEventManager>().TriggerPlayerDash();

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
                myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
            }
        }
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy") ){ 
            Debug.Log("touché");
            getDamage(10);
        }
        if(col.CompareTag("Upgrade") ){ 
            //Mettre l'Upgrade dans l'inventaire.
        }
    }
    private void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("Enemy") ){ 
            getDamage(10);
        }
    }

    public void getDamage(float damage){
        if(!isInvincible){
            if(shieldLife <= 0) life -= damage;
            else shieldLife -= 1;
            GameObject.Find("LevelUI").GetComponent<LevelUIEventManager>().TriggerPlayerHealthChange((int) life,100);
            //Insérer la vie du shield pour l'UI ici.
            StartCoroutine("InvincibiltyCount");
        }
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

    private IEnumerator Shoot(){
        canShoot = false;
        yield return new WaitForSeconds(shootRecovery);
        canShoot = true;
    }
    private IEnumerator ShootWave(){
        canShootWave = false;
        yield return new WaitForSeconds(5f);
        canShootWave = true;
    }
}
