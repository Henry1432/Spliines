using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subPointRender : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
