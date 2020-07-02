using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;

public class DialogDataReader
{
    JsonData jsonRoot;
    public bool isFirst = true;
    public int nowIndex = -1;
    
    public int DialogueNumber;
    
    public string nowContent;
    public List<string>chooseString = new List<string>();
    public List<int>chooseEnter = new List<int>();
    public int nextIndex = -1;
    public string firstMinContent;
    public string minContent;

    //当前对话结束
    public bool isDialogueEnd { get { return (nowIndex < 0 || nowIndex >= DialogueNumber); } }

    public DialogDataReader(string NPCid)
    {
        nowContent = "";
        firstMinContent = "";
        minContent = "";

        // StringBuilder stringBuilder = new StringBuilder(2048);
        //stringBuilder.Append(Application.dataPath);
        //stringBuilder.Append("/Resources/");
        // stringBuilder.Append(NPCid);
        //stringBuilder.Append(".json");
        Debug.Log(2);
        string readerData = Resources.Load<TextAsset>(LanguageManager.getInstance().getLanguageString("NPC_"+NPCid+"_Dialogue_File")).text;

        // StreamReader reader = File.OpenText(stringBuilder.ToString());
        // string readerData = reader.ReadToEnd();
        //reader.Close();
        jsonRoot = JsonMapper.ToObject(readerData);

        try
        {
            firstMinContent = jsonRoot["firstMinContent"].ValueAsString();
            minContent = jsonRoot["normalMinContent"].ValueAsString();
        }
        catch { }
    }
    
    

    //开始一次对话
    public int StartDialogue()
    {
        nowIndex = 0;

        if (isFirst && jsonRoot["firstEnter"] != null) nowIndex = jsonRoot["firstEnter"].ValueAsInt();
        else if (jsonRoot["noFirstEnter"] != null) nowIndex = jsonRoot["noFirstEnter"].ValueAsInt();

        updateDialogue();
        isFirst = false;
        return nowIndex;
    }

    //下一句对话，chooseIndex不存在就直接跳nextEnter,chooseIndex表示选第几项
    public int NextDialogue(int chooseIndex)
    {
        if (nowIndex < 0) return nowIndex;//对话已经结束

        if(chooseIndex >=0 && chooseIndex< chooseEnter.Count)nowIndex = chooseEnter[chooseIndex];
        else nowIndex = nextIndex;

        updateDialogue();
        return nowIndex;
    }

    //更新本次对话信息
    void updateDialogue()
    {
        chooseString.Clear();
        chooseEnter.Clear();
        nextIndex = -1;
        if (nowIndex < 0) return;
        JsonData dialogueContent = jsonRoot["dialogContent"];
        if (dialogueContent != null && nowIndex < dialogueContent.Count)
        {
            DialogueNumber = dialogueContent.Count;
            //获取内容
            JsonData contentJD = dialogueContent[nowIndex]["content"];
            if (contentJD != null)
            {
                nowContent = contentJD.ValueAsString();
            }

            //更新选择
            JsonData chooseJD = dialogueContent[nowIndex]["choose"];
            if (chooseJD != null)
            {
                for(int i = 0; i < chooseJD.Count; i++)
                {
                    chooseString.Add(chooseJD[i].ValueAsString());
                }
            }

            //更新选择进入
            JsonData chooseEnterJD = dialogueContent[nowIndex]["chooseEnter"];
            if (chooseEnterJD != null)
            {
                for (int i = 0; i < chooseEnterJD.Count; i++)
                {
                    chooseEnter.Add(chooseEnterJD[i].ValueAsInt());
                }
            }

            //更新直接跳转节点
            JsonData nextEnterJD = dialogueContent[nowIndex]["nextEnter"];
            if (nextEnterJD != null)
            {
                nextIndex = nextEnterJD.ValueAsInt();
            }
            else nextIndex = -1;
        }
    }

}
