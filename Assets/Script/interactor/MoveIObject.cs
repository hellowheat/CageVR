using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIObject : InteractorObject
{
    public Transform moveObj;
    public GameObject moveHit;

    public override void bePointEnter(Interactor interactor, RaycastHit hit)
    {
        base.bePointEnter(interactor, hit);
        if (moveHit)
        {
            moveHit.SetActive(true);
            moveHit.transform.position = hit.point+Vector3.up;
            moveHit.transform.forward = hit.normal;
        }
    }

    public override void bePointExit(Interactor interactor)
    {
        base.bePointExit(interactor);
        if (moveHit)
        {
            moveHit.SetActive(false);
        }
    }

    public override void beInteractorEnter(Interactor interactor, RaycastHit hit)
    {
        Vector3 movePos = hit.point;
        movePos.y = moveObj.position.y;
        moveObj.position = movePos;
    }
}
