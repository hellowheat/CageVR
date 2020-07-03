using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractorType
{
    PICK,//抓取
    DIALOGUE,//对话
    OPEN,//打开（门）
    CHOOSE//选择（对话框）
}
public class InteractorObject: MonoBehaviour
{

    public Material nearMaterial;//靠近时的材质
    public Renderer rd;//渲染器，不写的话自动识别
    public string interactorPath;//交互文本信息
    public InteractorType interactorType;//交互类型
    protected Material noramlMaterial;//一般材质
    public Transform TransformInstance { get { return transform; } }

    [HideInInspector]
    public string interactorInfo;//交互文本信息

    void Start()
    {
        try
        {
            if (rd == null){rd = GetComponent<Renderer>();}
            noramlMaterial = rd.sharedMaterial;
        }
        catch { }
        interactorInfo = LanguageManager.getInstance().getLanguageString("interactor_"+interactorPath);
    }

    //被靠近时候
    public void beNearTriggerEnter()
    {
        if (nearMaterial)
        {
            rd.material = nearMaterial;
        }
    }

    //远离时
    public void beNearTriggerExit()
    {

    }

    //被指向时
    public void bePointEnter()
    {
        if (nearMaterial)
        {
            rd.material = nearMaterial;
        }
    }

    //被取消指向时
    public void bePointExit()
    {
        if (noramlMaterial)
        {
            rd.material = noramlMaterial;
        }
    }

    //被交互
    public void beInteractorEnter()
    {

    }

    //被结束交互
    public void beInteractorExit()
    {

    }
}
