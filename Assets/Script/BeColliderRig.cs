using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeColliderRig : MonoBehaviour
{
    public List<Transform> maskTransforms;
    bool isAddComponent;
    void Start()
    {
        isAddComponent = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isAddComponent) return;
        if(maskTransforms.IndexOf(collision.transform) == -1)
        {
            isAddComponent = true;
            gameObject.AddComponent<Rigidbody>();
        }
    }
}
