using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SplineGenerator : MonoBehaviour
{
    public List<SplinePoint> points = new List<SplinePoint>();
    [SerializeField] float t;
    public GameObject p; 
    public bool test; //Delete

    
    private void Update()
    {
        t = Mathf.Clamp01(t);
        if (test)
        {
            //test = false;
            p.transform.position = GetPoint(points[0], points[1]);
        }
    }

    Vector3 GetPoint(SplinePoint p1, SplinePoint p2)
    {
        return GetPoint(p1.transform.position, p1.postPoint.transform.position, p2.prePoint.transform.position, p2.transform.position, t);
    }

    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 A, B, C, D, E, p;

        A = Vector3.Lerp(p0, p1, t);
        B = Vector3.Lerp(p1, p2, t);
        C = Vector3.Lerp(p2, p3, t);
        D = Vector3.Lerp(A, B, t);
        E = Vector3.Lerp(B, C, t);

        p = Vector3.Lerp(D, E, t);

        return p;
    }
}
