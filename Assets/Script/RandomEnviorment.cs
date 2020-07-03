using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnviorment : MonoBehaviour
{
    public List<GameObject> randomType;
    public List<Vector2> randomIdNumber;//随机数量
    public bool mapHasTop,mapHasBottom,mapHasLeft,mapHasRight,mapHasCenter;//地图有某方向
    GameObject box;
    void Start()
    {
        box = new GameObject("randomEnviorment");
        box.transform.localPosition = Vector3.zero;
        box.transform.parent = transform;
        
    }
    
    void Update()
    {
        
    }
}
