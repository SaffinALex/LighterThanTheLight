using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //Représente l'accessibilité d'un noeud
    public bool accessible = false;
    //Représente le fait d'avoir réussi le noeud
    public bool complete = false;
    //Représente le noeud parent
    public Node parent;
    //Représente les noeuds enfants
    public List<Node> childs;

    protected List<GameObject> constraintsCubes = new List<GameObject>();

    public static GameObject CONSTRAINTS_DESIGN; //Permet d'avoir un objet regroupant toutes les designs contraintes (les cubes qui relient les nodes)

    protected Locker lockElement;

    // Start is called before the first frame update
    void Start()
    {
        if(CONSTRAINTS_DESIGN == null){
            CONSTRAINTS_DESIGN = GameObject.Find("CONSTRAINTS_DESIGN");
        }
        lockElement = Instantiate(Resources.Load("Prefabs/UI_3D/Lock") as GameObject).GetComponent<Locker>();
        lockElement.transform.parent = transform;
        lockElement.transform.localPosition = new Vector3(0,4.0f,0);

        // gameObject.SetActive(IsVisible() || (parent != null && parent.IsVisible()) );
    }

    // Update is called once per frame
    void Update()
    {
        DrawConstraints();
    }

    void DrawConstraints(){
        if(constraintsCubes.Count != childs.Count ){
            //Delete before
            for (int i = 0; i < constraintsCubes.Count; i++)
            {
                Destroy(constraintsCubes[i]);
            }
            //Create
            for(int i = 0; i < childs.Count; i++){
                GameObject cubeConstraint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                constraintsCubes.Add(cubeConstraint);
                cubeConstraint.transform.parent = CONSTRAINTS_DESIGN.transform;
                cubeConstraint.GetComponent<MeshRenderer>().material = CONSTRAINTS_DESIGN.GetComponent<MeshRenderer>().material;
            }
        }

        for (int i = 0; i < childs.Count; i++) {
            constraintsCubes[i].transform.position = (transform.position + childs[i].transform.position) / 2;
            constraintsCubes[i].transform.LookAt(transform);
            constraintsCubes[i].transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(transform.position, childs[i].transform.position) - 1.0f);
        }
        SetOpacity(IsVisible() ? 1.0f : 0.0f);
        SetOpacityConstraints(IsAccessible() ? 1.0f : 0.0f);
        lockElement.SetOpacity(!IsAccessible() && IsVisible() ? 1.0f : 0.0f);
    }

    void SetAccessible(bool a){
        this.accessible = a;
    }

    void SetComplete(bool c){
        this.complete = c;
    }

    /**
     * Retourne vrai si le noeud est accessible
     */
    public bool IsAccessible(){
        return parent == null || parent.IsCompleted();
    }

    /**
     * Retourne vrai si le noeud a été complété faux sinon
     */
    public bool IsCompleted(){
        return this.complete;
    }

    /**
     * Permet de savoir si le noeud sera visible dans la scène
     */
    public bool IsVisible(){
        return parent == null || (parent.IsAccessible());
    }

    void SetOpacity(float opacity){
        var botRenderer = GetComponent<MeshRenderer>();
        for (int i = 0; i < botRenderer.materials.Length; i++){
            var color = botRenderer.materials[i].GetColor("_BaseColor");
            botRenderer.materials[i].SetColor("_BaseColor", new Color(color.r, color.g, color.b, opacity));
        }
    }

    void SetOpacityConstraints(float opacity){
        for (int c = 0; c < constraintsCubes.Count; c++)
        {
            var constraintsRenderer = constraintsCubes[c].GetComponent<MeshRenderer>();
            for (int i = 0; i < constraintsRenderer.materials.Length; i++)
            {
                var color = constraintsRenderer.materials[i].GetColor("_BaseColor");
                constraintsRenderer.materials[i].SetColor("_BaseColor", new Color(color.r, color.g, color.b, opacity));
            }
        }
    }
}
