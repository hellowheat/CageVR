using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InteractorTriggerEventType
{
    ENTER,EXIT
}

//交互器的触发器事件
public class InteractorTriggerEvent : MonoBehaviour
{
    public delegate void TriggerCallBackFucntion(Collider collider, InteractorTriggerEventType type);

    TriggerCallBackFucntion enterCallBack;
    TriggerCallBackFucntion exitCallBack;

    public void setEnterCallBack(TriggerCallBackFucntion triggerEnterEvent)
    {
        enterCallBack = triggerEnterEvent;
    }

    public void setExitCallBack(TriggerCallBackFucntion triggerExitEvent)
    {
        exitCallBack = triggerExitEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        enterCallBack?.Invoke(other,InteractorTriggerEventType.ENTER);
    }
    private void OnTriggerExit(Collider other)
    {
        exitCallBack?.Invoke(other, InteractorTriggerEventType.ENTER);
    }
}
