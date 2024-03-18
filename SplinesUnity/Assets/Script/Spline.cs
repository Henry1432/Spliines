using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spline : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public List<Vector3> direction = new List<Vector3>();

    private void OnDrawGizmos()
    {
        Gizmos.DrawLineList(points.ToArray());
    }

    public void AddPoint(Vector3 point, Vector3 dir)
    {
        if (points.Count > 0)
        {
            points.Add(point);
            direction.Add(dir);
        }
        points.Add(point);
        direction.Add(dir);
    }
    public void FixLists()
    {
        //points.Remove(points.First());
        points.Remove(points.Last());
        //direction.Remove(direction.First());
        direction.Remove(direction.Last());
    }
}
