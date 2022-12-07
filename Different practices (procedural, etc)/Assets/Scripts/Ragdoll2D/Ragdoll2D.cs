using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Ragdoll2D : MonoBehaviour
{

    [SerializeField]
    Animator anim;

    [SerializeField]
    List<Collider2D> colliders;
    [SerializeField]
    List<HingeJoint2D> hingeJoints;
    [SerializeField]
    Rigidbody2D[] rbs;
    [SerializeField]
    List<LimbSolver2D> limbSolvers;

    public void Awake()
    {
        ToggleRagDoll(false);
    }

    public void ToggleRagDoll(bool ragDollOn)
    {
        anim.enabled = !ragDollOn;

        foreach(var col in colliders)
        {
            col.enabled = ragDollOn;
        }

        foreach (var rb in rbs)
        {
            rb.simulated = ragDollOn;
        }

        foreach (var joint in hingeJoints)
        {
            joint.enabled = ragDollOn;
        }


        foreach (var limb in limbSolvers)
        {
            limb.weight = ragDollOn ? 0 : 1;
        }
    }
}
