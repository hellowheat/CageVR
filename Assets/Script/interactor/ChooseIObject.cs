
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseIObject : InteractorObject
{
    public ShowDialogue showDialogue;
    public int chooseId;
    public override void Start()
    {
        base.Start();
        interactorInfo = LanguageManager.getInstance().getLanguageString("interactor_" + interactorPath  + chooseId);
    }

    public override void beInteractorEnter(Interactor interactor, RaycastHit hit)
    {

    }

    public override void beInteractorExit(Interactor interactor)
    {
        showDialogue.clickDialog(chooseId - 1);
    }
}
