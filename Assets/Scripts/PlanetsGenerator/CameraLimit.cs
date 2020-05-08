using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class CameraLimit : MonoBehaviour
{
    public bool reloadCameraSize = false;
    public float bias = 0.5f;
    PolygonCollider2D limits;
    // Start is called before the first frame update
    void Start()
    {
        reloadCameraSize = true;
        limits = GetComponent<PolygonCollider2D>();
    }

    Vector2 CameraSize(){
        float halfFieldOfView = Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad;
        float height = (Mathf.Abs(Camera.main.transform.position.z)) * Mathf.Tan(halfFieldOfView) * 2.0f;
        float width = Camera.main.aspect * height;

        return new Vector2(width, height);
    }

    float heightZone(){
        float heightZone =  Mathf.Abs(PlayerBorder.size.y - CameraSize().y);
        return heightZone;
    }

    float widthZone(){
        float widthZone =  Mathf.Abs(PlayerBorder.size.x - CameraSize().x);
        return widthZone;
    }

    void UpdateCameraSize(){
        List<Vector2> allPoints = new List<Vector2>();
        allPoints.Add(new Vector2(-widthZone(), heightZone()));
        allPoints.Add(new Vector2(widthZone(), heightZone()));
        allPoints.Add(new Vector2(widthZone(), -heightZone()));
        allPoints.Add(new Vector2(-widthZone(), -heightZone()));

        limits.SetPath(0, allPoints);
    }

    // Update is called once per frame
    void Update()
    {
        if(reloadCameraSize){
            reloadCameraSize = false;
            UpdateCameraSize();
        }
    }
}
