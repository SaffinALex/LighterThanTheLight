using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFall : MonoBehaviour
{
    [SerializeField] protected float speedRotation;
    [SerializeField] protected float speed;
    [SerializeField] protected float powerAttraction;

    GameObject player;

    void Start(){
        player = GameObject.Find("playerShip");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector2 wantedDirection = ((player.transform.position - transform.position).normalized * (1f / Vector2.Distance(player.transform.position, transform.position)) * powerAttraction) * Time.deltaTime;
            wantedDirection += Vector2.down * speed * Time.deltaTime;
            transform.position = transform.position + new Vector3(wantedDirection.x, wantedDirection.y, 0);
            transform.Rotate(0, speedRotation * Time.deltaTime, 0);
        }
    }
}
