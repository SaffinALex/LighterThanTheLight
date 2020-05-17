using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent (typeof(LookAtConstraint))]
public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = Camera.main.transform;
        cs.weight = 1;
        GetComponent<LookAtConstraint>().AddSource(cs);
    }
}
