using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scrolling : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int time;
    public GameObject level;
    private float timer;
    private bool bossIsActive;
    public GameObject boss;
    private GameObject bossActive;
    public Transform spawner;
    public Transform  spawnerBoss;
    private bool alertLaunched;
    void Start()
    {
        timer = 0;
        alertLaunched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < time){
            timer += Time.deltaTime;
        }
        if(timer > time - 10 && timer < time - 1 && !alertLaunched){
            //Lancer l'alerte Boss
            alertLaunched = true;
            Debug.Log("Alerte Boss");
            GameObject.Find("LevelUI").GetComponent<LevelUIEventManager>().TriggerBossWarning();
            level.GetComponent<EnemyManager>().enabled = false;
        }
        else if(timer > time - 2 && !bossIsActive){
            if(boss != null){ 
                //Le boss apparait
                bossActive = Instantiate(boss, new Vector3(0,0,0), Quaternion.identity);
                bossActive.transform.position = spawnerBoss.position;
                bossIsActive = true;
            }
        }
        if(timer >= time){
            if(bossActive == null){
                alertLaunched = false;
                Debug.Log("Terminer le level");
                StartCoroutine("FinishLevel");
            }
        }
 
    }

    IEnumerator FinishLevel(){
        yield return new WaitForSeconds(2f);
        GameObject.Find("PanelUI").GetComponent<PanelUIManager>().ToggleEndLevelPanel();
    }
}
