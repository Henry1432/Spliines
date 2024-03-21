using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SplineGenerator : MonoBehaviour
{
    public List<SplinePoint> points = new List<SplinePoint>();
    [SerializeField] private float segments; //Number of Segments
    [SerializeField] private float t;

    public List<Spline> splines = new List<Spline>();
    [SerializeField] private GameObject subSplines;
    public bool closedEdit;
    private bool closed;
    private int splineEditIndex = 0;

    private void Start()
    {
        if(subSplines == null)
        {
            subSplines = GameObject.FindGameObjectWithTag("SplineHolder");
        }
        pointCheck();
    }

    private void Update()
    {
        pointCheck();
        t = Mathf.Clamp01(t);
        splineEditIndex = 0;
        if (closed)
        {
            for (int i = 0; points.Count > i; i++)
            {
                if (i >= 1)
                {
                    CreateSpline(points[i - 1], points[i]);
                }
            }
            CreateSpline(points[points.Count - 1], points[0]);
        }
        else
        {
            for (int i = 0; points.Count > i; i++)
            {
                if (i >= 1)
                {
                    CreateSpline(points[i - 1], points[i]);
                }
            }
        }
        if(splineEditIndex < splines.Count)
        {
            Debug.Log("Pop");
            for(int i = splines.Count - 1; i >= splineEditIndex; i--)
            {
                splines[i].DeleteSpline();
                splines.Remove(splines.Last());
            }
        }

        closed = closedEdit;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(p.transform.position, p.transform.position + GetDirection(points[0], points[1]).normalized * 0.5f);
    }
    //user interface
    public void CreateSpline(SplinePoint p0, SplinePoint p1)
    {
        Spline spline;
        bool newSpline = true;
        if (splineEditIndex >= splines.Count)
        {
            GameObject splineObj = new GameObject();
            splineObj.name = "spline" + subSplines.transform.childCount;
            splineObj.transform.parent = subSplines.transform;
            spline = splineObj.AddComponent<Spline>();
            spline.splineGenerator = this;
            spline.editIndex = 0;
        }
        else
        {
            //Debug.Log(splineEditIndex);
            spline = splines[splineEditIndex];
            spline.splineGenerator = this;
            spline.editIndex = 0;
            newSpline = false;
        }

        t = 0;
        float tn = 1 / segments;

        while (t <= 1)
        {
            t = Mathf.Clamp01(t);
            spline.AddPoint(GetPoint(p0, p1), GetDirection(p0, p1));

            t += tn;
        }
        if(spline.points.Count > spline.editIndex)
        {
            spline.points.RemoveRange(0, 2);
            spline.direction.RemoveRange(0, 2);
        }
        if(newSpline)
        {
            spline.FixLists();
            splines.Add(spline);
        }
        splineEditIndex++;

    }

    public Vector3 GetDirection(SplinePoint p1, SplinePoint p2)
    {
        return GetDirection(p1.transform.position, p1.postPoint.position, p2.prePoint.position, p2.transform.position, t);
    }
    public Vector3 GetPoint(SplinePoint p1, SplinePoint p2)
    {
        return GetPoint(p1.transform.position, p1.postPoint.position, p2.prePoint.position, p2.transform.position, t);
    }

    //math
        //derivative of get point
    Vector3 GetDirection(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 d;

        float t2 = Mathf.Pow(t, 2);
        d = p0 * (-3 * (t2) + 6*(t) - 3) +
            p1 * (9 * (t2) - 12 * (t) + 3) +
            p2 * (-9 * (t2) + 6 * t) +
            p3 * (3 * t2);

        return d;
    }
        //simplified lerp kinda
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 p;

        float t2 = Mathf.Pow(t, 2);
        float t3 = Mathf.Pow(t, 3);
        p = p0 * (-t3 + 3*(t2) - 3*t + 1) +
            p1 * (3*(t3) -6* (t2) + 3*t) + 
            p2 * (-3*(t3) + 3 * (t2)) + 
            p3 * (t3);

        return p;
    }
        //old getpoint
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
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

    private void pointCheck()
    {
        points.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            SplinePoint point = transform.GetChild(i).GetComponent<SplinePoint>();
            if (point)
                points.Add(point);
        }
    }
}
