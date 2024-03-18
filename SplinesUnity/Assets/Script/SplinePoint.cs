using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinePoint : MonoBehaviour
{
    public Transform prePoint;
    public Transform postPoint;

    private void Start()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        if (children.Length >= 2)
        {
            prePoint = children[1];
            postPoint = children[2];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
