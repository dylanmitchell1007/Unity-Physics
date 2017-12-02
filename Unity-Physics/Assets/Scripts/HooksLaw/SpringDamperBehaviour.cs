using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HooksLaw
{
    public class SpringDamperBehaviour : MonoBehaviour
    {
        [SerializeField]
        private SpringDamper springdamper;
        public float springConstant = 1.0f;
        public float dampeningFactor = 1.0f;
        public float restlength = 1.0f;

        public GameObject Cube1;
        public GameObject Sphere2;


        // Use this for initialization
        void Start()
        {            
            
            Particle Partical1 = new Particle(new Vector3(0,0,0), new Vector3(-0.1f, 0, 0), 1);
            Particle Partical2 = new Particle(new Vector3(10, 0, 0), new Vector3(0.1f, 0, 0), 1);
         

            var send1 = new GameObject();
            send1.AddComponent<ParticleBehaviour>();
            send1.GetComponent<ParticleBehaviour>().partical = Partical1;




            var send2 = new GameObject();
            send2.AddComponent<ParticleBehaviour>();
            send2.GetComponent<ParticleBehaviour>().partical = Partical2;

            springdamper = new SpringDamper(Partical1, Partical2, 1, 1, 1);

                }

        // Update is called once per frame
        void FixedUpdate()
        {
            springdamper._Ks = springConstant;
            springdamper._Kd = dampeningFactor;
            springdamper._Lo = restlength;

            springdamper.CalculateForce();
            
        }

        void LateUpdate()
        {
            Cube1.transform.position = springdamper.particle_1.Position;
            Sphere2.transform.position = springdamper.particle_2.Position;
        }
    }
}