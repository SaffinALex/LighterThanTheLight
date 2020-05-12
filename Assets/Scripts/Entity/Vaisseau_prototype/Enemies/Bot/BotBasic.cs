using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBasic : EnemySpaceShip
{
    [SerializeField] protected float speed;
    [SerializeField] protected BotBulletPrototype bullet;
    protected Rigidbody2D rgbd2D;

    [SerializeField] protected float timerShoot;
    protected float currTimerShoot;

    const float timerTouch = 0.05f;
    protected float currTimerTouch = 0;

    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        currTimerShoot = 0;
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currTimerTouch += Time.deltaTime;
        if(currTimerTouch >= timerTouch){
            transform.GetChild(0).gameObject.SetActive(true);
        }
        // transform.Rotate(0,0,Time.deltaTime * 200);
        if(life <= 0){
            isAlive = false;
            Die();
        }
        Move();
        if(transform.position.y < - EnnemiesBorder.size.y / 2 + 1) Die();
    }

    protected override void Move(){
        rgbd2D.velocity = - transform.up * speed;
    }

    protected void Die(){
        Destroy(gameObject);
    }

    public override float InfligeDamage(float damages) {
        transform.GetChild(0).gameObject.SetActive(false);
        currTimerTouch = 0;
        return base.InfligeDamage(damages);
    }
}
