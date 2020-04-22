using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpaceBackground : MonoBehaviour
{
    private Camera cam;
    private float Top;
    private float width;
    public List<GameObject> props;

    public int maxProps;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ResetMeshSize();
        PopulateRandom();
    }
    public void PopulateRandom(){
        for(int i = 0; i<maxProps; i++){
            int randomIndex = Random.Range(0, props.Count);
            float randomHeight = Random.Range(-Top/2,Top/2);
            float randomWidth = Random.Range(-width/2,width/2);
            GameObject newProp = Instantiate(props[randomIndex]);
            newProp.transform.parent = transform.parent.transform.Find("Props");
            
            newProp.transform.localPosition = new Vector3(randomWidth, randomHeight, 0);
        }
    }
    public void ResetMeshSize(){
        Vector3 scale = transform.localScale;
        scale.y = (cam.orthographicSize);
        scale.x = (scale.y * cam.aspect);
        scale.y += 5;
        transform.localScale = scale;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
 
        Vector3[] baseVertices = mesh.vertices;
    
        var vertices = new Vector3[baseVertices.Length];
    
        for (var i=0;i<vertices.Length;i++){
            var vertex = baseVertices[i];
            vertex.x = vertex.x * scale.x;
            vertex.y = vertex.y * scale.y;
            vertex.z = vertex.z * scale.z;
    
            vertices[i] = vertex;
        }
    
        mesh.vertices = vertices;
      
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        Top = GetComponent<MeshRenderer>().bounds.extents.y;
        width = GetComponent<MeshRenderer>().bounds.extents.x;
    }

    private void OnTriggerExit(Collider other) {
        Destroy(other.gameObject);
        int randomIndex = Random.Range(0, props.Count);
        float randomWidth = Random.Range(-width/2,width/2);
        GameObject newProp = Instantiate(props[randomIndex]);
        newProp.transform.parent = transform.parent.transform.Find("Props");
        
        newProp.transform.localPosition = new Vector3(randomWidth, Top, 0);
    }
}
