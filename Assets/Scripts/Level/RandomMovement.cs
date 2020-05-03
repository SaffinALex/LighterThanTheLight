using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float timerChangeMovement = 3f;
    public float maxRandPosition = 0.5f;
    private float timerMovement = 0f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private Vector3 nextPosition;
    private Quaternion nextRotation;

    protected Rigidbody rgbd;
    
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        SetRandomPosition();
        SetRandomRotation();
    }

    // Update is called once per frame
    void Update() {
        if(timerMovement < timerChangeMovement){
            timerMovement += Time.deltaTime;
            timerMovement = timerMovement >= timerChangeMovement ? timerChangeMovement : timerMovement;

            rgbd.velocity = Vector3.Lerp(lastPosition, nextPosition, Mathf.SmoothStep(0.0f, 1.0f, timerMovement / timerChangeMovement));
            transform.localRotation = Quaternion.Lerp(lastRotation, nextRotation, Mathf.SmoothStep(0.0f, 1.0f, timerMovement / timerChangeMovement));

            if(timerMovement >= timerChangeMovement){
                timerMovement = 0f;
                SetRandomPosition();
                SetRandomRotation();
            }
        }
    }

    void SetRandomPosition(){
        Vector3 randPosUnit = Random.insideUnitSphere * maxRandPosition;
        lastPosition = nextPosition;
        nextPosition = randPosUnit;
    }

    void SetRandomRotation(){
        float randX = Random.Range(0f, 0.025f);
        float randY = Random.Range(0f, 0.025f);
        float randZ = Random.Range(0f, 0.025f);
        float randW = Random.Range(0f, 0.025f);
        lastRotation = nextRotation;
        nextRotation = new Quaternion(initialRotation.x + randX, initialRotation.y + randY,initialRotation.z + randZ,initialRotation.w + randW);
    }
}
