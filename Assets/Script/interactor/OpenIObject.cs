using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum doorD { x,z};
public class OpenIObject : InteractorObject
{
    public GameObject doorAixs;
    public doorD doorDir=doorD.x;
    public float openAngle = 90;
    public float peekAngle = 20;
    public int openTurn = 1;
    public float openTime = 0.2f;
    Transform _lock;
    bool isDoorOpen;
    bool isMoveEnd = true;
    string[] DoorInteractorPath = { "Open_Door", "Close_Door" };

    public override void Start()
    {
        base.Start();
        _lock = transform.Find("lock");
        isDoorOpen = false;
    }
    
    void Update()
    {
        
    }

    public override void beInteractorEnter(Interactor interactor, RaycastHit hit)
    {
        if (isDoorOpen) closeDoor();
        else openDoor(calcRotateDir(interactor.transform.position));
    }

    public override void beInteractorExit(Interactor interactor)
    {

    }

    void openDoor(int dir)
    {
        if (isMoveEnd)
        {
            isDoorOpen = true;
            interactorInfo = LanguageManager.getInstance().getLanguageString("interactor_" + DoorInteractorPath[1]);
            isMoveEnd = false;
            doorAixs.transform.DOLocalRotate(Vector3.up * openAngle * dir, openTime).OnComplete(() =>
                {
                    isMoveEnd = true;
                });
        }
    }

    public void peekDoor()
    {
        if (isMoveEnd)
        {
            isDoorOpen = true;
            interactorInfo = LanguageManager.getInstance().getLanguageString("interactor_" + DoorInteractorPath[1]);
            isMoveEnd = false;
            doorAixs.transform.DOLocalRotate(Vector3.up * peekAngle, openTime).OnComplete(() =>
            {
                isMoveEnd = true;
            });
        }
    }

    void closeDoor()
    {
        if (isMoveEnd)
        {
            isDoorOpen = false;
            interactorInfo = LanguageManager.getInstance().getLanguageString("interactor_" + DoorInteractorPath[0]);
            isMoveEnd = false;
            doorAixs.transform.DOLocalRotate(Vector3.zero, openTime).OnComplete(() =>
            {
                isMoveEnd = true;
            });
        }
    }

    int calcRotateDir(Vector3 interactorPos)
    {
        Vector3 doorPosition = transform.position;
        if(doorDir == doorD.x)
        {
            return System.Math.Sign(doorPosition.x - interactorPos.x) * openTurn;
        }
        else if(doorDir == doorD.z)
        {
            return System.Math.Sign(doorPosition.z - interactorPos.z) * openTurn ;
        }
        return 1;
    }
}
