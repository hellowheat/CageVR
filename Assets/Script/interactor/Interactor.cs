using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//交互器
public class Interactor : MonoBehaviour
{
    public InteractorTriggerEvent trigger;
    public float pointDistance;//检测长度
    public TextMesh textMesh;
    [Header("line")]
    public bool useLine;//使用画线
    public Material lineMaterial;
    public float lineWidth;
    public Vector3 lineOffset;
    public GameObject lineHitPrfb;
    [Header("pick")]
    public GameObject pickPosition;//手心拾取道具位置


    private List<InteractorObject> interactorObjs;//附近交互对象集
    InteractorObject pointerInteractorObj;  //指向的交互对象
    InteractorObject nowHighShowInteractorObj;//当前高亮对象
    InteractorObject nowInteractorObj;//当前交互对象，为空表示没有交互
    RaycastHit savehit;//检测的射线
    void Awake()
    {
        interactorObjs = new List<InteractorObject>();
    } 

    void Start()
    {
        trigger?.setEnterCallBack(TriggerCallBack);//设置进入监听
        trigger?.setExitCallBack(TriggerCallBack);//设置离开监听

        if (useLine)
        {
            GameObject line = new GameObject("line");
            LineRenderer drawLine = line.AddComponent<LineRenderer>();
            drawLine.useWorldSpace = false;
            drawLine.material = lineMaterial;
            drawLine.positionCount = 2;
            drawLine.startWidth = drawLine.endWidth = lineWidth;
            drawLine.SetPosition(0, transform.position + lineOffset);
            drawLine.SetPosition(1, transform.position + transform.forward.normalized * pointDistance + lineOffset);
            line.transform.SetParent(transform);
            line.layer = 9;
            lineHitPrfb = Instantiate(lineHitPrfb, null);
            lineHitPrfb.layer = 9  ;
        }

        StartCoroutine(checkPoint());
        StartCoroutine(updateLineHit());
    }

    IEnumerator updateLineHit()
    {
        var waitTime = new WaitForSeconds(0.01f);
        RaycastHit raycastHit;
        while (true)
        {
            if (useLine)
            {
                if (Physics.Raycast(transform.position + lineOffset, transform.forward.normalized, out raycastHit, pointDistance,~(1 << 11)))
                {
                    Debug.Log("cast " + raycastHit.transform.name);
                    lineHitPrfb.transform.position = raycastHit.point;
                    lineHitPrfb.transform.forward = -raycastHit.normal;
                    lineHitPrfb.SetActive(true);
                }
                else
                {
                    lineHitPrfb.SetActive(false);
                }
            }

            yield return waitTime;
        }
    }


    //交互器发生附近碰撞时回调函数
    void TriggerCallBack(Collider collider,InteractorTriggerEventType type)
    {
        InteractorObject interactorObject = collider.GetComponent<InteractorObject>();
        if (interactorObject != null)//碰撞物体是可交互对象
        {
            if (type == InteractorTriggerEventType.ENTER)
            {
                //触发器发生碰撞事件
                if (interactorObjs.IndexOf(interactorObject) == -1)//未在已碰撞里面
                {
                    interactorObjs.Add(interactorObject);//添加到交互对象中

                }
            }
            else if (type == InteractorTriggerEventType.EXIT)
            {
                //触发器发生离开碰撞事件
                if (interactorObjs.IndexOf(interactorObject) != -1)//已在已碰撞里面
                {
                    interactorObjs.Add(interactorObject);//添加到交互对象中
                }
            }
        }
    }

    //持续监听交互器的指向
    IEnumerator checkPoint()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.1f);//隔一段时间监听一次
        RaycastHit hit;
        while (true)
        {
            Physics.Raycast(transform.position, transform.forward, out hit, pointDistance);
            if (Physics.Raycast(transform.position, transform.forward, out hit, pointDistance) && hit.transform.GetComponent<InteractorObject>())//检测碰撞，并且碰撞的对象有InteractorObject
            {
                savehit = hit;
                InteractorObject interactorObject = hit.transform.GetComponent<InteractorObject>();
                pointerInteractorObj = interactorObject;
            }
            else
            {
                //指向的对象为空
                pointerInteractorObj = null;
            }
            updateSelectObject();

            yield return waitTime;
        }
    }

    //更新交互对象事件
    void updateSelectObject()
    {
        if (nowInteractorObj == null) //未处于交互状态
        {
            InteractorObject calcInteractor = null;//应交互的对象
            if (pointerInteractorObj) calcInteractor = pointerInteractorObj;//有指向的目标
            else
            {
                //无指向的目标
                for (int i = 0; i < interactorObjs.Count; i++)
                {
                    if (calcInteractor == null || Vector3.Distance(interactorObjs[i].transform.position, transform.position) < Vector3.Distance(calcInteractor.transform.position, transform.position))
                    {
                        calcInteractor = interactorObjs[i];
                    }
                }
            }

            //处理事件，当应该选择的和目前选择的不同时
            if (calcInteractor != nowHighShowInteractorObj)
            {
                nowHighShowInteractorObj?.bePointExit(this);
                nowHighShowInteractorObj = calcInteractor;
                nowHighShowInteractorObj?.bePointEnter(this, savehit);
            }
        }
        else
        {
            //处于交互状态,高亮对象置空
            nowHighShowInteractorObj?.bePointExit(this);
            nowHighShowInteractorObj = null;
        }

        //更新触发器文本
        if (textMesh)
        {
            textMesh.text = nowHighShowInteractorObj ? nowHighShowInteractorObj.interactorInfo : string.Empty;
        }
    }

    //发生交互
    public void InteractorActionStart()
    {
        if(nowInteractorObj == null)
        {
            nowInteractorObj = nowHighShowInteractorObj;
            nowHighShowInteractorObj?.bePointExit(this);//不被指向
            nowHighShowInteractorObj = null;
            nowInteractorObj?.beInteractorEnter(this,savehit);//被交互
        }
    }

    //结束交互
    public void InteractorActionEnd()
    {
        if(nowInteractorObj != null)
        {
            nowInteractorObj?.beInteractorExit(this);
            nowInteractorObj = null;
            updateSelectObject();//重新更新选项
        }
    }

    //被交互对象尝试主动结束交互
    public void tryToStopInteractor(InteractorObject interactorObject)
    {
        if(interactorObject == nowInteractorObj)
        {
            InteractorActionEnd();
        }
    }
}
