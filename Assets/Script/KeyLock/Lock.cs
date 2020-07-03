using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public string mdString;
    Rigidbody door;
    // Start is called before the first frame update
    void Start()
    {
        door =transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void open()
    {
        door.isKinematic = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
