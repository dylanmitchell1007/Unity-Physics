//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace HooksLaw
//{


//    public class DSpring : MonoBehaviour
//    {


//        private GameObject _sphere;
//        public GameObject cursorPrefab;
//        public float GravForce;
//        public bool GravityAll, ApplyWind, LockTopLeft, LockTopRight, LockBotLeft, LockBotRight;
//        public float ks, kd;
//        private GameObject mouse;
//        [HideInInspector] public List<ParticleBehaviour> pbs = new List<ParticleBehaviour>();
//        public List<SpringDamperBehaviour> sbs = new List<SpringDamperBehaviour>();
//        public int Size;
//        private int Size2;
//        public List<Triangles> trianglesList = new List<Triangles>();
//        [HideInInspector] public ParticleBehaviour[] verts;
//        public Vector3 Wind;

//        // Use this for initialization
//        private void Start()
//        {
//            Wind = new Vector3(5, 0, 5);
//            ks = 10f;
//            kd = .5f;
//            Generate();
//            SetTriangles();
           
//        }

//        // Update is called once per frame
//        private void Update()
//        {

           

//            foreach (var sd in sbs)
//            {
//                sd.springConstant = ks;
//                sd.dampeningFactor = kd;
//            }
//            foreach (var p in pbs)
//            {
//                if (p.partical.IsGravity)
//                    p.partical.Addforce(new Vector3(0, -9.81f, 0) * GravForce);
//                p.partical.IsGravity = GravityAll;
               
//            }

//            foreach (var triangle in trianglesList)
//                if (ApplyWind)
//                    triangle.AerodynamicForce(Wind);
//            var removedList = new List<SpringDamperBehaviour>();
//            foreach (var sd in sbs)
//            {
//                sd.SpringDot(sd.p1.partical, sd.p2.partical, ks, kd);
//                if (sd.DistanceBreak())
//                    removedList.Add(sd);
//            }
//            foreach (var p in removedList)
//            {
//                if (sbs.Contains(p))
//                    sbs.Remove(p);
//                DestroyImmediate(p.gameObject);
//            }
//            foreach (var p in sbs)
//            {
//                if (p == null)
//                    break;
//                p.SpringDot(p.p1.partical, p.p2.partical, ks, kd);
//                //Debug.DrawLine(p.p1.particle.Position, p.p2.particle.Position);
//            }

//        }



//                //Bending Springs
//                //Horizontal
//                if (i % Size != Size - 1 && i % Size != Size - 2)
//                {
//                    var go = new GameObject();
//                    var sD = go.AddComponent<SpringDamperBehaviour>();
//                    go.transform.parent = transform;
//                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
//                    sD.p1 = verts[i];
//                    sD.p2 = verts[i + 2];
//                }

//                //Vertical
//                if (i < Size2 - Size * 2)
//                {
//                    var go = new GameObject();
//                    var sD = go.AddComponent<SpringDamperBehaviour>();
//                    go.transform.parent = transform;
//                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
//                    sD.p1 = verts[i];
//                    sD.p2 = verts[i + Size * 2];
//                }
//            }
//        }

//     
//        public void SetTriangles()
//        {
//            var bank = new List<SpringDamperBehaviour>();

//            // pbs = FindObjectsOfType<ParticleBehaviour>().ToList();
//            bank = FindObjectsOfType<SpringDamperBehaviour>().ToList();
//            for (var i = 0; i < Size * Size - Size; i++)
//                if (i < Size2 - Size && i % Size != Size - 1)
//                {
//                    trianglesList.Add(new Triangles(verts[i].partical,
//                        verts[i + 1].partical,
//                        verts[i + Size].partical));
//                    trianglesList.Add(new Triangles(verts[i].partical,
//                        verts[i + 1].partical,
//                        verts[i + Size + 1].partical));
//                }
//        }

//        }
//    }
//}