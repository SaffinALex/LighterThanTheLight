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
            transform.position = Vector3.Lerp(currentNode.transform.position, targetNode.transform.position, timerMove / timeMove);
            transform.position = new Vector3(transform.position.x, targetNode.transform.position.y + 1.0f, transform.position.z);
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

        if(movementY > 0){
            if (currentNode.childs[0] && currentNode.childs[0].IsAccessible()) ChangeNode(currentNode.childs[0]);
        }else if(movementY < 0){
            if (currentNode.childs[currentNode.childs.Count - 1] && currentNode.childs[currentNode.childs.Count - 1].IsAccessible()) ChangeNode(currentNode.childs[currentNode.childs.Count - 1]);
        }

        if(movementX > 0){
            int center = currentNode.childs.Count / 2;
            if(currentNode.childs.Count + 1 % 2 == 0 && currentNode.childs[center] && currentNode.childs[center].IsAccessible()) ChangeNode(currentNode.childs[center]);
        }else if(movementX < 0){
            ChangeNode(currentNode.parent);
        }
    }

    public void ChangeNode(Node target){
        Debug.Log(target != null);
        if(target != null && destinationAcheive){
            destinationAcheive = false;
            timerMove = 0f;
            targetNode = target;
        }
    }
}
