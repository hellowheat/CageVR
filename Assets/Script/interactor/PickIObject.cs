using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickIObject : InteractorObject
{
    FixedJoint fixedJoint;
    FaceCamera fc;
    

    public override void Start()
    {
        base.Start();
        fc = GetComponent<FaceCamera>();
    }

    public override void beInteractorEnter(Interactor interactor,RaycastHit hit)
    {
        if(gameObject.GetComponent<FixedJoint>() == null)
        {
            if (fc) fc.enabled = false;

            transform.position = interactor.pickPosition.transform.position ;
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            fixedJoint.breakForce = 200;
            fixedJoint.connectedBody = interactor.GetComponent<Rigidbody>();

        }
    }

    public override void beInteractorExit(Interactor interactor)
    {
        try
        {
            fixedJoint.breakForce = 0;
            if (fc) fc.enabled = true;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.velocity = interactor.gameObject.GetComponent<Rigidbody>().velocity;
        }
        catch { }
       // Destroy(gameObject.GetComponent<FixedJoint>()) ;
    }

    private void OnJointBreak(float breakForce)
    {
        Debug.Log(breakForce);
        if (breakForce > 1000)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            //rb.AddForce(transform.up * breakForce / 10);

        }
    }
}
