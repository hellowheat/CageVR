using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public string mdString;
    public OpenIObject openIObject;
    Rigidbody door;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        door =transform.parent.GetComponent<Rigidbody>();
        isOpen = false;
    }

    // Update is called once per frame
    public void open()
    {
        if (!isOpen)
        {
            isOpen = true;
            openIObject.enabled = true;
            openIObject.peekDoor();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //已经在钥匙上处理
    }
}
