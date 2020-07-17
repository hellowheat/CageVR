using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMoveAction : MonoBehaviour
{
    public GameObject Player;
    public GameObject moveHit;
    public CameraMask moveEyeMask;
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
        if(hit.transform && hit.transform.tag.CompareTo("CanMoveTo") == 0)
        {
            moveEyeMask.StartMask(() =>
            {
                Player.transform.Translate((hit.point-transform.position) - new Vector3(0, (hit.point - transform.position).y, 0), Space.World);
                // Player.transform.position = hit.point + new Vector3(0, -hit.point.y, 0) ;
            }
            , null);
        }
    }

    public void MoveBack()
    {
        Vector3 offsetDir = Camera.main.transform.forward * -1;
        offsetDir.y = 2;

        RaycastHit backHit;
        float[] offsetDis = { 6, 4, 2, 1 };
        for(int i = 0; i < offsetDis.Length; i++)
        {
            Vector3 offsetPos = Player.transform.position + offsetDir.normalized * offsetDis[i];
            Physics.Raycast(offsetPos, Vector3.down, out backHit, maxDistance);
            if (backHit.transform.tag.CompareTo("CanMoveTo") == 0)
            {
                moveEyeMask.StartMask(() =>
                {
                    Player.transform.position = backHit.point + new Vector3(0, -backHit.point.y, 0);
                }
                , null);
                break;
            }
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
        }//清空已绘制的线
        
        Vector3 startPos = transform.position;//射线初始位置
        Vector3 startVelocity = transform.forward * velocity;//初始速度
        Vector3 a = new Vector3(0, dropVelocity, 0);//重力的加速度
        float highRotate = Vector3.Angle(startVelocity, startVelocity - new Vector3(0, startVelocity.y, 0));//高度方向的角度


        bool showline = true;//交替绘制时，本次是否绘制标示
        float distance = 0;//已绘制长度

        bool beStop=false;
        hit.point = Vector3.zero;
        while (true)//持续画线
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
                Physics.Raycast(startPos, endPos - startPos, out hit, (endPos - startPos).magnitude, ~(1 << 9));
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
            if (distance >= maxDistance) break;//超过最大长度
            if (highRotate > 45) break;//移动夹角大于45度也不用绘制了
        }

        moveAnimator.SetBool("stop", beStop);
        moveHit.SetActive(true);
        moveHit.transform.position = hit.point;

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
