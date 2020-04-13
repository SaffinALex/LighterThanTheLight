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
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update() {
        ZoneLimit.size = zoneSize;
        float height = 2.0f * - ( Camera.main.transform.position.z / 2 )  * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * Camera.main.aspect;

        Vector2[] allPoints = new Vector2[5];
        allPoints[0] = (new Vector2(-zoneSize.x / 2 * 2, zoneSize.y / 2 * 2));
        allPoints[1] = (new Vector2(zoneSize.x / 2 * 2, zoneSize.y / 2 * 2));
        allPoints[2] = (new Vector2(zoneSize.x / 2 * 2, -zoneSize.y / 2 * 2));
        allPoints[3] = (new Vector2(-zoneSize.x / 2 * 2, -zoneSize.y / 2 * 2));
        allPoints[4] = (new Vector2(-zoneSize.x / 2 * 2, zoneSize.y / 2 * 2));

        limits.points = allPoints;
    }
}
