using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HooksLaw
{


    /// <RestLength >
    /// Execution Order(Gravity, spring force)
    /// Bueler integration
    /// bending springs.
    /// </summary>
    public class ClothBehaviour : MonoBehaviour
    {
        public int Rows = 2;
        public int Columns = 2;
        public float offset = 1.0f;
        public float mass = 1.0f;
        public bool useGravity = false;
        public float springconstant = 1;
        public float dampingfactor = 1;
        public float restlength = 1;
        public GameObject model;
        public List<GameObject> GOS;
        public List<Particle> particles;
        public List<SpringDamper> dampers;
        private Vector3 gravity()
        {

            return new Vector3(0, -0.98f, 0);
        }
        // Use this for initialization
        void Start()
        {
            GOS = new List<GameObject>();
            particles = new List<Particle>();
            dampers = new List<SpringDamper>();
            #region Generate Grid
            for (int c = 0; c < Columns; c++)
            {
                for (int r = 0; r < Rows; r++)
                {
                    Particle p = new Particle(new Vector3(r * offset, c * offset, 0), new Vector3(0.0f, 0.0001f, 0), mass);
                    var go = new GameObject();
                    go.AddComponent<ParticleBehaviour>();
                    go.GetComponent<ParticleBehaviour>().partical = p;
                    var m = Instantiate(model, go.transform.position, go.transform.rotation);
                    m.transform.SetParent(go.transform);

                    particles.Add(p);
                    GOS.Add(go);
                }
            }
            #endregion
            ////Horizontal 
            int xIncrementer = 1;
            for (int i = 0; i < Rows * Columns; i++)
            {
                int second = i + 1;
                if (xIncrementer == Rows)
                {
                    xIncrementer = 1;
                    continue;
                }
                var p1 = particles[i];
                var p2 = particles[second];

                var springDamper = new SpringDamper(p1, p2, springconstant, dampingfactor, restlength);
                dampers.Add(springDamper);
                xIncrementer++;
            }
            //Vertical
            for (int i = 0; i < Rows * Columns; i++)
            {
                int second = i + Columns;
                if (i + Columns > (Rows * Columns) - 1)
                {
                    continue;
                }

                var p1 = particles[i];
                var p2 = particles[second];

                var springDamper = new SpringDamper(p1, p2, springconstant, dampingfactor, restlength);
                dampers.Add(springDamper);
            }


            ////Diagnally Up Right
            for (int i = 0; i < Rows * Columns; i++)
            {
                int second = (i + 1) + Columns;

                if (i + Columns > (Rows * Columns) - 1)
                {
                    continue;
                }
                if (xIncrementer == Rows)
                {
                    xIncrementer = 1;
                    continue;
                }
                var p1 = particles[i];
                var p2 = particles[second];
                var springDamper = new SpringDamper(p1, p2, springconstant, dampingfactor, restlength);
                dampers.Add(springDamper);
                xIncrementer++;
            }
            ////Diagnolly Up Left
            for (int i = 0; i < Rows * Columns - 1; i++)
            {
                int second = (i) + Columns;

                if (i + Columns > (Rows * Columns) - 1)
                {
                    continue;
                }
                if (xIncrementer == Rows)
                {
                    xIncrementer = 1;
                    continue;
                }
                var p1 = particles[i + 1];
                var p2 = particles[second];
                var springDamper = new SpringDamper(p1, p2, springconstant, dampingfactor, restlength);
                dampers.Add(springDamper);
                xIncrementer++;
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
        void FixedUpdate()
        {


            foreach (var Sdamper in dampers)
            {
                Debug.DrawLine(Sdamper.particle_1.Position, Sdamper.particle_2.Position, Color.green);
                Sdamper._Ks = springconstant;
                Sdamper._Kd = dampingfactor;
                Sdamper._Lo = restlength;

                if (useGravity == true)
                {
                    Sdamper.particle_1.Addforce(gravity());
                    Sdamper.particle_2.Addforce(gravity());
                }
                Sdamper.CalculateForce();

            }
        }
    }
    
}
    
    

           
    


