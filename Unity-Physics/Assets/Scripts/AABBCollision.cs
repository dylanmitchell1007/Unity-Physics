using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;





public class AABBCollision
{
    public Vector2 min;
    public Vector2 max;


    public void InitBox(Vector2 position, float size)
    {
        min = position;
        max = position;

        min.x -= size;
        min.y -= size;
        max.x += size;
        max.y += size;




    }

};
public class Utilities
{
    public List<AABBCollision> axisList;
    public void sortandsweep(List<AABBCollision> objs)
    {
        var sorted = objs.OrderBy(x => x.min.x).ToList();
        axisList = sorted;


    }


    public bool TestOverLap(AABBCollision a, AABBCollision b)
    {
        var d1x = b.min.x - a.max.x;
        var d1y = b.min.y - a.max.y;
        var d2x = a.min.x - b.max.x;
        var d2y = a.min.y - b.max.y;




        if (d1x > 0 || d1y > 0)
        {
            return false;
        }
        if (d2x > 0 || d2y > 0)
        {
            return false;
        }
        else
            return true;
    }

}
