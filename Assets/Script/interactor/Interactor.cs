using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//交互器
public class Interactor : MonoBehaviour
{
    public InteractorTriggerEvent trigger;
    public float pointDistance;
    public TextMesh textMesh;
    private List<InteractorObject> interactorObjs;//附近交互对象集
    InteractorObject pointerInteractorObj;  //指向的交互对象
    InteractorObject nowHighShowInteractorObj;//当前高亮对象
    void Awake()
    {
        interactorObjs = new List<InteractorObject>();
    } 

    void Start()
    {
        trigger?.setEnterCallBack(TriggerCallBack);
        trigger?.setExitCallBack(TriggerCallBack);
        StartCoroutine(checkPoint());
    }
    
    
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

    //监听交互器指向
    IEnumerator checkPoint()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.1f);//隔一段时间监听一次
        RaycastHit hit;
        while (true)
        {
            Physics.Raycast(transform.position, transform.forward, out hit, pointDistance);
           if(hit.transform != null) Debug.Log("point " + hit.transform.name);
            if(Physics.Raycast(transform.position, transform.forward, out hit, pointDistance) && hit.transform.GetComponent<InteractorObject>())//检测碰撞，并且碰撞的对象有InteractorObject
            {
                InteractorObject interactorObject = hit.transform.GetComponent<InteractorObject>();
                if (interactorObject)
                {
                    pointerInteractorObj = interactorObject;
                    updateInteractorObject();
                }
            }
            else
            {
                //指向的对象为空
                //Debug.Log("pointer is null");
                pointerInteractorObj = null;
                updateInteractorObject();
            }

            yield return waitTime;
        }
    }

    void updateInteractorObject()
    {
        InteractorObject calcInteractor=null;//应交互的对象
        if (pointerInteractorObj) calcInteractor = pointerInteractorObj;//有指向的目标
        else
        {
            //无指向的目标
            for(int i = 0; i < interactorObjs.Count; i++)
            {
                if (calcInteractor == null || Vector3.Distance(interactorObjs[i].transform.position ,transform.position) < Vector3.Distance(calcInteractor.transform.position, transform.position))
                {
                    calcInteractor = interactorObjs[i];
                }
            }
        }

        //处理事件，当应该选择的和目前选择的不同时
        if(calcInteractor != nowHighShowInteractorObj)
        {
            nowHighShowInteractorObj?.bePointExit();
            nowHighShowInteractorObj = calcInteractor;
            nowHighShowInteractorObj?.bePointEnter();

            //更新触发器文本
            if (textMesh)
            {
                textMesh.text = nowHighShowInteractorObj ? nowHighShowInteractorObj.interactorInfo : string.Empty;
            }
        }
    }

    
}
