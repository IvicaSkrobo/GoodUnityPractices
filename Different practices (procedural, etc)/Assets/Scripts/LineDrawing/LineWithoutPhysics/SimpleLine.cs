using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleLine : MonoBehaviour
{
    LineRenderer line;
    List<Vector2> points;

    private void Awake()
    {
        line =GetComponent<LineRenderer>();
    }

    public void UpdateLine(Vector2 pos)
    {
        if(points==null)
        {
            points = new List<Vector2>();
            SetPoint(pos);
        }

        if(Vector2.Distance(points.Last(),pos)>0.1f)
        {
            SetPoint(pos);
        }
    }

    void SetPoint(Vector2 pointPos)
    {
        points.Add(pointPos);
        line.positionCount++;
        line.SetPosition(line.positionCount - 1,pointPos);

    }
}
