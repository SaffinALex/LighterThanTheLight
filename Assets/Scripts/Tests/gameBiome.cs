using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameBiome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world");

        biomeForet.LoadAllBiomeType();

        Biome m_monBiome = Biome.GenerateBiome(1);
        Debug.Log(m_monBiome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
