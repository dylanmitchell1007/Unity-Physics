using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HooksLaw
{
    
    public class ParticleBehaviour : MonoBehaviour
    {
      
        [SerializeField]
        public Particle partical;

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = partical.Update(Time.fixedDeltaTime);
        }
    }
}