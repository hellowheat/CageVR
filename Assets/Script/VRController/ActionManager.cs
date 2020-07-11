using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager
{
    private ActionManager(){}


    public bool isTeleportSlowPress { get; private set; }
    public bool isPress { get; private set; }
    public bool isTeleportFastPress { get; private set; }

    static private ActionManager instance = new ActionManager();
    static public ActionManager getInstance()
    {
        return instance;
    }
}
