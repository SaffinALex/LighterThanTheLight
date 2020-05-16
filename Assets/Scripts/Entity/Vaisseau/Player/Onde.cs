using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onde : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius;
    public int damage;
    public List<UpgradeOnde> upgradeOndes;
    public int nbrUpgradeMax;
    private float timer;
    public float timeBeforeExplosion;
    private Animator animator;
    void Start()
    {
        timer = 0;
        animator = GetComponent<Animator>();
        GetComponent<CircleCollider2D>().enabled = false;

        List<UpgradeOnde> bufferUpgrades = new List<UpgradeOnde>(new UpgradeOnde[nbrUpgradeMax]);
        foreach (UpgradeOnde wp in upgradeOndes)
            bufferUpgrades.Add(wp);
        upgradeOndes = bufferUpgrades;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer < timeBeforeExplosion){
            transform.position = new Vector3(transform.position.x, transform.position.y+1f*Time.deltaTime, transform.position.z);
        }
        else{
            if(timer >= timeBeforeExplosion + 2){
                StartCoroutine("WaveExploded");
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy") && animator.GetBool("isExploded")){
            col.GetComponent<EntitySpaceShipBehavior>().life-= damage;
        }
    }
    void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("Enemy") && animator.GetBool("isExploded")){
            col.GetComponent<EntitySpaceShipBehavior>().life-= damage;
        }
    }

    IEnumerator WaveExploded(){
        animator.SetBool("isExploded", true);
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }


}
