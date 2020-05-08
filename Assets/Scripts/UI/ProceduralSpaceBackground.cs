using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpaceBackground : MonoBehaviour
{
    private Camera cam;
    public List<GameObject> props;

    public float minDepth = 0;
    public float maxDepth = 10;

    public float margin = 1;

    public int maxProps;

    public float FrontProps_MinLifetime;
    public float FrontProps_MaxLifetime;
    public float BackProps_MinLifetime;
    public float BackProps_MaxLifetime;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Mesh mesh = getGeneratedMesh(minDepth, maxDepth);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        //ResetMeshSize();
        PopulateRandom();
    }
    public void PopulateRandom(){
        for(int i = 0; i<maxProps; i++){
            int randomIndex = Random.Range(0, props.Count);
            placeAtRandomPos(Instantiate(props[randomIndex]));
            
        }
    }

    private void placeAtRandomPos(GameObject newProp){
        float depth = Random.Range(minDepth,maxDepth);

        float halfFieldOfView = Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad;
        float cameraHeight = ((Mathf.Abs(Camera.main.transform.position.z) + depth + transform.position.z) * Mathf.Tan(halfFieldOfView));
        float cameraWidth = (Camera.main.aspect * cameraHeight) + (2*margin/3);
        cameraHeight += margin;

        float randomHeight = Random.Range(-cameraHeight, cameraHeight);
        float randomWidth = Random.Range(-cameraWidth,cameraWidth);

        newProp.transform.parent = transform.parent.transform.Find("Props");
        Vector3 scaleMult = new Vector3(1/transform.parent.transform.Find("Props").transform.localScale.x, 1/transform.parent.transform.Find("Props").transform.localScale.y, 1/transform.parent.transform.Find("Props").transform.localScale.z); 
        newProp.transform.localPosition = new Vector3(randomWidth*scaleMult.x, randomHeight*scaleMult.y, depth*scaleMult.z);
        newProp.GetComponent<BackgroundPropScript>().setDescentSpeed(getPropRandomDepthScaledSpeed(depth,cameraHeight));
    }

    private void placeAtRandomTopPos(GameObject newProp){
        float depth = Random.Range(minDepth,maxDepth);

        float halfFieldOfView = Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad;
        float cameraHeight = ((Mathf.Abs(Camera.main.transform.position.z) + depth + transform.position.z) * Mathf.Tan(halfFieldOfView));
        float cameraWidth = (Camera.main.aspect * cameraHeight) + (2*margin/3);

        float randomHeight = Random.Range((cameraHeight+margin-0.5f), (cameraHeight+margin));
        float randomWidth = Random.Range(-cameraWidth,cameraWidth);

        newProp.transform.parent = transform.parent.transform.Find("Props");
        Vector3 scaleMult = new Vector3(1/transform.parent.transform.Find("Props").transform.localScale.x, 1/transform.parent.transform.Find("Props").transform.localScale.y, 1/transform.parent.transform.Find("Props").transform.localScale.z); 
        newProp.transform.localPosition = new Vector3(randomWidth*scaleMult.x, randomHeight*scaleMult.y, depth*scaleMult.z);
        newProp.GetComponent<BackgroundPropScript>().setDescentSpeed(getPropRandomDepthScaledSpeed(depth,cameraHeight));
    }

    private void OnTriggerExit(Collider other) {
        placeAtRandomTopPos(other.gameObject);
    }

    private Mesh getGeneratedMesh(float frontDepth, float backDepth){
        Mesh mesh = new Mesh();

        float halfFieldOfView = Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad;

        float frontHeight = ((Mathf.Abs(Camera.main.transform.position.z) + frontDepth + transform.position.z) * Mathf.Tan(halfFieldOfView));
        float frontWidth = Camera.main.aspect * frontHeight;
        frontHeight+=margin;
        frontWidth+=(2*margin/3);

        float backHeight = ((Mathf.Abs(Camera.main.transform.position.z) + backDepth + transform.position.z) * Mathf.Tan(halfFieldOfView));
        float backWidth = Camera.main.aspect * backHeight;
        backHeight+=margin;
        backWidth+=(2*margin/3);

        Vector3[] vertices = new Vector3[8]
        {
            new Vector3(frontWidth, frontHeight, frontDepth), //0
            new Vector3(-frontWidth, frontHeight, frontDepth), //1
            new Vector3(frontWidth, -frontHeight, frontDepth), //2
            new Vector3(-frontWidth, -frontHeight, frontDepth), //3

            new Vector3(backWidth, backHeight, backDepth), //4
            new Vector3(-backWidth, backHeight, backDepth), //5
            new Vector3(backWidth, -backHeight, backDepth), //6
            new Vector3(-backWidth, -backHeight, backDepth), //7
        };
        mesh.vertices = vertices;

        int[] tris = new int[12*3]
        {
            //top lower left triangle
            0, 1, 5,
            //top upper right triangle
            5, 4, 0,

            //bottom lower left triangle
            3, 2, 6,
            //bottom upper right triangle
            6, 7, 3,

            //right lower left triangle
            2, 0, 4,
            //right upper right triangle
            4, 6, 2,

            //left lower left triangle
            1, 3, 7,
            //left upper right triangle
            7, 5, 1,

            //front lower left triangle
            2, 3, 1,
            //front upper right triangle
            1, 0, 2,

            //back lower left triangle
            7, 6, 4,
            //back upper right triangle
            4, 5, 7
        };
        mesh.triangles = tris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }

    public float getPropRandomDepthScaledSpeed(float propDepth, float distanceToTravel){
        float speed = 0;

        float depthScale = (propDepth-minDepth)/(maxDepth-minDepth);

        float minrange = FrontProps_MinLifetime + depthScale*(BackProps_MinLifetime - FrontProps_MinLifetime);
        float maxrange = FrontProps_MaxLifetime + depthScale*(BackProps_MaxLifetime - FrontProps_MaxLifetime);
        speed = distanceToTravel / Random.Range(minrange,maxrange);
        return speed;
    }
}
