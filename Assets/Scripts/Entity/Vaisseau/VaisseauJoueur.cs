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
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        posx = 0;
        isInvincible = false;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 change = Vector3.zero;
        change.x = Input.GetAxis("Horizontal");
        change.y = Input.GetAxis("Vertical");
        if(canShoot && Input.GetKey("v")){
            StartCoroutine("Shoot");
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        //Le Dash se fait seulement sur X.
        if(dash.getCanDash() && Input.GetKeyDown("k") && change.x != 0){
            Debug.Log("Dash");
            posx = dash.runDash(change.x);
            if(change.x < 0)
                StartCoroutine("FlippingLeft");
            if(change.x > 0 )
             StartCoroutine("FlippingRight");
           // transform.position = new Vector3(transform.position.x + posx, transform.position.y, transform.position.z);
        }
        else{
            //Si il Dash il ne peut pas bouger et (attaquer?)
            if(animator.GetBool("isFlipLeft") || animator.GetBool("isFlipRight")){
                myRigidBody.MovePosition(transform.position + new Vector3(posx,0f,0f)*speed * Time.deltaTime);
            }

            //Si il ne Dash pas il peut bouger
            else{
                transform.position = new Vector3(transform.position.x + change.x*speed, transform.position.y + change.y*speed, transform.position.z);
                //GetComponent<Rigidbody>().MovePosition(transform.position + change * speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
    	if(col.gameObject.CompareTag("BotBullet") && !isInvincible){ 
            life -= col.gameObject.GetComponent<BotBullet>().getDamage();
            Destroy(col.gameObject);
            StartCoroutine("InvincibiltyCount");
    	}
        /*else if(col.gameObject.CompareTag("Obstacle") && !isInvincible){ 
            //life -= col.gameObject.getDmg();
            StartCoroutine("InvincibiltyCount");
    	}*/
    }

    private IEnumerator InvincibiltyCount(){
        isInvincible = true;
        yield return new WaitForSeconds(recoveryTime);
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
