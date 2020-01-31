using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaisseauJoueur : Vaisseau
{
    public Onde onde;
    public Dash dash;
    public int vieShield;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
