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
    private Vector3 normal;
    private float dist;


    private void Awake()
    {
        sourse = gameObject.GetComponent<SkeletonNode>();
        transform.position = sourse.transform.position;
        normal = (target.transform.position - sourse.transform.position).normalized;
        dist = Vector3.Distance(target.transform.position, sourse.transform.position);
        targetScale = target.transform.localScale;
        targetRotation = target.transform.rotation * Quaternion.Inverse(sourse.transform.rotation);

        target.qJoint = this;
    }

    private void Update()
    {
        Vector3 newPos = normal * dist;
        target.localTrans.SetTRS(newPos, targetRotation, targetScale);
    }

    //public void setLocal()
    //{
    //    normal = (sourse.localTrans.ValidTRS() ? (sourse.localTrans.rotation * (Quaternion.Euler(angle) * baseNormal)) : (Quaternion.Euler(angle) * baseNormal)).normalized;
    //    Vector3 newPos = normal * dist;
    //    Quaternion normalRotation = Quaternion.FromToRotation(baseNormal, normal);
    //    //Joint[] joints = target.GetComponents<Joint>();
    //    //foreach (Joint joint in joints)
    //    //{
    //    //    //joint.editStartNormalDirectionDown(normalRotation);
    //    //}
    //    targetRotation = normalRotation;
    //    target.localTrans.SetTRS(newPos, targetRotation, targetScale);
    //}

    public void editStartNormalDirectionDown(Quaternion q)
    {
        Joint[] joints = target.GetComponents<Joint>();
        foreach (Joint joint in joints)
        {
            joint.editStartNormalDirectionDown(q);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + normal * 1.5f);
    }
}
