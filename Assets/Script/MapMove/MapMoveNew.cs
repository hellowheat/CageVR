using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoveNew : MonoBehaviour
{
    public List<GameObject> MMap;//地图节点
    public List<Vector2Int> MMapOrder;//地图顺序
    public List<GameObject> noneMap;//-1地图对象
    public float mapSize = 160;
    public GameObject playerObject;//监听角色
    public int startLoopMapIndex;//进入循环地图节点
    public List<int> loopMapIndex;//循环地图节点
    public int startMapIndex;//角色初始节点
    List<int[]> MMapRelation;//地图之间的连接

    int nowIndex;//当前地图中心节点
    Vector4 nowIndexNear;//当前地图四方向节点

    Vector3[] offset = { new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(0, 0, -1), new Vector3(-1, 0, 0) };
    bool allowUpdate = false;
    void Start()
    {
        nowIndex = startMapIndex;
        calcRelation();
        updateMMap();
        allowUpdate = true;
    }

    void Update()
    {
        if (playerObject && allowUpdate)
        {
            float disX = playerObject.transform.position.x - MMap[nowIndex].transform.position.x;
            float disZ = playerObject.transform.position.z - MMap[nowIndex].transform.position.z;
            if (disZ > mapSize/2) moveNextMMap(0);
            else if (disX > mapSize / 2) moveNextMMap(1);
            else if (disZ < -mapSize / 2) moveNextMMap(2);
            else if (disX < -mapSize / 2) moveNextMMap(3);
        }
    }


    //根据地图顺序更新地图连接，在地图顺序MMapOrder变化时才调用
    void calcRelation()
    {
        int[][] fturn = { new int[] { 0, -1 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } };
        //断开全部连接重新计算
        MMapRelation = new List<int[]>();
        for (int i = 0; i < MMap.Count; i++) MMapRelation.Add(new int[4] { -1, -1, -1, -1 });

        //计算标准路线连接
        for (int i=0; i< MMapOrder.Count; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                int pIndex = MMapOrder[i][j];
                if (pIndex == -1) continue;
                for(int k = 0; k < 4; k++)
                {
                    int ti = i + fturn[k][0];
                    int tj = j + fturn[k][1];
                    if (ti >= MMapOrder.Count || ti < 0 || tj >= 2 || tj < 0) continue;
                    MMapRelation[pIndex][k] = MMapOrder[ti][tj];
                }
            }
        }

        //加入循环路线连接
        int lastIndex = startLoopMapIndex;//上个连接的循环节点
        if(lastIndex != -1)
        {
            for(int i = 0; i < MMap.Count; i++)
            {
                if(loopMapIndex.IndexOf(i) != -1)//该节点是循环节点
                {
                    MMapRelation[i][0] = MMapRelation[i][2] = -1;
                    MMapRelation[i][3] = lastIndex;
                    MMapRelation[lastIndex][1] = i;
                    MMapRelation[i][1] = startLoopMapIndex;
                    lastIndex = i;
                }
            }
        }

        //更新临近节点
        for(int i=0;i<4;i++)
            nowIndexNear[i] = MMapRelation[nowIndex][i];
    }

    //向指定方向移动，会更新四方向
    void moveNextMMap(int goalDirction)
    {
        int fromIndex = nowIndex;
        if (MMapRelation[nowIndex][goalDirction] == -1) return;

        //移动当前节点
        nowIndex = (int)nowIndexNear[goalDirction];

        int[] turnDirection = { 2, 3, 0, 1 };//相反方向序号
        //更新非上个地图的其他三方向
        for(int i = 0; i < 4; i++)
        {
            if (i == turnDirection[goalDirction]) nowIndexNear[i] = fromIndex;
            else nowIndexNear[i] = MMapRelation[nowIndex][i];
        }

        //刷新地图
        updateMMap();
    }

    //根据地图四方向和中心更新位置显示
    void updateMMap()
    {
        for (int i = 0; i < MMap.Count; i++)
        {
            if (i != nowIndex && i != nowIndexNear.x && i != nowIndexNear.y && i != nowIndexNear.z && i != nowIndexNear.w)
                MMap[i].SetActive(false);
            else MMap[i].SetActive(true);
        }
        
        for(int i = 0; i < 4; i++)
        {
            int offsetIndex = (int)nowIndexNear[i];
            if (offsetIndex != -1)
            {
                MMap[offsetIndex].transform.position = MMap[nowIndex].transform.position + (offset[i] * mapSize);
                MMap[offsetIndex].SetActive(true);
            }
            else
            {
                if (noneMap.Count != 0)
                {
                    noneMap[i].transform.position = MMap[nowIndex].transform.position + (offset[i] * mapSize);
                    noneMap[i].SetActive(true);
                }
            }
        }
        
    }
    

    public void setLoop()
    {

    }

    public void breakLoop()
    {

    }
}
