using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public GameObject top;
    public GameObject bot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOpacity(float opacity){
        var botRenderer = bot.GetComponent<MeshRenderer>();
        var topRenderer = top.GetComponent<MeshRenderer>();

        for(int i = 0; i < botRenderer.materials.Length; i++){
            var colorB = botRenderer.materials[i].GetColor("_BaseColor");
            botRenderer.materials[i].SetColor("_BaseColor", new Color(colorB.r, colorB.g, colorB.b, opacity));
        }

        for (int i = 0; i < topRenderer.materials.Length; i++)
        {
            var colorB = topRenderer.materials[i].GetColor("_BaseColor");
            topRenderer.materials[i].SetColor("_BaseColor", new Color(colorB.r, colorB.g, colorB.b, opacity));
        }
    }
}
