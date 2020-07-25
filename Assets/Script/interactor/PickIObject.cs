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
    bool interactorEnd = true;
    Vector3 lastPos;
    Vector3[] lastMoveDis;//上几帧的位置
    float[] lastTime;//上几帧的时间

    public override void Start()
    {
        base.Start();
        fc = GetComponent<FaceCamera>();
        rb = gameObject.GetComponent<Rigidbody>();
        lastMoveDis = new Vector3[3];
        lastTime = new float[3];
        lastPos = transform.position;
        for (int i=0;i< lastMoveDis.Length;i++) lastMoveDis[i] = transform.position;
    }


    void LateUpdate()
    {
        if (!interactorEnd)
        {
            for (int i = 0; i < lastMoveDis.Length - 1; i++) 
            {
                lastMoveDis[i] = lastMoveDis[i + 1];
                lastTime[i] = lastTime[i + 1];
            }
            lastMoveDis[lastMoveDis.Length - 1] = transform.position - lastPos ;
            lastTime[lastMoveDis.Length - 1] = Time.deltaTime;
            lastPos = transform.position;
        }
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

            Vector3 goalMove = interactor.pickPosition.transform.position - (hit.point-transform.position);
            Debug.Log("interactor :"+ interactor.pickPosition.transform.position +
                ",goalMove:"+ goalMove + 
                ",offsetMove:"+ (hit.point - transform.position));

            transform.DOMove(goalMove, 0.1f).OnComplete(()=> {
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

    private void Update()
    {

    }

    public override void beInteractorExit(Interactor interactor)
    {
        try
        {
            interactorEnd = true;
            Destroy(fixedJoint);
           // fixedJoint.breakForce = 0;

            if (fc) fc.enabled = true;

            if (rb)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                //rb.velocity = interactor.gameObject.GetComponent<Rigidbody>().velocity;
                Vector3 sumVelocity = Vector3.zero ;
                for(int i=0; i < lastMoveDis.Length; i++)
                {
                    sumVelocity += lastMoveDis[i] / lastTime[i];
                }
                rb.velocity = sumVelocity / lastMoveDis.Length * 2;
                Debug.Log( "release velocity:"+rb.velocity);
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
