using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRend;

    private EdgeCollider2D coll;

    private readonly List<Vector2> points = new List<Vector2>();

    // Start is called before the first frame update
    void Awake()
    {
        lineRend = GetComponent<LineRenderer>();
        coll = GetComponent<EdgeCollider2D>();
      
    }

    public bool CanAppend(Vector2 pos)

    {
        if(lineRend.positionCount<=0)
        {
            return true;
        }

        return Vector2.Distance(lineRend.GetPosition(lineRend.positionCount-1),pos) > DrawScript.RESOLUTION;
      
    }
    public void SetPosition(Vector2 pos)
    {
       if(!CanAppend(pos))
        {
            return;
        }

        lineRend.positionCount++;
        points.Add(pos);
        

        lineRend.SetPosition(lineRend.positionCount-1, pos);
        coll.SetPoints(points);
    }
}
