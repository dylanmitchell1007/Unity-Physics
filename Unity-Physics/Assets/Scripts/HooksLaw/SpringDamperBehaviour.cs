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
        private SpringDamper sd;
        public GameObject Cube1;
        public GameObject Sphere2;
        public ParticleBehaviour p1, p2;

        // Use this for initialization
        void Start()
        {

            Particle Partical1 = new Particle(new Vector3(0, 0, 0), new Vector3(-0.1f, 0, 0), 1);
            Particle Partical2 = new Particle(new Vector3(10, 0, 0), new Vector3(0.1f, 0, 0), 1);


            var send1 = new GameObject();
            send1.AddComponent<ParticleBehaviour>();
            send1.GetComponent<ParticleBehaviour>().partical = Partical1;




            var send2 = new GameObject();
            send2.AddComponent<ParticleBehaviour>();
            send2.GetComponent<ParticleBehaviour>().partical = Partical2;

            springdamper = new SpringDamper(Partical1, Partical2, 1, 1, 1);

        }
        public bool DistanceBreak()
        {
            if ((p2.partical.Position - p1.partical.Position).magnitude > 5 * sd._Lo)
                return true;
            return false;
        }
        public void SpringDot(Particle a, Particle b, float springK, float springD)
        {
            sd._Ks = springK;
            sd._Kd = springD;

            var dir = b.Position - a.Position;
            var l = dir.magnitude;
            var e = dir / l;

            var v1 = Vector3.Dot(e, a.Velocity);
            var v2 = Vector3.Dot(e, b.Velocity);

            var s = sd._Ks * (sd._Lo - l);
            var d = sd._Kd * (v1 - v2);

            var f = -s - d;
            var f1 = f * e;
            var f2 = -f1;

            a.Addforce(f1);
            b.Addforce(f2);
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