using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMask : MonoBehaviour
{
    public float showTime;
    public delegate void MaskFineshFunc();
    Material material;
    Color inColor;
    Color outColor;
    [HideInInspector]
    public bool isInMask { get; private set; }
    void Start()
    {
        material = GetComponent<Renderer>().material;
        outColor = inColor = material.color;
        inColor.a = 1;
        outColor.a = 0;
        material.color = outColor;
    }
    
    //进行一次完整遮罩
    public void StartMask(MaskFineshFunc inFunc, MaskFineshFunc outFunc)
    {
        isInMask = true;
        material.DOColor(inColor, showTime).OnComplete(()=>{ inFunc?.Invoke(); }).SetEase(Ease.OutCubic);
        material.DOColor(outColor, showTime).SetDelay(showTime).OnComplete(() => { outFunc?.Invoke(); isInMask = false; }).SetEase(Ease.InCubic);
    }
    
}
