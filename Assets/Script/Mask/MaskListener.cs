using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MaskListener : MonoBehaviour
{
    public GameObject CallTriggerObject;
    private List<GameObject> canHideObj;
    List<GameObject> hideTrigger;//用于进入隐藏场景的Trigger
    List<GameObject> showTrigger;//用于进入显示场景的Trigger
    List<GameObject> resetTrigger;//用于重设场景的Trigger
    List<GameObject> masks;//用于重设场景的Trigger
    string canBeHideTagString = "canBeHide";//可隐藏目标的渲染队列
    string hideTriggerString = "MaskHideTrigger";
    string showTriggerString = "MaskShowTrigger";
    string resetTriggerString = "MaskResetTrigger";
    string wallMaskString = "WallMask";

    List<TriggerEnterMask> showEnterTrigger = new List<TriggerEnterMask>();
    List<TriggerEnterMask> hideEnterTrigger = new List<TriggerEnterMask>();
    List<TriggerEnterMask> resetEnterTrigger = new List<TriggerEnterMask>();
    void Start()
    {
        canHideObj = new List<GameObject>();
        hideTrigger = new List<GameObject>();
        showTrigger = new List<GameObject>();
        resetTrigger = new List<GameObject>();
        masks = new List<GameObject>();
        if (CallTriggerObject)
        {
            SerachAllHideObjAndTrigger(transform);
        }
        for (int i = 0; i < showTrigger.Count; i++)
        {
            if (showTrigger[i].GetComponent<TriggerEnterMask>())
            {
                showEnterTrigger.Add(showTrigger[i].GetComponent<TriggerEnterMask>());
                showEnterTrigger[i].setEventCallObject(CallTriggerObject);
            }
        }
        for (int i = 0; i < hideTrigger.Count; i++)
        {
            if (hideTrigger[i].GetComponent<TriggerEnterMask>())
            {
                hideEnterTrigger.Add(hideTrigger[i].GetComponent<TriggerEnterMask>());
                hideEnterTrigger[i].setEventCallObject(CallTriggerObject);
            }
        }
        for (int i = 0; i < resetTrigger.Count; i++)
        {
            if (resetTrigger[i].GetComponent<TriggerEnterMask>())
            {
                resetEnterTrigger.Add(resetTrigger[i].GetComponent<TriggerEnterMask>());
                resetEnterTrigger[i].setEventCallObject(CallTriggerObject);
            }
        }
    }

    void SerachAllHideObjAndTrigger(Transform nowTransform)
    {
        for (int i = 0; i < nowTransform.childCount; i++)
        {
            Transform childTransform = nowTransform.GetChild(i);
            if (childTransform.tag.CompareTo(canBeHideTagString) == 0)//能隐藏的对象
            {
                canHideObj.Add(childTransform.gameObject);
                continue;
            }else if(childTransform.tag.CompareTo(showTriggerString) == 0)
            {
                //遮罩显示触发
                showTrigger.Add(childTransform.gameObject);
            }
            else if (childTransform.tag.CompareTo(hideTriggerString) == 0)
            {
                //遮罩隐藏触发
                hideTrigger.Add(childTransform.gameObject);
            }
            else if (childTransform.tag.CompareTo(resetTriggerString) == 0)
            {
                //遮罩重设触发
                resetTrigger.Add(childTransform.gameObject);
            }
            else if (childTransform.tag.CompareTo(wallMaskString) == 0)
            {
                //遮罩
                masks.Add(transform.gameObject);
            }
            SerachAllHideObjAndTrigger(childTransform);//找儿子
        }
    }
    void showEvent()
    {
        Debug.Log("show");
    }
    void hideEvent()
    {
        Debug.Log("hide");
    }
    void resetEvent()
    {
        Debug.Log("reset");
    }

    private float chekcTimeDis=1.0f;
    private float lastChekcTime;
    void Update()
    {
        if (chekcTimeDis <= lastChekcTime)
        {
            for (int i = 0; i < showTrigger.Count; i++)
            {
                if (showEnterTrigger[i] && showEnterTrigger[i].isTriggerEnter)
                {
                    showEnterTrigger[i].isTriggerEnter = false;
                    showEvent();
                    break;
                }
            }
            for (int i = 0; i < hideTrigger.Count; i++)
            {
                if (hideEnterTrigger[i] && hideEnterTrigger[i].isTriggerEnter)
                {
                    hideEnterTrigger[i].isTriggerEnter = false;
                    hideEvent();
                    break;
                }
            }
            for (int i = 0; i < resetTrigger.Count; i++)
            {

                if (resetEnterTrigger[i] && resetEnterTrigger[i].isTriggerEnter)
                {
                    resetEnterTrigger[i].isTriggerEnter = false;
                    resetEvent();
                    break;
                }
            }
        }
        else lastChekcTime += Time.deltaTime;    
    }
}
