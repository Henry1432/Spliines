using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour 
{
    //should hold the info about where the joint is and the 2 nodes its a joint between
    public SkeletonNode sourse, target;
    private Vector3 normal;
    private float dist;

    private void Awake()
    {
        sourse = gameObject.GetComponent<SkeletonNode>();
        transform.position = sourse.transform.position;
        normal = (target.transform.position - sourse.transform.position).normalized;
        dist = Vector3.Distance(target.transform.position, sourse.transform.position);
    }
}
