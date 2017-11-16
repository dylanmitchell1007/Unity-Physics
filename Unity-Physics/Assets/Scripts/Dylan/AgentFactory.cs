using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Dylan
{
    public class AgentFactory : MonoBehaviour
    {
        public int count;
        public GameObject AgentPrefab;
        private static List<Agent> AgentList = new List<Agent>();
        public List<GameObject> allobjects;
        private static List<Agent> Agents { get { return AgentList; } }
        
        public void Create()
        {
            
            for(int i = 0; i < count; i++)
            {
                var go = GameObject.Instantiate(AgentPrefab);
                go.name = string.Format("(0)  (1)", "Agent: ", AgentList.Count);
                var behaviour = go.AddComponent<BoidBehaviour>();
                var boid = ScriptableObject.CreateInstance<Boid>();
                boid.Intialize(10);
                behaviour.setBoid(boid);
                AgentList.Add(boid);

                AgentList.Add(boid);
                allobjects.Add(go);
            }         
        }
        public void Destroy()
        {
            var allagents = GameObject.FindObjectsOfType<AgentBehaviour>().ToList();
            var allboids = GameObject.FindObjectsOfType<Boid>().ToList();
            
            allagents.Clear();
            allboids.Clear();

            for(int i = 0; i< AgentList.Count; i++)
            {
                DestroyImmediate(allobjects[i]);
            }

            AgentList.Clear();
            allobjects.Clear();
        }
        public static List<Boid>Getboids()
        {
            var temp = new List<Boid>();
            foreach(Boid b in AgentList)
            {
                temp.Add(b);
            }
            return temp;
        }

        void Awake()
        {
            Create();
        }
        public void LateUpdate()
        {
        }

    }










}