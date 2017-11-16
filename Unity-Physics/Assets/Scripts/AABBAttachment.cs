using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABBAttachment : MonoBehaviour
{
    public AABBCollision one = new AABBCollision();
    public AABBCollision two = new AABBCollision();
    public Utilities ult = new Utilities();
    public GameObject box1 = new GameObject();
    public GameObject box2 = new GameObject();
    public List<AABBCollision> gameojs;
    public bool Collision = false;
    void Start ()
    {
        one.InitBox(new Vector2(box1.transform.position.x, box1.transform.position.y), 1);
        two.InitBox(new Vector2(box2.transform.position.x, box2.transform.position.y), 1);
        Debug.Log(ult.TestOverLap(one, two));
        Collision = ult.TestOverLap(one, two);

        ult.sortandsweep(gameojs);
    }

    void Update ()
    {
       
        one.InitBox(new Vector2(box1.transform.position.x, box1.transform.position.y), 1);
        two.InitBox(new Vector2(box2.transform.position.x, box2.transform.position.y), 1);
        Debug.Log(ult.TestOverLap(one, two));
        Collision = ult.TestOverLap(one, two);
    }
}
