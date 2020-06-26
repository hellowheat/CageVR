using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class TestSlice : MonoBehaviour
{
    
    public GameObject plane;
    // Start is called before the first frame update
    void Start() 
    {
        if (plane)
        {
            EzySlice.Plane p = new EzySlice.Plane(transform.position, transform.up);
            SlicedHull slicedHull = gameObject.Slice(transform.position, transform.up);
            if (slicedHull != null)
            {
                slicedHull.CreateLowerHull(gameObject);
                slicedHull.CreateUpperHull(gameObject);
            }
            Debug.Log("slice success");
        }
    }

}
