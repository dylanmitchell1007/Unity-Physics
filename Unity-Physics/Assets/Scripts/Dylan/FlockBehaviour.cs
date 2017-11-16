using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dylan
{
    public class FlockBehaviour : MonoBehaviour
    {
        public List<Boid> boidlist;
        public float AlignmentCo = 1.0f;
        public float DispersionCo = 1.0f;
        public float CohesionCo = 1.0f;
        public float MaxForce = 5.0f;
        public float _distanceapart;
        public float _neighborDistance;
        public float tendtoCo = 1;
        public Vector3 GetCenter()
        {
            var center = Vector3.zero;
            foreach (var boid in boidlist)
            {
                center += boid.Position;

            }
            return center;
        }

        public Vector3 Dispersion(Boid bi, float threshhold)
        {
            Vector3 force = Vector3.zero;

            var neighbors = boidlist.FindAll(x => Vector3.Distance(x.Position, bi.Position) < _neighborDistance);

            foreach (var bj in neighbors)
            {
                if (bj != bi)
                {
                    var distance = (bj.Position - bi.Position).magnitude;
                    if (distance < threshhold)
                    {
                        force = force - (bj.Position - bi.Position);
                    }
                }
            }

            return force;
        }

        public Vector3 Cohesion(Boid bi)
        {

            Vector3 force = Vector3.zero;
            var neighbors = boidlist.FindAll(x => Vector3.Distance(x.Position, bi.Position) < _neighborDistance);

            foreach (var bj in neighbors)
            {
                if (bj != bi)
                {
                    force = force + bj.Position;
                }
            }
            if (boidlist.Count > 1)
                force = force / (boidlist.Count - 1);

            return (force - bi.Position);
        }


        public Vector3 Alignment(Boid bi)
        {
            Vector3 force = Vector3.zero;

            var neighbors = boidlist.FindAll(x => Vector3.Distance(x.Position, bi.Position) < _neighborDistance);

            foreach (var bj in neighbors)
            {
                if (bj != bi)
                    force = force + bj.Velocity;
            }

            if (boidlist.Count > 1)
                force = force / (boidlist.Count - 1);

            return (force - bi.Velocity);

        }

        public void Start()
        {
            boidlist = GameObject.FindObjectsOfType<Boid>().ToList();
        }
        public void Update()
        {
            boidlist = GameObject.FindObjectsOfType<Boid>().ToList();
            foreach (var boid in boidlist)
            {
                var vAlignment = Alignment(boid);
                var vDispersion = Dispersion(boid, _distanceapart);
                var vCohesion = Cohesion(boid);
                var allforce = AlignmentCo * vAlignment + DispersionCo * vDispersion + CohesionCo * vCohesion;
                allforce = Vector3.ClampMagnitude(allforce, MaxForce);
                var tendto = Vector3.zero - boid.Position;
                boid.Add_Force(tendtoCo, tendto.normalized);
                boid.Add_Force(allforce.magnitude, allforce.normalized);

            }

        }
       

    }
    }