using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public Node currentNode;
    public Node targetNode;
    //Max Speed of the SpaceShip
    public float timeMove = 1.0f;
    protected float timerMove;

    protected bool destinationAcheive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timerMove < timeMove) timerMove += Time.deltaTime;
        if(timerMove > timeMove) timerMove = timeMove;

        if(!destinationAcheive && targetNode != null){
            transform.position = Vector3.Lerp(currentNode.transform.position, targetNode.transform.position, Mathf.SmoothStep(0.0f, 1.0f, timerMove / timeMove));
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
            if (timerMove != timeMove){
                transform.LookAt(new Vector3(targetNode.transform.position.x, targetNode.transform.position.y + 1.0f, targetNode.transform.position.z));
                transform.Rotate(0, -90f, 0);
            }
            if (timerMove == timeMove) {
                currentNode = targetNode;
                destinationAcheive = true;
                targetNode = null;
            }
        }

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        //Si la direction n'est pas nulle alors on se dirige vers la node la plus proche de cette direction pnysiquement
        if(movementX != 0 || movementY != 0){
            Vector3 pointDirection = new Vector3(Node.distanceNodes * movementX + transform.position.x, currentNode.transform.position.y, Node.distanceNodes * movementY + transform.position.z);
            ChangeNode(getNearNode(pointDirection));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            currentNode.SetComplete(true);
        }
    }

    public void ChangeNode(Node target){
        if(target != null && destinationAcheive && target.IsAccessible()){
            destinationAcheive = false;
            timerMove = 0f;
            targetNode = target;
        }
    }

    /**
     * Permet de récupérer le noeud le plus proche du point projeté
     */
    Node getNearNode(Vector3 point){
        float minDistance = currentNode.parent ? Vector3.Distance(point, currentNode.parent.transform.position) : -1;
        Node nearNode = currentNode.parent ? currentNode.parent : null;
        for(int i = 0; i < currentNode.childs.Count; i++){
            float distanceNode = Vector3.Distance(currentNode.childs[i].transform.position, point);
            if(distanceNode < minDistance || minDistance == -1){
                minDistance = distanceNode;
                nearNode = currentNode.childs[i];
            }
        }

        return nearNode;
    }
}
