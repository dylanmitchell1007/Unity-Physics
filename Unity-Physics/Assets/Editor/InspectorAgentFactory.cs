using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Dylan
{
#if UNITY_EDITOR
    [CustomEditor(typeof(AgentFactory))]
    public class InspectorAgentFactory : Editor
    {
        GUIStyle header = new GUIStyle();
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var mytarget = target as AgentFactory;

            GUILayout.Space(40);

            if (GUILayout.Button("Add:"))
            {
                mytarget.Create();
            }
           
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
#endif
}
