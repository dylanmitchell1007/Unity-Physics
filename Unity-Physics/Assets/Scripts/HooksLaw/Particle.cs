using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HooksLaw
{
    [System.Serializable]
    public class Particle
    {
        public Particle()
        {
            position = Vector3.zero;
            velocity = Vector3.zero;
            acceleration = Vector3.zero;
            force = Vector3.zero;
            mass = 1;
        }

        public Particle(Vector3 p, Vector3 v,float m)
        {
            position = p;
            velocity = v;
            force = Vector3.zero;
            acceleration = Vector3.zero;
            mass = m;
        }
        [SerializeField]
        Vector3 position;
        public Vector3 Position { get { return position;}}
        Vector3 velocity;
        public Vector3 Velocity { get { return velocity; } }
        Vector3 acceleration;

        float mass;
        Vector3 force;
       public bool Locked = false;

        // Update is called once per frame
        public Vector3 Update(float deltatime)
        {
            if(Locked == true)
            {
                return Position;
            }
            
            acceleration = force / mass;
            velocity += acceleration * deltatime;
            position += velocity;
            force = Vector3.zero;
            return position;           
            //if locked return
           
        }
        public void Addforce(Vector3 f)
        {
            force += f;
        }
    }
    [System.Serializable]
    public class SpringDamper
    {
        Particle m_body1;

        Particle m_body2;
        public Particle particle_1 { get { return m_body1; } }
        public Particle particle_2 { get { return m_body2; } }



        public float _Ks;
        public float _Lo;
        public float _Kd;

        public SpringDamper()
        {
           
        }
     
        public SpringDamper(Particle p1, Particle p2, float springConstant, float restLength, float dampeningFactor)
        {
            m_body1 = p1;
            m_body2 = p2;
            _Ks = springConstant;
            _Kd = dampeningFactor;
            _Lo = Vector3.Distance(p1.Position, p2.Position);
            
        }
        public void CalculateForce()
        {
            Vector3 length = m_body2.Position - m_body1.Position;
            float L = length.magnitude;
            Vector3 E = length / L;


            var vOne = m_body1.Velocity;
            var vTwo = m_body2.Velocity;
            var v1 = Vector3.Dot(E, vOne);
            var v2 = Vector3.Dot(E, vTwo);

            float sSwitch = -_Ks * (_Lo - L) - _Kd * (v1 - v2);

            Vector3 force = sSwitch * E;
           
          


            m_body1.Addforce(force);
            m_body2.Addforce(-force);
        }
    }
}