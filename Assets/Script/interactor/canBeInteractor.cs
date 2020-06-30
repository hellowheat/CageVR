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
public class canBeInteractor: MonoBehaviour
{

    public Material nearPick;//靠近时的材质
    public string interactorName;//自己交互用的名字
    public InteractorType interactorType;//交互类型

    [HideInInspector]
    public string interactorHit;//交互提示，用于反馈给交互器
    private Material noramlMaterial;//一般材质

    void Start()
    {
        try
        {
            noramlMaterial = GetComponent<Renderer>().material;
        }
        catch { }
        
    }

    //被靠近时候
    public void beNearEnter()
    {

    }

    //远离时
    public void beNearExit()
    {

    }

    public void beStartInteractor()
    {

    }


}
