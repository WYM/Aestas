using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{

    //string agcPath;                 //agc文件目录
    //CsvParser cp;                   //角色配置Csv

    AgcParser apCharacter;
    AgcParser apSuit;
    AgcParser apEmotion;

    public GameObject cPrefab;             //角色Prefab(模型0)
    public Dialogue Talk;
    public AudioControl Audio;

    public Transform foreTransform; //前景摄像机Transform

    public Dictionary<string, GameObject> c2ds; //角色对象字典

    public void Start()
    {
        //载入角色、表情配置文件
        /*
        agcPath = Application.streamingAssetsPath + "/ags/agc/";

        cp = new CsvParser(agcPath + "/Character.agc");
        cp.Load();

        cpEmotion = new CsvParser(agcPath + "/Emotion.agc");
        cpEmotion.Load();

        cpSuit = new CsvParser(agcPath + "/Suit.agc");
        cpSuit.Load();
        */

        //载入角色配置文件
        apCharacter = ResourcesOperator.LoadAgc(G.characterAgcFilename);
        apSuit = ResourcesOperator.LoadAgc(G.suitAgcFilename);
        apEmotion = ResourcesOperator.LoadAgc(G.emotionAgcFilename);

        //初始化c2ds字典
        c2ds = new Dictionary<string, GameObject>();
        Debug.Log("[CM]" + GetNameById(2));
        
    }

    public void Update()
    { 
    
    }

    //以 角色ID 查询 角色中文名
    public string GetNameById(int cId)
    {
        //return cp.GetDataByIdAndName(cId, "角色名");
        return apCharacter.cps.GetRowsByNameAndValue("ID", cId.ToString())[0][1];
    }

    //以 角色ID 查询 角色英文名
    public string GetNameEById(int cId)
    {
        //return apCharacter.cps.GetDataByIdAndName(cId, "cNameE");
        return apCharacter.cps.GetRowsByNameAndValue("ID", cId.ToString())[0][2];
    }

    //以 角色Id 和 套装名 查询 套装适用模型
    public string GetModel(int cId, string sName)
    {
        //return cp.GetDataByIdAndName(cId, "适用模型");
        string cName = GetNameById(cId);
        string[][] suits = apSuit.cps.GetRowsByNameAndValue("cName", cName);
        foreach (string[] s in suits)
        {
            if (s[1] == "sName")
            {
                return s[5];
            }
        }
        return "";
    }

    //以 角色英文名 查询 角色中文名
    public string GetNameByNameE(string cNameE)
    {
        //return cp.GetDataByNameAndValue("角色代称", Ename, "角色名");
        return apCharacter.cps.GetRowsByNameAndValue("cNameE", cNameE)[0][1];
    }

    //以 角色中文名 查询 角色英文名
    public string GetNameEByName(string cName)
    {
        //return cp.GetDataByNameAndValue("角色名", Name, "角色代称");
        return apCharacter.cps.GetRowsByNameAndValue("cName", cName)[0][2];
    }

    //以 角色英文名 查询 角色ID
    public int GetIdByNameE(string cNameE)
    {
        return int.Parse(apCharacter.cps.GetRowsByNameAndValue("cNameE", cNameE)[0][0]);
    }

    //以 角色中文名 查询 角色ID
    public int GetIdByName(string cName)
    {
        return int.Parse(apCharacter.cps.GetRowsByNameAndValue("cName", cName)[0][0]);
    }

    //以 套装名 查询 套装英文名
    public string GetSuitE(string cName, string sName)
    {
        string[] suitInfo = GetSuitInfo(cName, sName);
        return suitInfo[1];
    }

    //以 套装英文名 查询 套装名
    public string GetSuit(string cName, string sNameE)
    {
        string[][] suits = apSuit.cps.GetRowsByNameAndValue("cName", cName);
        foreach (string[] s in suits)
        {
            if (s[1] == "sName")
            {
                return s[1];
            }
        }
        return "";
    }

    //以 角色名 和 套装名 获取 套装信息数组
    public string[] GetSuitInfo(string cName, string sName)
    {
        string[][] suits = apSuit.cps.GetRowsByNameAndValue("cName", cName);
        int index = 0;
        int i = 0;
        foreach (string[] suit in suits)
        {
            if (suit[1] == sName)
            {
                index = i;
            }
            i++;
        }
        string[] s = suits[index];
        return new string[] { s[1], s[2], s[4], s[5], s[6], s[7], s[8], }; //0套装名，1套装代称，2适用模型，34表情位置XY，56表情大小XY
    }


    //以 角色名 和 套装名 获取 该套装下的表情数组
    public string[][] GetEmotions(string cName, string sName)
    {
        //string[][] suits = apSuit.cps.GetRowsByNameAndValue("cName", cName);
        string[][] emos = apEmotion.cps.GetRowsByNameAndValue("cName", cName);

        int emoCount = 0;
        foreach (string[] s in emos) //获取符合条件的表情数量
        {
            if (s[1] == sName)
            {
                emoCount++;
            }
        }

        string[][] emoArray = new string[emoCount][];
        int emoI = 0;
        foreach (string[] s in emos)
        {
            if (s[1] == sName)
            {
                emoArray[emoI] = new string[] { s[2], s[3], s[4] };
                emoI++;
            }
        }
        return emoArray;
    }

    /*
    //旧版
    public string[][] GetEmotionByName(string cName)
    {
        string[][] rowsArray = cpEmotion.GetRowsByNameAndValue("角色名", cName);
        string[][] emoArray = new string[rowsArray.Length][];
        for(int i = 0; i < rowsArray.Length; ++i)
        {
            emoArray[i] = new string[] { rowsArray[i][1], rowsArray[i][2] }; //[0]为表情名, [1]为表情代称
        }
        return emoArray;
    }

    //旧版
    public string[][] GetSuitByName(string cName)
    {
        string[][] rowsArray = cpSuit.GetRowsByNameAndValue("角色名", cName);
        string[][] suitArray = new string[rowsArray.Length][];
        for (int i = 0; i < rowsArray.Length; ++i)
        {
            suitArray[i] = new string[] { rowsArray[i][1], rowsArray[i][2] }; //[0]为表情名, [1]为表情代称
        }
        return suitArray;
    }
    */

    //某一角色的对象是否存在于场景中
    public bool GetCharacterStatus(string cName)
    {
        return c2ds.ContainsKey(cName) ? true : false;
    }

    //以 角色名 创建角色对象
    public bool CreateCharacter(string cName, string sName)
    {
        if (GetCharacterStatus(cName)) return false; //角色已存在时退出

        GameObject characterObj = (Instantiate(cPrefab, Vector3.zero, Quaternion.identity) as GameObject);
        characterObj.transform.parent = foreTransform;
        characterObj.transform.localScale = Vector3.one;

        Character2D c2d = GetCharacter2D(characterObj);
        c2d.InitCharacter(GetIdByName(cName), cName, GetNameEByName(cName), GetSuitInfo(cName, sName), GetEmotions(cName, sName));
        c2ds.Add(cName, characterObj); //将角色对象添加到字典
        return true;
    }

    //以 角色名 删除场景中的角色
    public bool DestoryCharacter(string cName)
    {
        if (!GetCharacterStatus(cName)) return false;
        c2ds.Remove(cName);
        return true;
    }

    //以 角色名 和 表情名 变更 场景中的角色表情
    public bool UpdateEmotion(string cName, string emo)
    {
        if (!GetCharacterStatus(cName)) return false; //角色不存在时退出
        GetCharacter2D(c2ds[cName]).UpdateEmotion(emo);

        return true;
    }

    //显示 或 隐藏 角色立绘
    public void Fade(string cName, bool isShow = true)
    {
        if (isShow)
        {
            GetCharacter2D(c2ds[cName]).Show();
        }
        else
        {
            GetCharacter2D(c2ds[cName]).Hide();
        }    
    }

    //移动角色
    public void MoveTo(string cName, string toPos)
    {
        GetCharacter2D(c2ds[cName]).MoveTo(toPos);
    }

    //角色对话
    public void Say(string cName, string text)
    {
        TalkAvatar(cName);
        Talk.UpdateText(text);
    }

    //播放音频
    public void PlayAudio(string voiceName)
    {
        Audio.PlayVoice(voiceName);
    }
    
    //改变对话框头像
    public void TalkAvatar(string cName)
    {
        Talk.UpdateAvatar(GetNameEByName(cName));
    }

    //返回对象的Character2D
    public static Character2D GetCharacter2D(GameObject cObj)
    {
        return cObj.GetComponent<Character2D>();
    }

    public void FX_Shake(string cName)
    {
        GetCharacter2D(c2ds[cName]).FX_Shake();
    }
}
