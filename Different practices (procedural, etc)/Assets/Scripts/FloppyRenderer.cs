using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloppyRenderer : MonoBehaviour
{
    public LineRenderer lineRend;

    public List<Transform> points = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
     lineRend.positionCount = points.Count;

    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }


    private void OnDrawGizmos()
    {
        lineRend.positionCount = points.Count;

        Draw();

    }


    private void Draw()
    {
        lineRend.SetPositions(points.Select(p => p.position).ToArray());

    }

}
