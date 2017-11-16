using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dylan
{
    public abstract class Agent : ScriptableObject
    {
        [SerializeField]
        protected float mass;
        [SerializeField]
        protected Vector3 velocity;
        [SerializeField]
        protected Vector3 acceleration;
        [SerializeField]
        protected Vector3 position;
        [SerializeField]
        protected Vector3 force;
        [SerializeField]
        protected float maxspeed;

        public abstract void Intialize(float Maxspeed);
        public abstract Vector3 Update_Agent(float deltaTime);
        public abstract bool Add_Force(float ms, Vector3 direction);
    }

   }