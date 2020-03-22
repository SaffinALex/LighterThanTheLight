using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    [SerializeField]
    private Rigidbody2D[] R2DPoints;

    public float speedScrolling;

    private Vector2 gizmosPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        for(float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(new Vector3(gizmosPosition.x, gizmosPosition.y, -7), 0.05f);
        }

        Gizmos.DrawLine(new Vector3(controlPoints[0].position.x, controlPoints[0].position.y, -7),
            new Vector3(controlPoints[1].position.x, controlPoints[1].position.y, -7));

        Gizmos.DrawLine(new Vector3(controlPoints[2].position.x, controlPoints[2].position.y, -7),
            new Vector3(controlPoints[3].position.x, controlPoints[3].position.y, -7));

        R2DPoints[0].velocity = new Vector3(0, -speedScrolling, 0);
        R2DPoints[1].velocity = new Vector3(0, -speedScrolling, 0);
        R2DPoints[2].velocity = new Vector3(0, -speedScrolling, 0);
        R2DPoints[3].velocity = new Vector3(0, -speedScrolling, 0);
    }

}
