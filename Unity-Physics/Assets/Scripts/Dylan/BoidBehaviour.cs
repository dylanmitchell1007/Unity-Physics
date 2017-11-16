using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dylan
{
    public class BoidBehaviour : AgentBehaviour
    {
        public void setBoid(Boid b)
        {
            agent = b;
        }

        public void LateUpdate()
        {
            transform.position = agent.Update_Agent(Time.deltaTime);
        }
    }
}