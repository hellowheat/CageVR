using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class CanBeBreak : MonoBehaviour
{
    public int cullTime;//切割迭代次数
    public Material cutMaterial;//切割的材质，没有就用原材质
    public float centerOffsetY;//中心偏移
    public float removeTime;//碎片移除时间
    public float speedThreashold;//破裂速度阈值

    private void OnCollisionEnter(Collision collision)
    {

        Rigidbody myrb = gameObject.GetComponent<Rigidbody>();
        Rigidbody crb = collision.transform.GetComponent<Rigidbody>();
        myrb.velocity = -myrb.velocity;
        if (myrb != null) Debug.Log(myrb.velocity+",my,"+ myrb.velocity.magnitude);
        if (crb != null) Debug.Log(crb.velocity+",collision" + crb.velocity.magnitude);

        if ((myrb != null && myrb.velocity.magnitude >= speedThreashold))
           // || (crb != null && crb.velocity.magnitude >= speedThreashold))
        {
            beBreak();
        }
    }

    void beBreak()
    {
        sliceObj(gameObject, cullTime, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
    }

    void sliceObj(GameObject gb,int time,Vector3 dir)
    {
        if (time <= 0) return;
        SlicedHull hull = gb.Slice(transform.position+new Vector3(0, centerOffsetY, 0), 
            dir);
        Vector3[] rotateType = { Vector3.up, Vector3.right, Vector3.forward };
        Material material = cutMaterial? cutMaterial:gb.GetComponent<Renderer>().material;
        if(hull != null)
        {
            if (hull.lowerHull != null)
            {
                GameObject lowGB = hull.CreateLowerHull(gb, material);
                lowGB.transform.position = gb.transform.position;
                lowGB.AddComponent<BoxCollider>();
                lowGB.AddComponent<Rigidbody>();
                lowGB.AddComponent<AutoParentUpdate>();
                lowGB.AddComponent<PickIObject>().interactorPath= "Pick_VasePart";
                if (removeTime >= 0) lowGB.AddComponent<AutoDestory>().destoryTime = removeTime ;
                sliceObj(lowGB, time - 1, Quaternion.AngleAxis(60, rotateType[Random.Range(0, 3)]) * dir);
            }
            if (hull.upperHull != null)
            {
                GameObject upGB = hull.CreateUpperHull(gb, material);
                upGB.transform.position = gb.transform.position;
                upGB.AddComponent<BoxCollider>();
                upGB.AddComponent<Rigidbody>();
                upGB.AddComponent<AutoParentUpdate>();
                upGB.AddComponent<PickIObject>().interactorPath = "Pick_VasePart";
                if (removeTime >= 0) upGB.AddComponent<AutoDestory>().destoryTime = removeTime;
                sliceObj(upGB, time - 1, Quaternion.AngleAxis(60, rotateType[Random.Range(0, 3)]) * dir);
            }

            Destroy(gb);
        }
    }
}
