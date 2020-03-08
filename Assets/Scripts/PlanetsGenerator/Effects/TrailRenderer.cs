using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRenderer : MonoBehaviour
{
    public Vector3 displacement;
    public Transform target;

    Rigidbody2D rgb2D;

    void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 vectorDirection = ((- transform.parent.position) - transform.localPosition) + target.position + displacement;
        Debug.Log(vectorDirection);
        rgb2D.velocity = vectorDirection * 2;
    }
}
