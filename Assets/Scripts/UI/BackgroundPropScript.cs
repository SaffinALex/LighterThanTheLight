using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPropScript : MonoBehaviour
{
    public Vector3 speed;
    public float minSize;
    public float maxSize;

    void Start() {
        float scale = Random.Range(minSize,maxSize);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Update()
    {
        if(checkMeshReadyToSet())
            setColliderMesh();

        transform.Translate(Time.deltaTime * speed);
    }

    private bool checkMeshReadyToSet(){
        return GetComponent<MeshFilter>().sharedMesh != null && GetComponent<MeshCollider>().sharedMesh == null;
    }

    private void setColliderMesh(){
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
    }
}
