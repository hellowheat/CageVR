using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMoveAction : MonoBehaviour
{
    public float velocity;
    public float dropVelocity;
    public float maxDistance;
    [Header("line")]
    public Material material;
    public float lineWidth;
    public float lineHideTime;
    public float lineShowTime;
    GameObject lineBox;
    ObjectPool linePool;
    List<GameObject> lineList;
    bool isShow;
    RaycastHit hit;
    Vector3 lastDrawPosition;
    Vector3 lastDrawForward;
    void Start()
    {
        isShow = false;

        lineBox = new GameObject("moveLine");
        lineBox.transform.position = Vector3.zero;

        GameObject line = new GameObject("line");
        line.SetActive(false);
        line.transform.parent = lineBox.transform;
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false;
        lineRenderer.material = material;
        linePool = new ObjectPool(line, lineBox.transform, 20);

        lineList = new List<GameObject>();
    }


    public void startShowMove()
    {
        Debug.Log("set show true");
        isShow = true;
        lineBox.SetActive(true);
    }

    public void endShowMove()
    {
        Debug.Log("set show false");
        isShow = false;
        lineBox.SetActive(false);
        MoveToShow();
    }

    public void MoveToShow()
    {

    }

    void updateShow()
    {
        if (lastDrawPosition == transform.position && lastDrawForward == transform.forward)
            return;
        lastDrawPosition = transform.position;
        lastDrawForward = transform.forward;

        while (lineList.Count > 0)
        {
            linePool.destory(lineList[0]);
            lineList.RemoveAt(0);
        }

        Vector3 startPos = transform.position;
        Vector3 startVelocity = transform.forward * velocity;
        Vector3 a = new Vector3(0, dropVelocity, 0);
        bool showline = true;
        float distance = 0;
        while (true)
        {
            Vector3 endVelocity = startVelocity + a;
            Vector3 endPos = startPos + (endVelocity + startVelocity) * lineShowTime / 2;
            
            if (showline)
            {
                //画线
                GameObject line = linePool.create(Vector3.zero, lineBox.transform.forward);
                if (line)
                {
                    LineRenderer lineRender = line.GetComponent<LineRenderer>();
                    lineRender.SetPosition(0, startPos);
                    lineRender.SetPosition(1, endPos);
                    lineList.Add(line);
                }
            }
            else
            {
                //不画线

            }
            distance += (endPos - startPos).magnitude;
            startVelocity = endVelocity;
            startPos = endPos;
            showline = !showline;
            if (distance >= maxDistance) break;
        }

    }


    float showCD = 0.016f;
    float showTimeDis = 1f;
    void Update()
    {
        Debug.Log(isShow);
        if (isShow)
        {
            if (showTimeDis >= showCD)
            {
                showTimeDis = 0;
                updateShow();
            }
            else showTimeDis += Time.deltaTime;
        }
    }
}
