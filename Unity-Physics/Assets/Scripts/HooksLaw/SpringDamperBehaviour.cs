using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HooksLaw
{
    public class SpringDamperBehaviour : MonoBehaviour
    {
        [SerializeField]
        SpringDamper springdamper;
        public GameObject Particle1;
        public GameObject Particle2;

        // Use this for initialization
        void Start()
        {
            Particle particalPos1 = new Particle(Particle1.transform.position, Vector3.zero,1);
            Particle particalPos2 = new Particle(Particle2.transform.position, Vector3.zero, 1);

            springdamper = new SpringDamper(particalPos1, particalPos2, 1, 1);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            springdamper.fixedupdate(Time.deltaTime);
        }
    }
}