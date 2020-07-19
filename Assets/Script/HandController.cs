using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum RotateDirection
{
    X_Plus, X_Minus,
    Y_Plus, Y_Minus,
    Z_Plus, Z_Minus
}

public class HandController : MonoBehaviour
{
    public float rotateTime = 0.1f;

    [Header("thumb")]
    public GameObject thumbRotateNode;
    public RotateDirection thumbDirection;
    public float thumbNoHoldAngle;
    public float thumbHoldAngle;
    public float thumbPressAngle;

    [Header("thumbNode")]
    public GameObject thumbNodeRotateNode;
    public RotateDirection thumbNodeDirection;
    public float thumbNodeNoHoldAngle;
    public float thumbNodeHoldAngle;
    public float thumbNodePressAngle;

    [Header("index")]
    public GameObject indexRotateNode;
    public RotateDirection indexDirection;
    public float indexNoHoldAngle;
    public float indexHoldAngle;
    public float indexPressAngle;

    [Header("indexNode")]
    public GameObject indexNodeRotateNode;
    public RotateDirection indexNodeDirection;
    public float indexNodeNoHoldAngle;
    public float indexNodeHoldAngle;
    public float indexNodePressAngle;

    [Header("middle")]
    public GameObject middleRotateNode;
    public RotateDirection middleDirection;
    public float middleNoHoldAngle;
    public float middleHoldAngle;
    public float middlePressAngle;

    [Header("middleNode")]
    public GameObject middleNodeRotateNode;
    public RotateDirection middleNodeDirection;
    public float middleNodeNoHoldAngle;
    public float middleNodeHoldAngle;
    public float middleNodePressAngle;
    
    void Start()
    {
        SetThumbNoHold();
        SetIndexNoHold();
        SetMiddleNoHold();
    }
    
    void Update()
    {
        
    }

    public void SetThumbNoHold()
    {
        thumbRotateNode.transform.DOLocalRotate(getDirection(thumbDirection) * thumbNoHoldAngle, rotateTime);
        thumbNodeRotateNode.transform.DOLocalRotate(getDirection(thumbNodeDirection) * thumbNodeNoHoldAngle, rotateTime);
    }

    public void SetThumbHold()
    {
        thumbRotateNode.transform.DOLocalRotate(getDirection(thumbDirection) * thumbHoldAngle, rotateTime);
        thumbNodeRotateNode.transform.DOLocalRotate(getDirection(thumbNodeDirection) * thumbNodeHoldAngle, rotateTime);
    }

    public void SetThumbPress()
    {
        thumbRotateNode.transform.DOLocalRotate(getDirection(thumbDirection) * thumbPressAngle, rotateTime);
        thumbNodeRotateNode.transform.DOLocalRotate(getDirection(thumbNodeDirection) * thumbNodePressAngle, rotateTime);
    }

    public void SetIndexNoHold()
    {
        indexRotateNode.transform.DOLocalRotate(getDirection(indexDirection) * indexNoHoldAngle, rotateTime);
        indexNodeRotateNode.transform.DOLocalRotate(getDirection(indexNodeDirection) * indexNodeNoHoldAngle, rotateTime);
    }

    public void SetIndexHold()
    {
        indexRotateNode.transform.DOLocalRotate(getDirection(indexDirection) * indexHoldAngle, rotateTime);
        indexNodeRotateNode.transform.DOLocalRotate(getDirection(indexNodeDirection) * indexNodeHoldAngle, rotateTime);
    }

    public void SetIndexPress()
    {
        indexRotateNode.transform.DOLocalRotate(getDirection(indexDirection) * indexPressAngle, rotateTime);
        indexNodeRotateNode.transform.DOLocalRotate(getDirection(indexNodeDirection) * indexNodePressAngle, rotateTime);
    }


    public void SetMiddleNoHold()
    {
        middleRotateNode.transform.DOLocalRotate(getDirection(middleDirection) * middleNoHoldAngle, rotateTime);
        middleNodeRotateNode.transform.DOLocalRotate(getDirection(middleNodeDirection) * middleNodeNoHoldAngle, rotateTime);
    }

    public void SetMiddleHold()
    {
        middleRotateNode.transform.DOLocalRotate(getDirection(middleDirection) * middleHoldAngle, rotateTime);
        middleNodeRotateNode.transform.DOLocalRotate(getDirection(middleNodeDirection) * middleNodeHoldAngle, rotateTime);
    }

    public void SetMiddlePress()
    {
        middleRotateNode.transform.DOLocalRotate(getDirection(middleDirection) * middlePressAngle, rotateTime);
        middleNodeRotateNode.transform.DOLocalRotate(getDirection(middleNodeDirection) * middleNodePressAngle, rotateTime);
    }


    Vector3 getDirection(RotateDirection rotateDirection)
    {
        switch (rotateDirection)
        {
            case RotateDirection.X_Plus:return Vector3.right;
            case RotateDirection.X_Minus:return Vector3.left;
            case RotateDirection.Y_Plus: return Vector3.up;
            case RotateDirection.Y_Minus: return Vector3.down;
            case RotateDirection.Z_Plus: return Vector3.forward;
            case RotateDirection.Z_Minus: return Vector3.back;
        }
        return Vector3.zero;
    }
}
