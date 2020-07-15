using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMoveAction : MonoBehaviour
{
    public GameObject Player;
    public GameObject moveHit;
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
    Animator moveAnimator;
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

        moveAnimator = moveHit.transform.Find("animation").GetComponent<Animator>();
    }


    public void startShowMove()
    {
        isShow = true;
        lineBox.SetActive(true);
    }

    public void endShowMove()
    {
        isShow = false;
        lineBox.SetActive(false);
        moveHit.SetActive(false);

        MoveToShow();
    }

    public void MoveToShow()
    {
        if(hit.transform.tag.CompareTo("CanMoveTo") == 0)
        {
            Player.transform.position = hit.point;
        }
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
        hit.point = Vector3.zero;

        bool beStop=false;
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

            
            if (hit.point == Vector3.zero)
            {
                Physics.Raycast(startPos, endPos - startPos, out hit, (endPos - startPos).magnitude);
                if (hit.point != Vector3.zero)
                {
                    if(hit.transform.tag.CompareTo("CanMoveTo") != 0)
                    {
                        beStop = true;
                    }
                    else
                    {
                        //找到了可以移动的
                        break;
                    }
                }
            }

            distance += (endPos - startPos).magnitude;
            startVelocity = endVelocity;
            startPos = endPos;
            showline = !showline;
            if (distance >= maxDistance) break;
        }

        moveAnimator.SetBool("stop", beStop);
        moveHit.transform.position = hit.point;
        moveHit.SetActive(true);

    }


    float showCD = 0.001f;
    float showTimeDis = 1f;
    void Update()
    {
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
