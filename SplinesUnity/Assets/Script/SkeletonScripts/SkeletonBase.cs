using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonBase : MonoBehaviour
{
    //this should be the thing that tracks all the nodes and how I interact with each joint
    public static SkeletonBase instance;
    public List<SkeletonNode> allNodes = new List<SkeletonNode>();
    public SkeletonNode baseNode;


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        //should cut out non children in loop below instead of bailing
        allNodes.AddRange(FindObjectsOfType<SkeletonNode>());

        foreach (SkeletonNode node in allNodes)
        {
            if(node.parentIndex == -1)
            {
                baseNode = node;
                break;
            }
        }

        Joint[] thisJoints = baseNode.gameObject.GetComponents<Joint>();
        
        foreach (Joint joint in thisJoints)
        {
            SetParentJoint(joint);
        }

        SetGlobals(baseNode);
    }

    private void Update()
    {
        //handle local rotations based on joint angles here so that the rotation doesnt cascade down.
            //it was happening because when you used sourcenode rotation it would do that for all below doubling and doubling the rotation

        SetGlobals(baseNode);
    }

    private void SetGlobals(SkeletonNode node)
    {
        node.globalTrans = node.SetGlobal();
        Joint[] thisJoints = node.gameObject.GetComponents<Joint>();

        foreach (Joint joint in thisJoints)
        {
            SetGlobals(joint.target);
        }
    }

    private void SetParentJoint(Joint joint)
    {
        SkeletonNode sourceNode = joint.sourse.gameObject.GetComponent<SkeletonNode>();
        joint.target.parentIndex = allNodes.IndexOf(sourceNode);
        Joint testJoint = joint.target.gameObject.GetComponent<Joint>();
        if (testJoint != null)
        {
            SetParentJoint(testJoint);
        }
    }
}
