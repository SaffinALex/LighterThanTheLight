using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class ZoneLimit : MonoBehaviour
{
    EdgeCollider2D limits;

    public Vector2 zoneSize = new Vector2(10,10);

    public static Vector2 size;
    // Start is called before the first frame update
    void Start()
    {
        limits = GetComponent<EdgeCollider2D>();

        float halfFieldOfView = Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad;
        float height = (2) * Mathf.Tan(halfFieldOfView) * 2.0f;
        float width = Camera.main.aspect * height;

        height *=1.5f;
        width *=1.5f;
        ZoneLimit.size.x = width;
        ZoneLimit.size.y = height;

        Vector2[] allPoints = new Vector2[5];
        allPoints[0] = (new Vector2(-width/2, height/2));
        allPoints[1] = (new Vector2(width/2, height/2));
        allPoints[2] = (new Vector2(width/2, -height/2));
        allPoints[3] = (new Vector2(-width/2, -height/2));
        allPoints[4] = (new Vector2(-width/2, height/2));

        limits.points = allPoints;
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
            other.gameObject.transform.GetComponent<Rigidbody>().velocity = new Vector2(0,0);
    }
}
