using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenNext : MonoBehaviour
{
    bool startMenu = false;
    public float timeTransition = 5f;
    protected float currTimeTransition = 0f;
    public Transform Camera;
    Quaternion baseRotation;
    Quaternion finalRotation;
    Animator animator;

    bool infiniteRotateCam = false;
    // Start is called before the first frame update
    void Start()
    {
        startMenu = false;
        currTimeTransition = 0;
        baseRotation = Camera.rotation;
        finalRotation = new Quaternion(baseRotation.x - 20f, baseRotation.y, baseRotation.z, baseRotation.w);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !startMenu){
            startMenu = true;
            animator.SetBool("End", true);
            infiniteRotateCam = true;
            App.sfx.PlayEffect("SelectMenu");
            App.sfx.PlaySound("MainMenu", 5f);
        }

        if(currTimeTransition < timeTransition && startMenu){
            currTimeTransition += Time.deltaTime;
            Camera.rotation = Quaternion.Lerp(baseRotation, finalRotation, Mathf.SmoothStep(0,1,currTimeTransition / timeTransition));
            if(currTimeTransition >= timeTransition){
                // SceneManager.LoadScene(2);
            }
        }

        if(infiniteRotateCam){
            Camera.Rotate(0,0,Time.deltaTime * 1f,0);
        }
    }
}
