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
            mass = 1;
        }

        public Particle(Vector3 p, Vector3 v, float m)
        {
            position = p;
            velocity = v;
            force =
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


        // Update is called once per frame
        public Vector3 Update(float deltatime)
        {
            acceleration = force / mass;
            velocity += acceleration * deltatime;
            position += velocity;
            return position;
        }
        public void Addforce(Vector3 f)
        {
            force += f;
        }
    }

    public class SpringDamper
    {
        Particle m_body1;
        Particle m_body2;

        Vector3 m_contact1;
        Vector3 m_contact2;

        float _Ks;
        float _Lo;
        float m_damping;

        public SpringDamper()
        {
           
        }
     
        public SpringDamper(Particle p1, Particle p2, float springConstant, float restLength)
        {
            m_body1 = p1;
            m_body2 = p2;
            _Ks = springConstant;
            _Lo = restLength;
        }
        public void fixedupdate(float deltatime)
        {
            Vector3 distance = m_body1.Position - m_body2.Position;
            float length = distance.magnitude;

            Vector3 relativeVelocity = m_body2.Velocity - m_body1.Velocity;
            Vector3 force = distance * _Ks * (_Lo - length) - m_damping * relativeVelocity;


            m_body1.Addforce(-force);
            m_body2.Addforce(force);
            m_body1.Update(deltatime);
            m_body2.Update(deltatime);
        }
    }
}