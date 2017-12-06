using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace HooksLaw
{
    public class DragAndClickBehaviour : MonoBehaviour/*, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler*/
    {

        public bool DragOnSurface = true;
        public ParticleBehaviour pat;
        private GameObject m_DragIcon;
        private RectTransform m_DragPlane;
        public bool _mousestate;
        public GameObject target;
        public Vector3 screenSpace;
        public Vector3 offset;
        public RaycastHit hitinfo;
        public GameObject gameObjCursor;
        public Vector3 Cursor;
        public float distance;




        public void OnPointerClick()
        {
            var part = target.transform.position;
            Debug.Log(target + ("Particle Clicked"));
            if (gameObjCursor.transform.position == pat.transform.position)
            {
             
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                target.transform.position = hitinfo.transform.position;

            }
        }


        //    public void OnBeginDrag(PointerEventData eventData)
        //    {
        //        var canvas = FindInParents<Canvas>(gameObject);
        //        if( canvas == null)
        //            return;
        //        m_DragIcon = new GameObject("icon");

        //        m_DragIcon.transform.SetParent(canvas.transform, false);
        //        m_DragIcon.transform.SetAsLastSibling();
        //        var image = m_DragIcon.AddComponent<Image>();

        //        image.sprite = GetComponent<Image>().sprite;
        //        image.SetNativeSize();

        //        if (DragOnSurface)
        //            m_DragPlane = transform as RectTransform;
        //        else
        //            m_DragPlane = canvas.transform as RectTransform;

        //        SetDraggedPosition(eventData);
        //    }

        //    public void OnDrag(PointerEventData Data)
        //    {
        //        if (m_DragIcon != null)
        //            SetDraggedPosition(Data);
        //    }

        //    public void OnEndDrag(PointerEventData eventData)
        //    {
        //        if (m_DragIcon != null)
        //            Destroy(m_DragIcon);
        //    }
        //    private void SetDraggedPosition(PointerEventData data)
        //    {
        //        if (DragOnSurface && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
        //            m_DragPlane = data.pointerEnter.transform as RectTransform;

        //        var rt = m_DragIcon.GetComponent<RectTransform>();
        //        Vector3 globalMousePos;
        //        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DragPlane, data.position, data.pressEventCamera, out globalMousePos))
        //        {
        //            rt.position = globalMousePos;
        //            rt.rotation = m_DragPlane.rotation;
        //        }
        //    }
        //    static public T FindInParents<T>(GameObject go) where T : Component
        //    {
        //        if (go == null) return null;
        //        var comp = go.GetComponent<T>();

        //        if (comp != null)
        //            return comp;

        //        Transform t = go.transform.parent;
        //        while (t != null && comp == null)
        //        {
        //            comp = t.gameObject.GetComponent<T>();
        //            t = t.parent;
        //        }
        //        return comp;
        //    }
        //}
        public void OnMouseDown()
        {

            GameObject.FindObjectsOfType<ParticleBehaviour>().ToList();
            gameObjCursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        }
        GameObject GetClickedObject(out RaycastHit hit)
        {

            GameObject.FindObjectOfType<ParticleBehaviour>();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
            {
                gameObjCursor.transform.position = Camera.main.WorldToScreenPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                target = hit.collider.gameObject;
            }

            return target;
        }

        float time = 1.0f;
        public void NoSpawn()
        {
            time -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                gameObjCursor = Instantiate<GameObject>(gameObjCursor);

                gameObjCursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            }
            if (time <= 0)
            {
                Destroy(gameObjCursor);
                time = 1;
            }

        }
        public void Update()
        {
          
            NoSpawn();
            Debug.Log(gameObjCursor.transform.position);
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitinfo;
                target = GetClickedObject(out hitinfo);
                if (target != null)
                {

                    _mousestate = true;
                    gameObjCursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                    Cursor = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                    screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                   
                    
                }
                if (gameObjCursor.transform.position == Cursor)
                {
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                    target.transform.position = hitinfo.transform.position;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Destroy(gameObjCursor);
                    _mousestate = false;
                    
                }
                if (_mousestate)
                {

                    var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

                    //convert the screen mouse position to world point and adjust with offset
                    var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
                    var MousePos = Camera.main.ScreenToWorldPoint(Cursor);
                    //update the position of the object in the world
                    gameObjCursor.transform.position = MousePos;
                    target.transform.position = curPosition;
                }

            }


        }

    }
}

