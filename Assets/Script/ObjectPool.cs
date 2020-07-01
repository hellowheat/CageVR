using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    Transform parent;
    GameObject saveGB;
    int maxOutNumber;
    int outNumber;
    List<GameObject> pools;
    public ObjectPool(GameObject gb,Transform parent,int maxOutNum)
    {
        pools = new List<GameObject>();
        outNumber = 0;
        saveGB = gb;
        this.parent = parent;
        maxOutNumber = maxOutNum;
    }

    public GameObject create(Vector3 position,Vector3 dirc)
    {
        if (outNumber >= maxOutNumber) return null;
        GameObject outGB;
        if (pools.Count > 0)
        {
            outGB = pools[0];
            pools.RemoveAt(0);
        }
        else
        {
            outGB = Object.Instantiate(saveGB, parent);
        }
        outGB.transform.position = position;
        if(dirc != Vector3.zero) outGB.transform.forward = dirc;
        outGB.SetActive(true);
        return outGB;
    }

    public void destory(GameObject gb)
    {
        gb.SetActive(false);
        pools.Add(gb);
    }
}
