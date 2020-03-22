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
        Debug.Log(limits);
        float height = 2.0f * - (Camera.main.transform.position.z / 2) * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * Camera.main.aspect;

        return new Vector2(width, height);
    }

    float heightZone(){
        float heightZone =  ZoneLimit.size.y - CameraSize().y;
        return (heightZone > 0 ? heightZone : 0) + bias;
    }

    float widthZone()
    {
        float widthZone =  ZoneLimit.size.x - CameraSize().x ;
        return (widthZone > 0 ? widthZone : 0) + bias;
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
