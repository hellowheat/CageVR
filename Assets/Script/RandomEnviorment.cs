using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnviorment : MonoBehaviour
{
    [Header("Bron Setting")]
    public int seed;//随机种子
    public float roadInnerWidth;//道路内宽度
    public float roadOuterWidth;//道路外宽度
    public float mapWidth;//地图宽度
    public float offsetZ;//高度偏移
    public bool mapHasTop,mapHasBottom,mapHasLeft,mapHasRight,mapHasCenter;//地图有某方向
    [Header("Bron Object")]
    public GameObject[] randomType;
    public float[] randomIdNumber;//每种随机数量
    public float[] awayCenter;//远离中心距离，此项控制其不在道路中心生成，树不会长在路中间
    Transform box;
    void Start()
    {
        box = new GameObject("randomEnviorment").transform;
        box.parent = transform; 
        box.localPosition = new Vector3(0, offsetZ, 0);

        

        /*随机生成植物装饰*/
        Random.InitState(seed);//初始化种子
        //统计方向
        bool[] hasDir = { mapHasTop, mapHasBottom, mapHasLeft, mapHasRight, mapHasCenter };
        Vector2[] dirOffset = {
            new Vector2(1,0),
            new Vector2(-1,0),
            new Vector2(0,1),
            new Vector2(0,-1)
        };
        int bronAreaNumber = 0 ;
        for (int i = 0; i < hasDir.Length; i++) if (hasDir[i]) bronAreaNumber++;
        if (bronAreaNumber > 0)//可以生成，不会陷入死循环
        {
            for (int i = 0; i < randomIdNumber.Length; i++)
            {
                for (int j = 0; j < randomIdNumber[i]; j++)
                {
                    if (awayCenter[i] >= roadInnerWidth / 2) continue;//离道路中心距离大于道路宽度，不用生成
                    GameObject gb = Instantiate(randomType[i]);
                    gb.transform.parent = box;
                    gb.transform.Rotate(Vector3.up, Random.Range(0, 290));
                    gb.transform.localPosition = Vector3.zero;

                    /*float posX, posZ;
                    //随机确定位置
                    while (true)
                    {
                         posX = Random.Range(-mapWidth / 2, mapWidth / 2);
                        if(Mathf.Abs(posX) <= roadInnerWidth)posZ = Random.Range(-mapWidth / 2, mapWidth / 2);
                        else posZ = Random.Range(-roadInnerWidth / 2, roadInnerWidth / 2);


                        int area = calcArea(posX,posZ);
                        //    Debug.Log(area);
                        if(area != -1 && hasDir[area])//在可用区域中
                        {
                            float disCenter = Mathf.Min(Mathf.Abs(posX), Mathf.Abs(posZ));
                            if(disCenter >= awayCenter[i])//没有距离道路中心太近
                            {
                                break;
                            }
                        }
                    }*/
                    int ranArea = randBronArea(hasDir);
                    Vector2 pos = randBronInArea(ranArea,awayCenter[i]);
                    gb.transform.localPosition = new Vector3(pos.x, 0, pos.y);
                }

            }
        }
    }
    
    int calcArea(float posX,float posZ)
    {
        int area = -1;
        if(Mathf.Abs(posZ) < roadInnerWidth/2 && posX >= roadOuterWidth/2)
        {
            area = 0;
        }
        else if (Mathf.Abs(posZ) < roadInnerWidth/2 && posX <= -roadOuterWidth/2)
        {
            area = 1;
        }
        else if (Mathf.Abs(posX) < roadInnerWidth / 2 && posZ >= roadOuterWidth / 2)
        {
            area = 2;
        }
        else if (Mathf.Abs(posX) < roadInnerWidth / 2 && posZ <= -roadOuterWidth / 2)
        {
            area = 3;
        }else if((Mathf.Abs(posX) <= roadInnerWidth/2 && Mathf.Abs(posZ) <roadOuterWidth/2) ||
            (Mathf.Abs(posX) <= roadInnerWidth / 2 && Mathf.Abs(posZ) < roadOuterWidth / 2))
        {
            return 4;
        }
            return area;
    }

    int randBronArea(bool[] hasDir)
    {
        int numDir = 0;
        for (int i = 0; i < hasDir.Length; i++) if (hasDir[i]) numDir++;
        if (numDir == 0) return 0;
        int random = Random.Range(0, numDir);
        for (int i = 0; i < hasDir.Length; i++)
        {
            if (hasDir[i])
            {
                if (random <= 0) return i;
                random--;
            }
        }
        return 0;
    }

    Vector2 randBronInArea(int areaIndex,float awayC)
    {
        Vector2 pos = new Vector2();
        if (areaIndex == 0)
        {
            pos.x = Random.Range(roadOuterWidth / 2, mapWidth / 2);
            pos.y = Random.value>=0.5f?
                Random.Range(awayC, roadInnerWidth / 2):
                Random.Range(-roadInnerWidth / 2, -awayC);
        }else if(areaIndex == 1)
        {
            pos.x = Random.Range(-mapWidth / 2, -roadOuterWidth / 2);
            pos.y = Random.value>=0.5f?
                Random.Range(awayC, roadInnerWidth / 2):
                Random.Range(-roadInnerWidth / 2, -awayC);
        }else if(areaIndex == 2)
        {
            pos.x =Random.value>=0.5f?
                pos.x = Random.Range(-roadInnerWidth / 2, -awayC):
                pos.x = Random.Range(awayC, roadInnerWidth / 2);
            pos.y = Random.Range(roadOuterWidth / 2, mapWidth / 2);
        }else if(areaIndex == 3)
        {
            pos.x = Random.value >= 0.5f ?
                pos.x = Random.Range(-roadInnerWidth / 2, -awayC) :
                pos.x = Random.Range(awayC, roadInnerWidth / 2);
            pos.y = Random.Range(-mapWidth / 2, -roadOuterWidth / 2);
        }else if (areaIndex == 4)
        {
            while (true)
            {
                pos.x = Random.Range(-roadOuterWidth / 2, roadOuterWidth / 2);
                if (Mathf.Abs(pos.x) <= roadInnerWidth) pos.y = Random.Range(-roadOuterWidth / 2, roadOuterWidth / 2);
                else pos.y = Random.Range(-roadInnerWidth / 2, roadInnerWidth / 2);
                if(Mathf.Abs(pos.x) > awayC && Mathf.Abs(pos.y) > awayC)
                {
                    break;
                }
            }
        }
        return pos;
    }
    
}
