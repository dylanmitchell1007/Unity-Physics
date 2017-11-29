using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour {
    [SerializeField]
    HooksLaw.Particle partical;
	// Use this for initialization
	void Start () {
        partical = new HooksLaw.Particle();
        partical.Addforce(Vector3.right);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = partical.Update(Time.fixedDeltaTime);
	}
}
