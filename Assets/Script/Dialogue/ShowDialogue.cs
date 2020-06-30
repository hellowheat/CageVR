using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    public string NPCid;
    public float normalShowCD;
    public List<float> extraShowCD_time;
    public List<char> extraShowCD_char;
    DialogDataReader data;
    TextMesh noramlContentMesh;
    TextMesh miniMesh_1;
    TextMesh miniMesh_2;

    GameObject normalGB;
    GameObject miniGB;
    GameObject clickHitGB;

    GameObject chooseBox;
    GameObject[] chooseGB;
    TextMesh[] chooseTM;

    int stringShowIndex = 0;
    const int chooseNumber = 6;
    
    // Start is called before the first frame update
    void Start()
    {
        data = new DialogDataReader(NPCid);
        chooseGB = new GameObject[chooseNumber] ;
        chooseTM = new TextMesh[chooseNumber];

        try
        {
            normalGB = transform.Find("normalContent").gameObject;
            miniGB = transform.Find("miniContent").gameObject;
            clickHitGB = normalGB.transform.Find("clickHit").gameObject;
            noramlContentMesh = normalGB.transform.Find("content").GetComponent<TextMesh>();
            miniMesh_1 = miniGB.transform.Find("content (1)").GetComponent<TextMesh>();
            miniMesh_2 = miniGB.transform.Find("content (2)").GetComponent<TextMesh>();
            chooseBox = normalGB.transform.Find("choose").gameObject;
            for (int i = 0; i < chooseNumber; i++)
            {
                chooseGB[i] = chooseBox.transform.Find("choose_"+(i+1).ToString()).gameObject;
                chooseTM[i] = chooseGB[i].transform.Find("content").GetComponent<TextMesh>();
                chooseGB[i].SetActive(false);
            }

            miniGB.SetActive(false);
            normalGB.SetActive(false);
        }
        catch { }
    }
    float disLastShowTime = 0;//距离上次字母显示时间
    float nowShowCD = 0;//本次显示冷却
    void Update()
    {
        //鼠标点击测试
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if(!normalGB.activeSelf)StartDialog();
            else clickDialog(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!normalGB.activeSelf) StartDialog();
            else clickDialog(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!normalGB.activeSelf) StartDialog();
            else clickDialog(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!normalGB.activeSelf) StartDialog();
            else clickDialog(3);
        }




        //文字依次显示
        if (normalGB.activeSelf && disLastShowTime >= nowShowCD)
        {
            disLastShowTime = 0;
            updateStringShowIndex();
        }
        else disLastShowTime += Time.deltaTime;

        //随机显示小内容
        randomShowMiniContent();
    }

    void updateStringShowIndex()
    {
        nowShowCD = 0;
        if (stringShowIndex < data.nowContent.Length){
        //更新nowShowCD
        for(int i=0;i< extraShowCD_char.Count; i++)
        {
            if (extraShowCD_char[i] == data.nowContent[stringShowIndex]) nowShowCD = extraShowCD_time[i];
        }

        if (nowShowCD == 0) nowShowCD = normalShowCD;
            stringShowIndex++;
            updateUIContent();
        }
    }

    public void StartDialog()
    {

        stringShowIndex = 0;
        data.StartDialogue();
        updateUIContent();//更新界面
        normalGB.SetActive(true);
        miniGB.SetActive(false);
    }

    public int clickDialog(int chooseIndex)
    {
        if (stringShowIndex >= data.nowContent.Length)//已经显示完
        {
            //有选项的时候就只能点击选项，没选项时候都可以点
            if (chooseIndex < 0 || data.chooseEnter.Count==0 || (chooseIndex >= 0 && chooseIndex < data.chooseEnter.Count))
            {
                stringShowIndex = 0;
                data.NextDialogue(chooseIndex);
                updateUIContent();
            }

        }
        else//还没有显示完就点击了
        {
            stringShowIndex = data.nowContent.Length;
            updateUIContent();
        }
        return data.nowIndex;
    }

    public void CloseDialog()
    {
        normalGB.SetActive(false);
    }

    void updateUIContent()
    {
        //
        if (data.isDialogueEnd) normalGB.SetActive(false);
        else
        {
            //更新文字
            noramlContentMesh.text = data.nowContent.Substring(0, stringShowIndex);

            //更新clickHit
            if (data.chooseEnter.Count == 0 && stringShowIndex >= data.nowContent.Length)
                clickHitGB.SetActive(true);
            else clickHitGB.SetActive(false);

            //更新选项
            for (int i = 0; i < chooseNumber; i++) chooseGB[i].SetActive(false);
            if (stringShowIndex >= data.nowContent.Length)
            {
                for(int i = 0; i < Math.Min(chooseNumber, data.chooseEnter.Count); i++)
                {
                    chooseTM[i].text=data.chooseString[i];
                    chooseGB[i].SetActive(true);
                }
            }
        }
    }

    void randomShowMiniContent()
    {
        string centerStr = "\n                       ";//添加此字符串可以让其居中
        if (data.isFirst && data.firstMinContent.Length != 0) 
        {
            if (miniGB.activeSelf == false && normalGB.activeSelf == false)
            {
                miniMesh_1.text = data.firstMinContent + centerStr;
                miniMesh_2.text = data.firstMinContent + centerStr;
                miniGB.SetActive(true);
            }
        }
        else
        {
            if(data.minContent.Length != 0)
            {
                if (miniGB.activeSelf == false && normalGB.activeSelf == false && UnityEngine.Random.Range(0, 100) == 1)
                {
                    miniMesh_1.text = data.minContent + centerStr;
                    miniMesh_2.text = data.minContent + centerStr;
                    miniGB.SetActive(true);
                }
            }
        }

    }
}
