using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class LanguageManager
{
    //所有语言
    public string[] allType;

    //当前语言
    private int languageType;

    //获取当前语言
    public string nowChooseLanguage { get => allType[languageType]; }

    //全部语言文本
    private Dictionary<string,string> allLanguageString;

    //获取指定串
    public string getLanguageString(string requeryString)
    {
        string value;
        if (allLanguageString.TryGetValue(requeryString, out value)) return value;
        return string.Empty;
    }

    //设置语言
    public void setLanguage(int index)
    {
        if(index>=0 && index < allType.Length)
        {
            PlayerPrefs.SetInt("language", index);
            languageType = index;
            UpdateLanguageString();
        }
    }


    //重新读取语言文件
    private void UpdateLanguageString()
    {
        try
        {
            allLanguageString.Clear();
            string[] languageStrings = Resources.Load<TextAsset>("LanguageString_" + allType[languageType]).text.Replace("\r\n","&").Split('&');
            foreach (string languageString in languageStrings)
            {
                string[] splltLS = languageString.Split(':');
                allLanguageString.Add(splltLS[0], splltLS[1]);
            }
        }
        catch { }
    }

    //单例
    private LanguageManager()
    {
        allType = Resources.Load<TextAsset>("LanguageType").text.Replace("\r\n", "&").Split('&');
        allLanguageString = new Dictionary<string, string>();
        languageType = PlayerPrefs.GetInt("language", 0);
        UpdateLanguageString();
        setLanguage(1);
    }

    public static LanguageManager ins = null;
    public static LanguageManager getInstance()
    {
        if (ins == null)
        {
            ins = new LanguageManager();
        }
        return ins;
    }
    
}
