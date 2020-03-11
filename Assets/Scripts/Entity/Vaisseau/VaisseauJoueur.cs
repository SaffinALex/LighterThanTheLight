using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaisseauJoueur : Vaisseau
{
    public Onde onde;
    public Dash dash;
    public int vieShield;
    public int recoveryTime;

    //IsInvincible = Recovery Time pour éviter de se faire enchainer trop violemment.
    private bool isInvincible;
    private bool canShoot;
    public Animator animator;
    public GameObject bullet;
    public Rigidbody2D myRigidBody;
    private float posx;
    private float width;
    private Transform oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        posx = 0;
        isInvincible = false;
        canShoot = true;
//        width = camera.orthographicSize;
       // oldPosition.position = transform.position;

    }

    // Update is called once per frame
    void FixedUpdate(){
        if (life <= 0){
            Destroy(this.gameObject);
        }
        //Ne pas sortir de l'écran
       /* if(!(transform.position.x > -width && transform.position.x < width && transform.position.y < width && transform.position.y > -width)){
            transform.position = oldPosition.position;
        }*/
Vector3 change = Vector3.zero;
        change.x = Input.GetAxis("Horizontal");
        change.y = Input.GetAxis("Vertical");
        if(canShoot && Input.GetKey("v")){
            StartCoroutine("Shoot");
            for(int i=0; i<weapons.Count; i++){
                weapons[i].gameObject.GetComponent<Weapon>().shoot(transform);
            }
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
           // transform.position = new Vector3(transform.position.x + posx, transform.position.y, transform.position.z);
        }
        else{
            //Si il Dash il ne peut pas bouger et (attaquer?)
            if(animator.GetBool("isFlipLeft") || animator.GetBool("isFlipRight")){
              //  oldPosition.position = transform.position;
                myRigidBody.MovePosition(transform.position + new Vector3(posx,0f,0f)*speed * Time.deltaTime);
            }

            //Si il ne Dash pas il peut bouger
            else{
                //transform.position = new Vector3(transform.position.x + change.x*speed* Time.deltaTime, transform.position.y + change.y*speed* Time.deltaTime, transform.position.z);
                //oldPosition.position = transform.position;
                myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
            }
        }
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col){
            if(col.CompareTag("Enemy") ){ 
                if(!isInvincible){
                    life -= 5;
                    UnityEngine.EventSystems.EventSystem.current.GetComponent<LevelUIEventManager>().TriggerPlayerHealthChange((int) life,500);
                    StartCoroutine("InvincibiltyCount");
                }
            }
            if(col.CompareTag("BotBullet") ){ 
                if(!isInvincible){
                    life -= col.gameObject.GetComponent<BotBullet>().getDamage();
                    GameObject.Find("LevelUI").GetComponent<LevelUIEventManager>().TriggerPlayerHealthChange((int) life,500);
                    StartCoroutine("InvincibiltyCount");
                    Destroy(col.gameObject);
                }
            }
    }
    private void OnCollisionEnter2D(Collision2D col){
    	if(col.gameObject.CompareTag("BotBullet") ){ 
            if(!isInvincible){
                life -= col.gameObject.GetComponent<BotBullet>().getDamage();
                GameObject.Find("LevelUI").GetComponent<LevelUIEventManager>().TriggerPlayerHealthChange((int) life,500);
                StartCoroutine("InvincibiltyCount");
            }
            Destroy(col.gameObject);

    	}
        /*else if(col.gameObject.CompareTag("Obstacle") && !isInvincible){ 
            //life -= col.gameObject.getDmg();
            StartCoroutine("InvincibiltyCount");
    	}*/
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
        yield return new WaitForSeconds(0.25f);
        canShoot = true;
    }
}
