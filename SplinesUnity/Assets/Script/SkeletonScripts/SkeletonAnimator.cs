using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class SkeletonAnimator : MonoBehaviour
{
    public SaveSkeletonInfo SaveSkeletonInfo = new SaveSkeletonInfo();

    public List<float> timeStamps = new List<float>();
    public float timer = 0;
    public int current;

    public bool loop;

    public List<Vector3> currentAngles = new List<Vector3>();

    public void Start()
    {
        current = 0;
    }

    private void Update()
    {
        if (timer < timeStamps.Last())
        {
            timer += Time.deltaTime;

            if(timer > timeStamps[current])
            {
                current++;
            }
        }
        else if(loop)
        {
            timer = 0;
            current = 0;
        }
        else
        {
            timer -= Time.deltaTime;
            current = timeStamps.Count - 1;
        }

        SetSkeleton();
    }

    private void SetSkeleton()
    {
        if(current < timeStamps.Count)
        {
            SaveSkeletonInfo.SetAnglesFromStamp(timeStamps[current]);

            currentAngles = SaveSkeletonInfo.angles;
        }
        SkeletonBase.instance.angles = currentAngles;
    }
}
