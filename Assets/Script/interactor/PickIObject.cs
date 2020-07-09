using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickIObject : InteractorObject
{
    FixedJoint fixedJoint;
    FaceCamera fc;
    Rigidbody rb;

    private Interactor interactor;//主动跟自己交互的交互器
    bool interactorEnd = false;

    public override void Start()
    {
        base.Start();
        fc = GetComponent<FaceCamera>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public override void beInteractorEnter(Interactor interactor,RaycastHit hit)
    {
        if(gameObject.GetComponent<FixedJoint>() == null)
        {
            this.interactor = interactor;
            if (fc) fc.enabled = false;//如果有面向相机，需要关闭面向相机

            Collider cd = gameObject.GetComponent<Collider>();//将trigger变成collider
            cd.isTrigger = false;


            interactorEnd = false;
            if(rb)rb.useGravity = false;
            
            transform.DOMove(interactor.pickPosition.transform.position, 0.1f).OnComplete(()=> {
                if(interactorEnd == false)
                {
                    fixedJoint = gameObject.AddComponent<FixedJoint>();
                    if (rb == null) rb = GetComponent<Rigidbody>();
                    rb.useGravity = true;
                    fixedJoint.breakForce = 200;
                    fixedJoint.connectedBody = interactor.GetComponent<Rigidbody>();
                }
            });
            
        }
    }

    public override void beInteractorExit(Interactor interactor)
    {
        try
        {
            interactorEnd = true;

            fixedJoint.breakForce = 0;

            if (fc) fc.enabled = true;

            if (rb)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.velocity = interactor.gameObject.GetComponent<Rigidbody>().velocity;
            }
        }
        catch { }
    }

    private void OnJointBreak(float breakForce)
    {
        if (breakForce > 1000)
        {
            //添加反向力
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            //rb.AddForce(transform.up * breakForce / 10);

            //回调交互器的断开交互函数
            interactor.tryToStopInteractor(this);
        }
    }
}
