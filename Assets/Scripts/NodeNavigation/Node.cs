using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    
    /** STATIC VARIABLES **/
        //Nombre total de noeud dans la scène
        public static int TotalNode = 0;
        //Permet de savoir la distance physique entre deux nodes
        public static float distanceNodes = 10.0f;
        //GameObject permettant de regrouper tout le visuel
        public static GameObject CONSTRAINTS_DESIGN; //Permet d'avoir un objet regroupant toutes les designs contraintes (les cubes qui relient les nodes)
        public static GameObject NODES_CHILDS; //Permet d'avoir un objet regroupant toutes les designs contraintes (les cubes qui relient les nodes)


    /** VARIABLE INFORMATION **/
    //Représente l'accessibilité d'un noeud
    public bool accessible = false;
        //Rend le noeud pour toujours innaccessible
        public bool disabled = false;
        //Représente le fait d'avoir réussi le noeud
        public bool complete = false;
        //Représente le noeud parent
        public Node parent;
        //Représente les noeuds enfants
        public List<Node> childs;
        //Représente l'intérieur de la node
        public NodeElement nodeElement;
    
    /** VARIABLE ALEATOIRE POUR LA GENERATION DE NODE ELEMENT */
    static readonly float probaLevel = 0.98f;
    static readonly float probaShop = 0.02f;

    //Enregistre la position des contraintes sur les enfants
    protected List<GameObject> constraintsCubes = new List<GameObject>();
    public float scaleConstraints = 1.0f;

    //Design d'un verrou pour montrer qu'un niveau est innaccessible
    public Locker lockElement;

    public bool generateTree = false;
    protected bool alreadyGenerate = false;

    protected Animator animator;

    public float opacity = 1f;
    public float opacityConstraints = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        /** On récupère les gameobject en statique si ils n'ont pas été assignés **/
        if(CONSTRAINTS_DESIGN == null){ CONSTRAINTS_DESIGN = GameObject.Find("CONSTRAINTS_DESIGN"); }
        if (NODES_CHILDS == null) { NODES_CHILDS = GameObject.Find("NODES_CHILDS"); }

        /** Si un objet verrou n'a pas été généré alors on en créé un **/
        if(lockElement == null){
            lockElement = Instantiate(Resources.Load("Prefabs/UI_3D/Lock") as GameObject).GetComponent<Locker>();
            lockElement.transform.parent = transform;
            lockElement.transform.localPosition = new Vector3(0,4.0f,0);
        }

        //Permet de créer un node element
        GenerateNodeElement();
    }

    // Update is called once per frame
    void Update()
    {
        DrawConstraints();
        if (generateTree){
            generateTree = false;
            GenerateTree();
        }
    }

    void DrawConstraints(){
        if(constraintsCubes.Count != childs.Count ){
            // if(parent != null) animator.SetTrigger("ConstraintsApparition");
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
            scaleConstraints = 0;
        }

        for (int i = 0; i < childs.Count; i++) {
            constraintsCubes[i].transform.position = (transform.position + childs[i].transform.position) / 2;
            constraintsCubes[i].transform.LookAt(transform);
            constraintsCubes[i].transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(transform.position, childs[i].transform.position) - 1.0f) * scaleConstraints;
        }
        
        SetOpacity(opacity);
        lockElement.SetOpacity(!IsAccessible() ? opacity : 0.0f);

        SetOpacityConstraints(opacityConstraints);
    }

    public void SetAccessible(bool a){
        this.accessible = a;
    }

    public void EnterNode(){
        nodeElement.Begin();
    }

    void SetComplete(bool c){
        if(c && !this.complete){
            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].GenerateTree();
            }
        }
        this.complete = c;
        if(parent != null && c) parent.ChildComplete(this);
    }

    public void ChildComplete(Node childNode){
        for (int i = 0; i < childs.Count; i++) {
            if(childNode != childs[i]){
                childs[i].disabled = true;
                childs[i].deleteChilds();
            }
        }
    }

    public void deleteChilds(){
        animator.SetTrigger("ConstraintsDisparition");
        for (int i = 0; i < childs.Count; i++) {
            childs[i].deleteChilds();
            childs[i].DeleteNode();
        }
    }

    public void DeleteConstraints(){
        for (int i = 0; i < constraintsCubes.Count; i++) {
            Destroy(constraintsCubes[i]);
        }
        constraintsCubes.Clear();
        childs.Clear();
    }

    public void DeleteNode(){
        animator.SetTrigger("DIE");
    }

    public void Die(){
        Destroy(gameObject);
    }

    /**
     * Retourne vrai si le noeud est accessible
     */
    public bool IsAccessible(){
        return !disabled && (parent == null || parent.IsCompleted());
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

    void GenerateNodeElement(){
        if(nodeElement != null) GameObject.Destroy(nodeElement.gameObject); //On supprime le nodeElement précédent
        GameObject nodeElementObject = new GameObject("Node Element");
        nodeElementObject.transform.parent = transform;

        float randomElement = Random.Range(0f,1f);
        if(randomElement > probaLevel){
            nodeElement = nodeElementObject.AddComponent<NodeShop>();
        }else{
            nodeElement = nodeElementObject.AddComponent<NodeLevel>();
        }
        nodeElement.GetEvent().AddListener(CompleteNode);
    }

    /** Permet de compléter un node, c'est à dire que son nodeElement est terminé **/
    void CompleteNode(){
        SetComplete(true);
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

    public void GenerateTree(int depth = 10){
        if(depth <= 0 || alreadyGenerate) return;
        alreadyGenerate = true;
        int childsNumber = Random.Range(0, 2);
        for(int i = 0; i <= childsNumber; i++){
            GameObject nextChild = Instantiate(gameObject);
            nextChild.name = "NodeChild" + (++TotalNode);
            Node nextChildNode = nextChild.GetComponent<Node>();
            nextChildNode.parent = this;
            nextChild.transform.parent = NODES_CHILDS.transform;

            Vector3 AngleParent = parent != null ? (transform.position - parent.transform.position).normalized : new Vector3();
            float angle = Mathf.Atan2(AngleParent.z, AngleParent.x) * Mathf.Rad2Deg;
            nextChild.transform.position += new Vector3(5.0f, 1.0f, (childsNumber / 2f * distanceNodes) - i * distanceNodes).normalized * 5.0f;
            nextChild.transform.RotateAround(transform.position, Vector3.up, - angle);
            childs.Add(nextChildNode);
            nextChildNode.childs.Clear();
            nextChildNode.SetComplete(false);
        }
        animator.SetTrigger("ConstraintsApparition");
    }

    public static float CalculateAngle(Vector3 from, Vector3 to) {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.y;
    }
}
