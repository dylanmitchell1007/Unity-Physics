using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HooksLaw
{

    public class AerodynamicsForce
    {


        public float Cd;
        public float p;
        public Vector3 windDirection;
        private List<Particle> Aparticles;


        AerodynamicsForce()
        {
            Cd = 1.0f;
            p = 1.0f;
            Aparticles = new List<Particle>();
        }
        AerodynamicsForce(List<Particle> p)
        {

        }
        AerodynamicsForce(List<Particle> p, float dragCo, float airDensity)
        {

        }
        void CalculateForces()
        {
            var p1 = Aparticles[0].Position;
            var p2 = Aparticles[1].Position;
            var p3 = Aparticles[2].Position;

            var Ao = 0.5f * (Vector3.Cross((p2 - p1), (p3 - p1))).magnitude;

        }

        void Start()
        {

        }


        void Update()
        {

        }
    }
}