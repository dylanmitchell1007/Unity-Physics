using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dylan
{
    [CreateAssetMenu(menuName = "Agents/Boid")]
    public class Boid : Agent
    {
        public Vector3 Position
        {
            get { return position; }
        }
        public float Mass { get { return mass;}}
        public float Maxspeed { get { return maxspeed; } }
        public Vector3 Velocity { get { return velocity; } }
        public Vector3 Force { get { return force; } }
        
        public override void Intialize(float Maxspeed)
        {
            mass = 1.0f;
            position = Vector3.zero;
            velocity = Random.onUnitSphere;
            acceleration = Vector3.zero;
            force = Vector3.zero;
            maxspeed = Maxspeed;
        }

        public override bool Add_Force(float mag, Vector3 direction)
        {  
            var f = mag * direction;            
            force += f;
            return false;
        }

        public override Vector3 Update_Agent(float deltatime)
        {
            //force = mass * acceleration
            acceleration = force / mass;
            velocity += acceleration * deltatime;
            velocity = Vector3.ClampMagnitude(velocity, Maxspeed);
            position += velocity * deltatime;
            force = Vector3.zero;
            return position;
        }
    }

}