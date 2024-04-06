using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour 
{
    //should hold the info about where the joint is and the 2 nodes its a joint between
    public SkeletonNode sourse, target;
    public Vector3 angle = Vector3.zero;

    private Vector3 targetScale;
    private Quaternion targetRotation;
    private Vector3 normal, startNormal;
    private float dist;
    public Vector3 test;

    private void Awake()
    {
        sourse = gameObject.GetComponent<SkeletonNode>();
        transform.position = sourse.transform.position;
        normal = (target.transform.position - sourse.transform.position).normalized;
        startNormal = normal;
        dist = Vector3.Distance(target.transform.position, sourse.transform.position);
        targetScale = target.transform.localScale;
        targetRotation = target.transform.rotation * Quaternion.Inverse(sourse.transform.rotation);

        target.qJoint = this;
    }

    private void Update()
    {
        normal = (Quaternion.Euler(angle) * startNormal).normalized;
        targetRotation = Quaternion.LookRotation(normal);
    }

    public void setLocal()
    {
        Vector3 newPos = normal * dist;
        target.localTrans.SetTRS(newPos, targetRotation, targetScale);
    }
}
