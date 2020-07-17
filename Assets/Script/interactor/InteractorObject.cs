using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorObject: MonoBehaviour
{

    public Material pointMaterial;//靠近时的材质
    public string interactorPath;//交互文本读取路径
    public Transform TransformInstance { get { return transform; } }



    [HideInInspector]
    public string interactorInfo;//交互文本信息
    private Renderer rd;//渲染器，不写的话自动识别
    protected Material noramlMaterial;//默认材质

    public virtual void Start()
    {
        try
        {
            foundRenderer(transform);
            noramlMaterial = rd.sharedMaterial;
        }
        catch { }
        interactorInfo = LanguageManager.getInstance().getLanguageString("interactor_"+interactorPath);

        
    }

    //被靠近时候
    public void beNearTriggerEnter(Interactor interactor)
    {

    }

    //远离时
    public void beNearTriggerExit(Interactor interactor)
    {

    }

    //被指向时
    public virtual void bePointEnter(Interactor interactor, RaycastHit hit)
    {
        if (pointMaterial)
        {
            rd.material = pointMaterial;
        }
    }

    //被取消指向时
    public virtual void bePointExit(Interactor interactor)
    {
        if (rd && noramlMaterial)
        {
            rd.material = noramlMaterial;
        }
    }

    //被交互
    public virtual void beInteractorEnter(Interactor interactor, RaycastHit hit)
    {

    }

    //被结束交互
    public virtual void beInteractorExit(Interactor interactor)
    {

    }

    void foundRenderer(Transform t)
    {
        rd = t.GetComponent<Renderer>();
        if(rd == null)
        {
            for(int i = 0; i < t.childCount; i++)
            {
                rd = t.GetChild(i).GetComponent<Renderer>();
                if (rd != null) break;
            }
        }
    }
}
