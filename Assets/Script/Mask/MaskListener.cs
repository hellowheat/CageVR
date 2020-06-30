using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MaskListener : MonoBehaviour
{
    List<GameObject> canHideObj;
    List<GameObject> hideGameObject;//用于进入隐藏场景的Trigger
    List<GameObject> showGameObject;//用于进入显示场景的Trigger
    List<GameObject> hideResetGameObject;//用于重设场景的Trigger,由hide触发
    List<GameObject> showResetGameObject;//用于重设场景的Trigger,由show触发
    List<GameObject> masks;//用于重设场景的Trigger

    string canBeHideTagString = "CanBeMask";//可隐藏目标的渲染队列
    string enterTriggerString = "MaskEnterTrigger";
    string resetTriggerString = "MaskResetTrigger";
    string wallMaskString = "WallMask";

    List<TriggerEnterMask> showEnterTrigger = new List<TriggerEnterMask>();
    List<TriggerEnterMask> hideEnterTrigger = new List<TriggerEnterMask>();
    List<TriggerEnterMask> hideResetTrigger = new List<TriggerEnterMask>();
    List<TriggerEnterMask> showResetTrigger = new List<TriggerEnterMask>();
    List<TriggerEnterMask> allResetTrigger = new List<TriggerEnterMask>();

    [HideInInspector]
    public bool isHideOut;
    bool canCheckHide;//能进行检查
    void Start()
    {
        canHideObj = new List<GameObject>();
        hideGameObject = new List<GameObject>();
        showGameObject = new List<GameObject>();
        hideResetGameObject = new List<GameObject>();
        showResetGameObject = new List<GameObject>();
        masks = new List<GameObject>();
        isHideOut = false;
        canCheckHide = true;
        SearchAllTrigger(transform);//查找子物体中所有的Trigger
        SearchAllHideObject(transform.parent);//查找兄弟物体中所有的可隐藏对象

        for (int i = 0; i < showGameObject.Count; i++)
        {
            if (showGameObject[i].GetComponent<TriggerEnterMask>())
            {
                showEnterTrigger.Add(showGameObject[i].GetComponent<TriggerEnterMask>());
            }
        }
        for (int i = 0; i < hideGameObject.Count; i++)
        {
            if (hideGameObject[i].GetComponent<TriggerEnterMask>())
            {
                hideEnterTrigger.Add(hideGameObject[i].GetComponent<TriggerEnterMask>());
            }
        }
        for (int i = 0; i < hideResetGameObject.Count; i++)
        {
            if (hideResetGameObject[i].GetComponent<TriggerEnterMask>())
            {
                hideResetTrigger.Add(hideResetGameObject[i].GetComponent<TriggerEnterMask>());
            }
        }
        for (int i = 0; i < showResetGameObject.Count; i++)
        {
            if (showResetGameObject[i].GetComponent<TriggerEnterMask>())
            {
                showResetTrigger.Add(showResetGameObject[i].GetComponent<TriggerEnterMask>());
            }
        }
    }
    void SearchAllHideObject(Transform nowTransform)
    {

        for (int i = 0; i < nowTransform.childCount; i++)
        {
            Transform childTransform = nowTransform.GetChild(i);
            if (childTransform.tag.CompareTo(canBeHideTagString) == 0)//能隐藏的对象
            {
                canHideObj.Add(childTransform.gameObject);
                continue;
            }
            SearchAllHideObject(nowTransform.GetChild(i));
        }
    }

    void SearchAllTrigger(Transform nowTransform)
    {
        for (int i = 0; i < nowTransform.childCount; i++)
        {
            Transform childTransform = nowTransform.GetChild(i);
            if (childTransform.tag.CompareTo(enterTriggerString) == 0)
            {
                //找到进入遮罩
                if (childTransform.name.ToLower().IndexOf("show") != -1)
                {   
                    //show类型遮罩
                    showGameObject.Add(childTransform.gameObject);
                }else if (childTransform.name.ToLower().IndexOf("hide") != -1)
                {
                    //hide类型遮罩
                    hideGameObject.Add(childTransform.gameObject);
                }
            }
            else if (childTransform.tag.CompareTo(resetTriggerString) == 0)
            {
                //找到重设遮罩
                if (childTransform.name.ToLower().IndexOf("show") != -1)
                    showResetGameObject.Add(childTransform.gameObject);
                else if (childTransform.name.ToLower().IndexOf("hide") != -1)
                    hideResetGameObject.Add(childTransform.gameObject);
                else allResetTrigger.Add(childTransform.gameObject.GetComponent<TriggerEnterMask>());
            }
            else if (childTransform.tag.CompareTo(wallMaskString) == 0)
            {
                //遮罩
                masks.Add(childTransform.gameObject);
            }
            SearchAllTrigger(childTransform);//找儿子
        }
    }
    void showEvent()
    {
        if (canCheckHide)
        {
            Debug.Log("showEvent");
            canCheckHide = false;
            isHideOut = false;
            showAllHideObject_HideMask();
        }
    }
    void hideEvent()
    {
        if (canCheckHide)
        {
            Debug.Log("hideEvent");
            canCheckHide = false;
            isHideOut = true;
            hideAllHideObject_ShowMask();
        }
    }
    void hideResetEvent()
    {
        if (!canCheckHide && isHideOut)
        {
            Debug.Log("hideResetEvent");
            canCheckHide = true;
            isHideOut = false;
            showAllHideObject_ShowMask();
        }
    }

    void showResetEvent()
    {
        if (!canCheckHide && !isHideOut)
        {
            Debug.Log("showResetEvent");
            canCheckHide = true;
            isHideOut = false;
            showAllHideObject_ShowMask();
        }
    }
    void allResetEvent()
    {
        Debug.Log("allResetEvent");
        canCheckHide = true;
        isHideOut = false;
        showAllHideObject_ShowMask();
    }

    void showAllHideObject_HideMask()
    {
        for(int i = 0; i < masks.Count; i++)
        {
            masks[i].SetActive(false);
        }
        for (int i = 0; i < canHideObj.Count; i++)
        {
            canHideObj[i].SetActive(true);
        }
    }

    void hideAllHideObject_ShowMask()
    {
        for (int i = 0; i < masks.Count; i++)
        {
            masks[i].SetActive(true);
        }
        for (int i = 0; i < canHideObj.Count; i++)
        {
            canHideObj[i].SetActive(false);
        }
    }

    void showAllHideObject_ShowMask()
    {
        for (int i = 0; i < masks.Count; i++)
        {
            masks[i].SetActive(true);
        }
        for (int i = 0; i < canHideObj.Count; i++)
        {
            canHideObj[i].SetActive(true);
        }
    }

    private float chekcTimeDis=1.0f;//每隔一秒检测更新一次
    private float lastChekcTime;
    void Update()
    {
        if (chekcTimeDis <= lastChekcTime)
        {
            
            for (int i = 0; i < showGameObject.Count; i++)
            {
                if (showEnterTrigger[i] && showEnterTrigger[i].isTriggerEnter)
                {
                    showEnterTrigger[i].isTriggerEnter = false;
                    showEvent();
                    break;
                }
            }
            for (int i = 0; i < hideGameObject.Count; i++)
            {
                if (hideEnterTrigger[i] && hideEnterTrigger[i].isTriggerEnter)
                {
                    hideEnterTrigger[i].isTriggerEnter = false;
                    hideEvent();
                    break;
                }
            }
            for (int i = 0; i < hideResetTrigger.Count; i++)
            {

                if (hideResetTrigger[i] && hideResetTrigger[i].isTriggerEnter)
                {
                    hideResetTrigger[i].isTriggerEnter = false;
                    hideResetEvent();
                    break;
                }
            }
            for (int i = 0; i < showResetTrigger.Count; i++)
            {

                if (showResetTrigger[i] && showResetTrigger[i].isTriggerEnter)
                {
                    showResetTrigger[i].isTriggerEnter = false;
                    showResetEvent();
                    break;
                }
            }
            for (int i = 0; i < allResetTrigger.Count; i++)
            {

                if (allResetTrigger[i] && allResetTrigger[i].isTriggerEnter)
                {
                    allResetTrigger[i].isTriggerEnter = false;
                    allResetEvent();
                    break;
                }
            }
        }
        else lastChekcTime += Time.deltaTime;    
    }
}
