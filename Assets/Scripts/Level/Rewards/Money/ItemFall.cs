using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFall : MonoBehaviour
{
    [SerializeField] protected float speedRotation;
    [SerializeField] protected float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        transform.Rotate(0, speedRotation * Time.deltaTime, 0);
    }
}
