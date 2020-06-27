using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public GameObject[] map;
    public List<Vector2Int> orders;
    public bool hasUpdate;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMapByOrder();
    }

    // Update is called once per frame
    void UpdateMapByOrder()
    {
        for (int i = 0; i < map.Length; i++) map[i].SetActive(false);
        int[] indexs = new int[2];
        for(int i = 0; i < orders.Count; i++)
        {
            indexs[0] = orders[i].x;
            indexs[1] = orders[i].y;
            for (int j = 0; j < 2; j++)
            {
                if (indexs[j] == -1) continue;
                float xBounds= map[indexs[j]].transform.Find("floor").GetComponent<Renderer>().bounds.size.x;
                float zBounds = map[indexs[j]].transform.Find("floor").GetComponent<Renderer>().bounds.size.z;
                map[indexs[j]].transform.position = new Vector3(-xBounds * i, 0, -zBounds * j);
                map[indexs[j]].SetActive(true);

            }
        }
    }

    void Update()
    {
        if (hasUpdate)
        {
            hasUpdate = false;
            UpdateMapByOrder();
        }
        
    }
}
