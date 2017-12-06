using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace HooksLaw
{
    public class DragAndClickBehaviour : MonoBehaviour
    {

        public bool _mousestate;
        public Particle pat;
        public GameObject target;
        public Vector3 screenSpace;
        public Vector3 offset;
        public RaycastHit hitinfo;

        public void OnMouseDown()
        {
            GameObject.FindObjectsOfType<ParticleBehaviour>().ToList();

        }
       public GameObject GetClickedObject(out RaycastHit hit)
        {
            var go = new GameObject();
            GameObject.FindObjectsOfType<ParticleBehaviour>().ToList();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
                target = hit.collider.gameObject;
            }

            return target;
        } 

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitinfo;
                target = GetClickedObject(out hitinfo);
                if (target != null)
                {
                    
                    _mousestate = true;
                    screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                }

                if (Input.GetMouseButtonUp(0))
                {
                    _mousestate = false;
                }
                if (_mousestate)
                {

                    var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

                    //convert the screen mouse position to world point and adjust with offset
                    var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

                    //update the position of the object in the world
                    target.transform.position = curPosition;
                }

            }


        }
    }
}
