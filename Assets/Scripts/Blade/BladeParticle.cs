using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeParticle : MonoBehaviour {

    private ParticleSystem particle;

	// Use this for initialization
	void Start ()
    {
        this.particle = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        particle.Simulate(Time.unscaledDeltaTime, true, false);
    }
}
