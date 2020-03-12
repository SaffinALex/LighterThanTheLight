using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public float backgroundSpeed = 2f;
    public float backgroundDepth = 50f;

    public float timeRotate = 2f;
    float timerRotate = 0f; 
    public float timeTransitionLeave = 2f;
    float timerTranstionLeave;
    public float timeTransitionEnter = 2f;
    float timerTranstionEnter;

    public GameObject playerPosition;
    public static GameObject player;

    public Transform pivotPoint;

    GameObject pivotObject;

    public static float speed;

    static BackgroundGenerator mainBackground;

    void Start()
    {
        BackgroundGenerator.speed = backgroundSpeed;
        transform.position = new Vector3(0f, 0f, backgroundDepth);
        player = playerPosition;

        timerRotate = timeRotate;
        timerTranstionEnter = timeTransitionEnter;
        timerTranstionLeave = timeTransitionLeave;

        mainBackground = this;
    }

    void FixedUpdate()
    {
        float transition = 1;
        //transform.position -= transform.up * Time.deltaTime * backgroundSpeed;
        if(timerTranstionEnter < timeTransitionEnter){
            timerTranstionEnter += Time.deltaTime;
            transition = 1 - timerTranstionEnter / timeTransitionEnter;

            timerTranstionLeave = 0f;
            timerRotate = 0f;
        } else if(timerRotate < timeRotate){
            timerRotate += Time.deltaTime;
            timerTranstionLeave = 0f;
            transition = 0f;
        } else if(timerTranstionLeave < timeTransitionLeave){
            timerTranstionLeave += Time.deltaTime;
            transition = timerTranstionLeave / timeTransitionLeave;
        }

        transform.RotateAround(pivotPoint.position, new Vector3(-1f, 0f, 0f), backgroundSpeed * Time.deltaTime * (1 - transition));
        transform.position = new Vector3(0f, transform.position.y - (backgroundSpeed * Time.deltaTime) * transition, transform.position.z);
    }

    public static void setNewPivot(Transform pivot){
        mainBackground.timerTranstionEnter = 0f;
        mainBackground.pivotPoint = pivot;
    }
}
