using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EntityHostile
{
    public ParticleSystem myParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        myParticleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
