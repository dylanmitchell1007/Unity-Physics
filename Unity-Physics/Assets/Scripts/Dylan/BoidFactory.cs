using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dylan
{
    public class BoidFactory : MonoBehaviour
    {

        public int count;
        public List<Agent> agents;
        public List<Boid>  boids;
        public List<AgentBehaviour> agentbehaviour;
        public List<BoidBehaviour> boidbehaviour;
        void Create()
        {
            var boid = new Boid();
            boidbehaviour = new List<BoidBehaviour>();

            for (int i = 0; i < count; i++)
            {
                var go = new GameObject();
                go.transform.SetParent(transform);
                go.name = string.Format("(0)  (1)", "Boids: ", 1);

                var behaviour = go.AddComponent<BoidBehaviour>();

                var agent = ScriptableObject.CreateInstance<Agent>();

                agents.Add(agent);
                agentbehaviour.Add(behaviour);
                behaviour.setBoid(boid);

            }
        }
        
        void Destroy()
        {
            foreach (var behaviour in boidbehaviour)
                DestroyImmediate(behaviour.gameObject);
            boids.Clear();
           boidbehaviour.Clear();
        }


    }

}