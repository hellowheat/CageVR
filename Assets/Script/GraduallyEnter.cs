using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GraduallyEnter : MonoBehaviour
{
    Image graduallyMask;
    public float graduallyTime;

    void Start()
    {
        graduallyMask = GetComponent<Image>();
        if (graduallyMask != null)
        {
            Color endColor = graduallyMask.color;
            endColor.a = 0;
            graduallyMask.DOColor(endColor, graduallyTime);
        }

    }
    
    void Update()
    {
        
    }
}
