using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIObject : InteractorObject
{
    ShowDialogue showDialogue;
    public override void Start()
    {
        base.Start();
        showDialogue = transform.Find("dialogue")?.GetComponent<ShowDialogue>();
    }

    public override void beInteractorEnter(Interactor interactor, RaycastHit hit)
    {
        if (showDialogue)
        {
            if (showDialogue.isDialogueEnd)
            {
                showDialogue.StartDialog();
            }
            else
            {
                showDialogue.clickDialog(-1);
            }
        }
        interactor.tryToStopInteractor(this);
    }

    public override void beInteractorExit(Interactor interactor)
    {
        
    }
}
