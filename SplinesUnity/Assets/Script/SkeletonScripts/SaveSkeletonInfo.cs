using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveSkeletonInfo : MonoBehaviour
{
    public List<Vector3> angles = new List<Vector3>();

    public float timeStamp = 0f;

    public bool test;

    private void Update()
    {
        if(test)
        {
            test = !test;
            angles = SkeletonBase.instance.angles;
            fInSkeletonInfo("Data\\" + timeStamp +".txt");

            SkeletonBase.instance.angles = angles;
        }
    }

    public void fInSkeletonInfo(string fileName)
    {
        StreamReader reader = new StreamReader(fileName);
        angles.Clear();

        float temp;
        if(float.TryParse(reader.ReadLine(), out temp))
        {
            timeStamp = temp;
        }

        while(!reader.EndOfStream)
        {
            string aString = reader.ReadLine();
            string[] angle = aString.Split(" ");
            float x, y, z;
            if (angle.Length >= 3)
            {
                if (float.TryParse(angle[0], out x) && float.TryParse(angle[1], out y) && float.TryParse(angle[2], out z))
                {
                    Vector3 a = new Vector3(x, y, z);
                    angles.Add(a);
                }
            }
        }
    }

    public void fOutSkeletonInfo(string fileName)
    {
        if(angles.Count > 0)
        {
            if(!File.Exists(Application.dataPath + fileName))
            {
                //File.Create(Application.dataPath + fileName);
            }

            StreamWriter writer = new StreamWriter(fileName, false);

            writer.WriteLine(timeStamp.ToString());

            for(int i = 0; i < angles.Count; i++)
            {
                if(i + 1 == angles.Count)
                {
                    writer.Write(angles[i].x + " " + angles[i].y + " " + angles[i].z);
                }
                else
                {
                    writer.WriteLine(angles[i].x + " " + angles[i].y + " " + angles[i].z);
                }
            }

            writer.Close();
        }
    }
}
