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
        void OnEnable()
        {
            partical = new Particle(transform.position, Vector3.zero, 1);
        }

        public void UpdateParticle()
        {
            partical.Update(Time.deltaTime);
            transform.position = partical.Position;
        }
        private void LateUpdate()
        {
            transform.position = partical.Position;
        }
    }

}