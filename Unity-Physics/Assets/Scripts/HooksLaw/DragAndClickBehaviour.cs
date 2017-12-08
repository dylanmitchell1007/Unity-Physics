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

        public GameObject Floor;

        private ClothBehaviour cloth;

        public Slider clickDragForce;

        public float ClickDragForce;

        public Vector3 minBoundry;
        public Vector3 maxBoundry;
        public Vector3 Scale;
        public float distance;

        #region WorldSpaceCursor
        public GameObject Model;
        public Vector3 CursorOffset;
        private GameObject _cursorGO;
        private Vector3 cursorWorldSpacePosition;
        #endregion


        #region OLD

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

        #endregion

        #region TestOverLap
        public bool TestOverLap(Vector3 col1MinBound, Vector3 col1MaxBound, Vector3 other)
        {
            Vector3 otherMaxBound = new Vector3(other.x + 1, other.y + 1, other.z + 1);
            Vector3 otherMinBound = new Vector3(other.x - 1, other.y - 1, other.z - 1);

            var d1x = otherMinBound.x - col1MaxBound.x;
            var d1y = otherMinBound.y - col1MaxBound.y;
            var d1z = otherMinBound.z - col1MaxBound.z;
            var d2x = col1MinBound.x - otherMaxBound.x;
            var d2y = col1MinBound.y - otherMaxBound.y;
            var d2z = col1MinBound.z - otherMaxBound.z;
            if (d1x > 0 || d1y > 0 || d1z > 0)
            {
                return false;
            }
            if (d2x > 0 || d2y > 0 || d2z > 0)
            {
                return false;
            }
            return true;
        }
        public bool TestOverLap(Vector3 col1MinBound, Vector3 col1MaxBound, Vector3 other, Vector3 otherScale)
        {
            Vector3 otherMaxBound = other + otherScale;
            Vector3 otherMinBound = other - otherScale;

            var d1x = otherMinBound.x - col1MaxBound.x;
            var d1y = otherMinBound.y - col1MaxBound.y;
            var d1z = otherMinBound.z - col1MaxBound.z;
            var d2x = col1MinBound.x - otherMaxBound.x;
            var d2y = col1MinBound.y - otherMaxBound.y;
            var d2z = col1MinBound.z - otherMaxBound.z;

            if (d1x > 0 || d1y > 0 || d1z > 0)
            {
                return false;
            }
            if (d2x > 0 || d2y > 0 || d2z > 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        public void Slider()
        {
            clickDragForce.minValue = 1;
            clickDragForce.maxValue = 500;
            ClickDragForce = clickDragForce.value;
            
        }
        public void BoundUpdate(Vector3 position, Vector3 scale)
        {
            minBoundry.x = position.x - scale.x;
            minBoundry.y = position.y - scale.y;
            minBoundry.z = position.z - scale.z;


            maxBoundry.x = position.x + scale.x;
            maxBoundry.y = position.y + scale.y;
            maxBoundry.z = position.z + scale.z;
        }
        public void Init(Vector3 position, Vector3 scale)
        {
            minBoundry.x = position.x - scale.x;
            minBoundry.y = position.y - scale.y;
            minBoundry.z = position.z - scale.z;


            maxBoundry.x = position.x + scale.x;
            maxBoundry.y = position.y + scale.y;
            maxBoundry.z = position.z + scale.z;
        }

        public void Start()
        {
            cloth = GameObject.FindObjectOfType<ClothBehaviour>();

            _cursorGO = new GameObject();
            _cursorGO.name = "_cursoGO";

            var model = Instantiate(Model, _cursorGO.transform.position, _cursorGO.transform.rotation);
            model.transform.SetParent(_cursorGO.transform);

            var cam = Camera.main;

            var mousePos = Input.mousePosition;

            cursorWorldSpacePosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0) + CursorOffset);

            _cursorGO.transform.position = cursorWorldSpacePosition;

            Init(cursorWorldSpacePosition, Scale);
            BoundUpdate(cursorWorldSpacePosition, Scale);
        }
        
        public void Update()
        {
            if(cloth == null)
            {
                cloth = GameObject.FindObjectOfType<ClothBehaviour>();
            }

            var cam = Camera.main;

            var mousePos = Input.mousePosition;

            cursorWorldSpacePosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0) + CursorOffset);

            _cursorGO.transform.position = cursorWorldSpacePosition;
            BoundUpdate(cursorWorldSpacePosition, Scale);

            var fbl = new Vector3(minBoundry.x, minBoundry.y, maxBoundry.z);
            var fbr = new Vector3(maxBoundry.x, minBoundry.y, maxBoundry.z);
            var ftr = new Vector3(maxBoundry.x, maxBoundry.y, maxBoundry.z);
            var ftl = new Vector3(minBoundry.x, maxBoundry.y, maxBoundry.z);
            var bbl = new Vector3(minBoundry.x, minBoundry.y, minBoundry.z);
            var bbr = new Vector3(maxBoundry.x, minBoundry.y, minBoundry.z);
            var btr = new Vector3(maxBoundry.x, maxBoundry.y, minBoundry.z);
            var btl = new Vector3(minBoundry.x, maxBoundry.y, minBoundry.z);
       
            Debug.DrawLine(bbl, bbr, Color.black);
            Debug.DrawLine(btl, btr, Color.black);

            #region MoreOLD
            ////    NoSpawn();
            ////    Debug.Log(gameObjCursor.transform.position);
            ////    if (Input.GetMouseButtonDown(0))
            ////    {
            ////        RaycastHit hitinfo;
            ////        target = GetClickedObject(out hitinfo);
            ////        if (target != null)
            ////        {

            ////            _mousestate = true;
            //gameObjCursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            //           Cursor = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            ////            screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
            ////            offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));


            ////        }
            ////        if (gameObjCursor.transform.position == Cursor)
            ////        {
            ////            offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            ////            target.transform.position = hitinfo.transform.position;
            ////        }

            ////        if (Input.GetMouseButtonUp(0))
            ////        {
            ////            Destroy(gameObjCursor);
            ////            _mousestate = false;

            ////        }
            ////        if (_mousestate)
            ////        {

            ////            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

            ////            //convert the screen mouse position to world point and adjust with offset
            ////            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            //          var MousePos = Camera.main.ScreenToWorldPoint(Cursor);
            ////            //update the position of the object in the world
            //           gameObjCursor.transform.position = MousePos;

            //            target.transform.position = curPosition;
            //        }

            //    }


            //}
            #endregion
        }

        void FixedUpdate()
        {
            foreach(var agent in cloth.particles)
            {
                if (agent.Position == Floor.transform.position)
                {
                    cloth.useGravity = true;
                    cloth.transform.position = Floor.transform.position;
                }
                if (TestOverLap(minBoundry, maxBoundry, agent.Position, new Vector3(45, 45, 45)))
                {
                    if(Input.GetMouseButton(0))
                    {
                        //get the mouse delta
                        var deltaX = Input.GetAxis("Mouse X");
                        var deltaY = Input.GetAxis("Mouse Y");

                        Vector3 translation = new Vector3(deltaX * ClickDragForce, deltaY * ClickDragForce, 0);
                        agent.Addforce(translation);
                    }
                    if (Input.GetMouseButton(1))
                    {
                        agent.Locked = true;
                    }
                    if (Input.GetMouseButton(2))
                    {
                        agent.Locked = false;
                    }
                  
                    Debug.Log("CURSOR COLLISION WITH PARTICLE");
                }
            }
        }
    }

}