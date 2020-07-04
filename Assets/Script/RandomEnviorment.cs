using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnviorment : MonoBehaviour
{
    public GameObject[] randomType;
    public Vector2[] randomIdNumber;//每种随机数量
    public bool mapHasTop,mapHasBottom,mapHasLeft,mapHasRight,mapHasCenter;//地图有某方向
    public float roadWidth;//道路宽度
    Transform box;
    void Start()
    {
        box = new GameObject("randomEnviorment").transform;
        box.localPosition = Vector3.zero;
        box.parent = transform;

        //统计方向
        bool[] hasDir = { mapHasTop, mapHasBottom, mapHasLeft, mapHasRight, mapHasCenter };


        Random.InitState(123);
        for (int i = 0; i < randomIdNumber.Length; i++)
        {
            for(int j=0;j< randomIdNumber[i].y; j++)
            {
                GameObject gb = Instantiate(randomType[i]);
                gb.transform.parent = box;
                gb.transform.localPosition = Vector3.zero;
                

            }
            
        }
        
    }
    
    void Update()
    {
        
    }
}
