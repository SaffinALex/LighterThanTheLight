using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class CameraLimit : MonoBehaviour
{
    PolygonCollider2D limits;
    // Start is called before the first frame update
    void Start()
    {
        limits = GetComponent<PolygonCollider2D>();
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        Debug.Log(height + " / " + width);

        List<Vector2> allPoints = new List<Vector2>();
        allPoints.Add(new Vector2(-width / 2, height / 2));
        allPoints.Add(new Vector2(width / 2, height / 2));
        allPoints.Add(new Vector2(width / 2, -height / 2));
        allPoints.Add(new Vector2(-width / 2, -height / 2));

        limits.SetPath(0, allPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
