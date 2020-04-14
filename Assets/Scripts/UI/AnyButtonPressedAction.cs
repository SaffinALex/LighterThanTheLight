using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class AnyButtonPressedAction : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
            GetComponent<Button>().onClick.Invoke();
    }
}
