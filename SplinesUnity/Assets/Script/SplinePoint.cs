using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinePoint : MonoBehaviour
{
    public GameObject prePoint;
    public GameObject postPoint;

    private void Start()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        if (children.Length >= 2)
        {
            prePoint = children[1].gameObject;
            postPoint = children[2].gameObject;
        }
    }
}
