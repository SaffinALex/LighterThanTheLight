﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PlanetIco : MonoBehaviour
{
    public bool autoUpdate = true;
    
    [Range(0,5)]
    public int resolution = 1;
    public float radius = 1f;
    [Range(0, 100)]
    public int powerSeed = 0;
    [Range(0f,1.5f)]
    public float powerRandom = 0f;
    [Range(-200f, 200f)]
    public float speedRotation = 5f;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColourGenerator colourGenerator = new ColourGenerator();

    protected List<Vector3> vertList;
    protected MeshFilter meshFilter;

    private struct TriangleIndices
    {
        public int v1;
        public int v2;
        public int v3;
 
        public TriangleIndices(int v1, int v2, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GeneratePlanet();
    }

    void Initialize(){
        shapeGenerator.UpdateSettings(shapeSettings);
        colourGenerator.UpdateSettings(colourSettings);
        meshFilter = gameObject.GetComponent< MeshFilter >();
        if(meshFilter == null){
            gameObject.GetComponent<MeshRenderer>();
            meshFilter = gameObject.AddComponent< MeshFilter >();
            meshFilter.sharedMesh = new Mesh();
        }
        meshFilter.GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;
    }

    public void GeneratePlanet(){
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated(){
        if(autoUpdate){
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated(){
        if(autoUpdate){
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh(){
        Create();
        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours(){
        colourGenerator.UpdateColours();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0,Time.deltaTime * speedRotation, 0);
    }

    // return index of point in the middle of p1 and p2
    private int getMiddlePoint(int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache, float radius)
    {
        // first check if we have it already
        bool firstIsSmaller = p1 < p2;
        long smallerIndex = firstIsSmaller ? p1 : p2;
        long greaterIndex = firstIsSmaller ? p2 : p1;
        long key = (smallerIndex << 32) + greaterIndex;
 
        int ret;
        if (cache.TryGetValue(key, out ret))
        {
            return ret;
        }
 
        // not in cache, calculate it
        Vector3 point1 = vertices[p1];
        Vector3 point2 = vertices[p2];
        Vector3 middle = new Vector3
		(
            (point1.x + point2.x) / 2f, 
            (point1.y + point2.y) / 2f, 
            (point1.z + point2.z) / 2f
		);
 
        // add vertex makes sure point is on unit sphere
		int i = vertices.Count;
        vertices.Add( calculateNewPosition( middle.normalized ) ); 
 
        // store it, return index
        cache.Add(key, i);
 
        return i;
    }

    float getRadius(){
        return radius;
       // return (radius * (1 + Random.value * powerRandom));
    }

    Vector3 calculateNewPosition(Vector3 point){
        return shapeGenerator.CalculatePointOnPlanet(point);
    }

    public void Create()
    {
        Mesh mesh = meshFilter.sharedMesh;
        mesh.Clear();
 
        vertList = new List<Vector3>();
        Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int>();
        //int index = 0;
 
        // create 12 vertices of a icosahedron
        float t = (1f + Mathf.Sqrt(5f)) / 2f;
 
        vertList.Add(calculateNewPosition(new Vector3(-1f,  t,  0f).normalized));
        vertList.Add(calculateNewPosition(new Vector3( 1f,  t,  0f).normalized));
        vertList.Add(calculateNewPosition(new Vector3(-1f, -t,  0f).normalized));
        vertList.Add(calculateNewPosition(new Vector3( 1f, -t,  0f).normalized));
 
        vertList.Add(calculateNewPosition(new Vector3( 0f, -1f,  t).normalized));
        vertList.Add(calculateNewPosition(new Vector3( 0f,  1f,  t).normalized));
        vertList.Add(calculateNewPosition(new Vector3( 0f, -1f, -t).normalized));
        vertList.Add(calculateNewPosition(new Vector3( 0f,  1f, -t).normalized));
 
        vertList.Add(calculateNewPosition(new Vector3( t,  0f, -1f).normalized));
        vertList.Add(calculateNewPosition(new Vector3( t,  0f,  1f).normalized));
        vertList.Add(calculateNewPosition(new Vector3(-t,  0f, -1f).normalized));
        vertList.Add(calculateNewPosition(new Vector3(-t,  0f,  1f).normalized));
 
 
        // create 20 triangles of the icosahedron
        List<TriangleIndices> faces = new List<TriangleIndices>();
 
        // 5 faces around point 0
        faces.Add(new TriangleIndices(0, 11, 5));
        faces.Add(new TriangleIndices(0, 5, 1));
        faces.Add(new TriangleIndices(0, 1, 7));
        faces.Add(new TriangleIndices(0, 7, 10));
        faces.Add(new TriangleIndices(0, 10, 11));
 
        // 5 adjacent faces 
        faces.Add(new TriangleIndices(1, 5, 9));
        faces.Add(new TriangleIndices(5, 11, 4));
        faces.Add(new TriangleIndices(11, 10, 2));
        faces.Add(new TriangleIndices(10, 7, 6));
        faces.Add(new TriangleIndices(7, 1, 8));
 
        // 5 faces around point 3
        faces.Add(new TriangleIndices(3, 9, 4));
        faces.Add(new TriangleIndices(3, 4, 2));
        faces.Add(new TriangleIndices(3, 2, 6));
        faces.Add(new TriangleIndices(3, 6, 8));
        faces.Add(new TriangleIndices(3, 8, 9));
 
        // 5 adjacent faces 
        faces.Add(new TriangleIndices(4, 9, 5));
        faces.Add(new TriangleIndices(2, 4, 11));
        faces.Add(new TriangleIndices(6, 2, 10));
        faces.Add(new TriangleIndices(8, 6, 7));
        faces.Add(new TriangleIndices(9, 8, 1));
 
 
        // refine triangles
        for (int i = 0; i < resolution; i++)
        {
            List<TriangleIndices> faces2 = new List<TriangleIndices>();
            foreach (var tri in faces)
            {
                // replace triangle by 4 triangles
                int a = getMiddlePoint(tri.v1, tri.v2, ref vertList, ref middlePointIndexCache, getRadius());
                int b = getMiddlePoint(tri.v2, tri.v3, ref vertList, ref middlePointIndexCache, getRadius());
                int c = getMiddlePoint(tri.v3, tri.v1, ref vertList, ref middlePointIndexCache, getRadius());
 
                faces2.Add(new TriangleIndices(tri.v1, a, c));
                faces2.Add(new TriangleIndices(tri.v2, b, a));
                faces2.Add(new TriangleIndices(tri.v3, c, b));
                faces2.Add(new TriangleIndices(a, b, c));
            }
            faces = faces2;
        }
 
        mesh.vertices = vertList.ToArray();
 
        List< int > triList = new List<int>();
        for( int i = 0; i < faces.Count; i++ )
        {
            triList.Add( faces[i].v1 );
            triList.Add( faces[i].v2 );
            triList.Add( faces[i].v3 );
        }		
        mesh.triangles = triList.ToArray();

        mesh.uv = new Vector2[ mesh.vertices.Length ];
 
        Vector3[] normales = new Vector3[ vertList.Count];
        for( int i = 0; i < normales.Length; i++ )
            normales[i] = vertList[i].normalized;
 
 
        mesh.normals = normales;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.Optimize();
        mesh = meshFilter.sharedMesh;

        //Low poly
        Vector3[] oldVerts = mesh.vertices;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = new Vector3[triangles.Length];
        for (int i = 0; i < triangles.Length; i++) {
            vertices[i] = oldVerts[triangles[i]];
            triangles[i] = i;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
