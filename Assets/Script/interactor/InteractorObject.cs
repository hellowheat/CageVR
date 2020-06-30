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
    public string interactorName;//自己交互用的名字
    public InteractorType interactorType;//交互类型

    [HideInInspector]
    public string interactorHit;//交互提示，用于反馈给交互器
    private Renderer rd;
    private Material noramlMaterial;//一般材质

    void Start()
    {
        try
        {
            rd = GetComponent<Renderer>();
            noramlMaterial = rd.material;
        }
        catch { }
        
    }

    //被靠近时候
    public void beNearEnter()
    {
        if (nearMaterial)
        {
            rd.material = nearMaterial;
        }
    }

    //远离时
    public void beNearExit()
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
    public void beStartInteractor()
    {

    }

    //被结束交互
    public void beEndInteractor()
    {

    }
}
