using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestory : MonoBehaviour
{
    public float destoryTime = 10;
    float checkCD = 1;
    float checkTime = 0;
    float waitTime = 0;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        waitTime += Time.deltaTime;
        checkTime += Time.deltaTime;
        if (checkTime > checkCD && rb != null)
        {
            if (rb.velocity != Vector3.zero) waitTime = 0;
        }

        if (waitTime >= destoryTime)
        {
            Destroy(gameObject);
        }

    }
}
