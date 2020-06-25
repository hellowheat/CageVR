using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum MCFaceType
{
    front,
    back,
    top,
    bottom,
    left,
    right
}
public class MagicCube
{
    public MCFace[] mcFace { get; set; }
    public MagicCube()
    {
        mcFace = new MCFace[6];
    }
}
