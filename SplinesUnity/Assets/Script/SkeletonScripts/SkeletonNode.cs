using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonNode : MonoBehaviour
{
    //should be the interface for the Base, holds higherarchy position, the head and shoulders would be at the same hierarchy position in relation to the body
    public Matrix4x4 localTrans, globalTrans;
    public int parentIndex; //if base node -1, else parent index
}
