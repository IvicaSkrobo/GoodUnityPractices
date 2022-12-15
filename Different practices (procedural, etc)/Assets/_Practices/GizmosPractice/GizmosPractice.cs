using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosPractice : MonoBehaviour
{


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(Vector3.zero, Vector3.one * 10);



       /* Gizmos.DrawWireSphere(Vector3.zero, 5f);

        for(int i=0; i < 10; i++)
        {
            Gizmos.color = Random.ColorHSV();
            Gizmos.DrawSphere(Random.insideUnitSphere * 5, 0.5F);
        }*/


    }


}
