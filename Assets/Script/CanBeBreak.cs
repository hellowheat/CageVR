using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class CanBeBreak : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            beBreak();
        }
    }

    void beBreak()
    {
        sliceObj(gameObject, 5);
    }

    void sliceObj(GameObject gb,int time)
    {
        if (time <= 0) return;
        SlicedHull hull = gb.Slice(gb.transform.position,new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
        if(hull != null)
        {
            if (hull.lowerHull != null)
            {
                GameObject lowGB = hull.CreateLowerHull();
                lowGB.AddComponent<BoxCollider>();
                lowGB.AddComponent<Rigidbody>();
                sliceObj(lowGB, time - 1);
            }
            if (hull.upperHull != null)
            {
                GameObject upGB = hull.CreateLowerHull();
                upGB.AddComponent<BoxCollider>();
                upGB.AddComponent<Rigidbody>();
                sliceObj(upGB, time - 1);
            }

            Destroy(gb);
        }
    }
}
