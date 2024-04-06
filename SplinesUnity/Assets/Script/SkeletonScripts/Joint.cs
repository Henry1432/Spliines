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
    private Vector3 normal, baseNormal, startNormal;
    private float dist;


    private void Awake()
    {
        sourse = gameObject.GetComponent<SkeletonNode>();
        transform.position = sourse.transform.position;
        normal = (target.transform.position - sourse.transform.position).normalized;
        baseNormal = normal;
        startNormal = normal;
        dist = Vector3.Distance(target.transform.position, sourse.transform.position);
        targetScale = target.transform.localScale;
        targetRotation = target.transform.rotation * Quaternion.Inverse(sourse.transform.rotation);

        target.qJoint = this;
    }

    private void Update()
    {
        setLocal();
    }

    public void setLocal()
    {
        normal = (sourse.globalTrans.ValidTRS() ? (sourse.globalTrans.rotation * (Quaternion.Euler(angle) * baseNormal)) : (Quaternion.Euler(angle) * baseNormal)).normalized;
        Vector3 newPos = normal * dist;
        Joint[] joints = target.GetComponents<Joint>();
        Quaternion normalRotation = Quaternion.FromToRotation(baseNormal, normal);
        foreach (Joint joint in joints)
        {
            joint.editStartNormalDirectionDown(normalRotation);
        }
        targetRotation = normalRotation;
        target.localTrans.SetTRS(newPos, targetRotation, targetScale);
    }

    public void editStartNormalDirectionDown(Quaternion q)
    {
        baseNormal = q * startNormal;
        normal = (q) * normal;
        Joint[] joints = target.GetComponents<Joint>();
        foreach (Joint joint in joints)
        {
            joint.editStartNormalDirectionDown(q);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + normal);
    }
}
