using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
enum FingerState
{
    nohold,hold,press
}

[RequireComponent(typeof(SteamVR_Behaviour_Pose))]
public class VRControllerRenderer : MonoBehaviour
{
    //public Interactor interactor;
    public HandController handController;
    public SteamVR_Action_Boolean triggerBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Action_Boolean gripBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public SteamVR_Action_Boolean TeleportBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    public SteamVR_Action_Boolean TurnLeftBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("TurnLeft");
    public SteamVR_Action_Boolean TurnRightBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("TurnRight");
    public SteamVR_Action_Boolean MoveBackBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("MoveBack");
    public SteamVR_Action_Pose poseAction = SteamVR_Input.GetAction<SteamVR_Action_Pose>("Pose");
    public SteamVR_Action_Skeleton SkeletonHand = SteamVR_Input.GetAction<SteamVR_Action_Skeleton>("SkeletonLeftHand");  
    private SteamVR_Behaviour_Pose pose;

    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    
    void Update() 
    {
        // Debug.Log("上个拇指：" + SkeletonHand.lastThumbCurl + ",上个食指：" + SkeletonHand.lastIndexCurl+ ",上个中指：" + SkeletonHand.lastMiddleCurl);
        //Debug.Log("拇指：" + SkeletonHand.thumbCurl + ",食指：" + SkeletonHand.indexCurl + ",中指：" + SkeletonHand.middleCurl);
       // Debug.Log("拇指食指展开：" + Mathf.Acos(SkeletonHand.thumbIndexSplay) / 3.141592653 * 180 + ",食指中指展开：" + Mathf.Acos(SkeletonHand.indexMiddleSplay) / 3.141592653 * 180);

        
        updateThumb();
        updateIndex();
        updateMiddle();
    }

    FingerState thumbState=FingerState.nohold;
    FingerState indexState = FingerState.nohold;
    FingerState middleState = FingerState.nohold;
    float curlThreshold = 0.34f;

    void updateThumb()
    {
        FingerState nowState;

        if (TeleportBtn.GetState(pose.inputSource) || TurnLeftBtn.GetState(pose.inputSource)
            || TurnRightBtn.GetState(pose.inputSource) || MoveBackBtn.GetState(pose.inputSource))
        {
            nowState = FingerState.press;
        }
        else if (SkeletonHand.thumbCurl <= curlThreshold)
        {
            nowState = FingerState.nohold;
        }
        else nowState = FingerState.hold;

        if(thumbState != nowState)
        {
            thumbState = nowState;
            switch (nowState)
            {
                case FingerState.nohold:handController.SetThumbNoHold();break;
                case FingerState.hold:handController.SetThumbHold();break;
                case FingerState.press:handController.SetThumbPress();break;
            }
        }
    }

    void updateIndex()
    {
        FingerState nowState;

        if (triggerBtn.GetState(pose.inputSource))
        {
            nowState = FingerState.press;
        }
        else if(SkeletonHand.indexCurl <= curlThreshold)
        {
            nowState = FingerState.nohold;
        }
        else nowState = FingerState.hold;

        if (indexState != nowState)
        {
            indexState = nowState;
            switch (nowState)
            {
                case FingerState.nohold: handController.SetIndexNoHold(); break;
                case FingerState.hold: handController.SetIndexHold(); break;
                case FingerState.press: handController.SetIndexPress(); break;
            }
        }
    }

    void updateMiddle()
    {
        FingerState nowState;

        if (gripBtn.GetState(pose.inputSource))
        {
            nowState = FingerState.press;
        }
        else if(SkeletonHand.middleCurl <= curlThreshold)
        {
            nowState = FingerState.nohold;
        }
        else nowState = FingerState.hold;

        if (middleState != nowState)
        {
            middleState = nowState;
            switch (nowState)
            {
                case FingerState.nohold: handController.SetMiddleNoHold(); break;
                case FingerState.hold: handController.SetMiddleHold(); break;
                case FingerState.press: handController.SetMiddlePress(); break;
            }
        }
    }
}
