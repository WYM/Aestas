using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System;

public class AgsParser : MonoBehaviour {

    ArrayList scriptList;
    string agsPath;

    public GameObject backgroundObj;
    public GameObject AudioManagerObj;

    Background2D bg2d;
    AudioControl audio;

	// Use this for initialization
	void Start () 
    {
        agsPath = Application.streamingAssetsPath + "/ags";
        scriptList = LoadFile(agsPath, "main.ags");
        CheckAgsObject("@dia|tttqaa#iooi");

        bg2d = backgroundObj.GetComponent<Background2D>();
        audio = AudioManagerObj.GetComponent<AudioControl>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void LineParse(string line)
    {
        string lineType = CheckAgsObject("line");
        switch (lineType)
        {
            case "log":
                cLog(line);
                break;
            case "s":
                cSoundFX(line);
                break;
            case "m":
                cMusic(line);
                break;
            case "default":
                break;
        }
    }

    ArrayList LoadFile(string path, string filename)
    {
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(path + "//" + filename);
        }
        catch (Exception ex)
        {
            qLog("Open file error.");
            return null;
        }

        string line;
        string massScript = "";

        while ((line = sr.ReadLine()) != null)
        {
            if (line != "")
            {
                massScript += line;
            }
        }

        sr.Close();
        sr.Dispose();

        string[] splitScript = massScript.Split('@');
        return new ArrayList(splitScript);

        int sequenceID = 0;
        int step = 0;
        string lineType = "";
        foreach (string s in splitScript)
        {
            lineType = CheckAgsObject(s);

            switch (lineType)
            {
                case "dia":
                case "mov":
                case "sl":
                    scriptList.Add(new List<string>());
                    break;
                case "c":
                case "bg":
                case "cg":
                    break;
                default:
                    break;
            }
        }
        /*
        int sequenceID = 0;
        int step = 0;
        string lineType = "";
        foreach (string s in splitScript)
        {
            lineType = CheckAgsObject(s);

            switch (lineType)
            { 
                case "dia": case "mov": case "sl":
                    scriptList.Add(new List<string>());
                    break;
                case "c": case "bg": case "cg":
                    break;
                default:
                    break;
            }
        }
         */
    }

    string CheckAgsObject(string agsLine)
    {
        Regex re = new Regex("(@)([a-z]*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        Match m = re.Match(agsLine);
        string lineType = "";
        if (m.Success)
        {
            lineType = m.Groups[2].ToString();
        }
        return lineType;
    }

    void qLog(string log)
    {
        Debug.Log("[AgsParser] " + log);
    }

    Match RegexMatch(string expression, string line)
    {
        Regex re = new Regex("(@log#)(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        Match m = re.Match(line);
        return m;
    }

    //@s命令
    void cSoundFX(string line)
    {
        Match m = RegexMatch(@"(@s\|)(.*)", line);
        if (m.Success)
        {
            if (m.Groups[2].ToString() != "stop")
            {
                audio.PlaySFX(m.Groups[2].ToString());
                qLog("[@s] 开始播放编号为 " + m.Groups[2].ToString() + " 的音效。");
            }
            else
            {
                audio.StopSFX();
                qLog("[@s] 停止了音效的播放。");
            }
        }
    }


    //@m命令
    void cMusic(string line)
    {
        Match m = RegexMatch(@"(@m\|)(.*)", line);
        if (m.Success)
        {
            if (m.Groups[2].ToString() != "stop")
            {
                audio.PlayBGM(m.Groups[2].ToString());
                qLog("[@m] 开始播放编号为 " + m.Groups[2].ToString() + " 的背景音乐。");
            }
            else
            {
                audio.StopBGM();
                qLog("[@m] 停止了背景音乐的播放。");
            }
        }
    }
    
    //@mov命令
    void cMovie(string line)
    {
        Match m = RegexMatch("(@mov#)(.*)", line);
        if(m.Success)
        {
            qLog("[@mov] 开始播放名称为 " + m.Groups[2].ToString() + " 的视频。");
        }
    }

    //@bg命令
    void cBackground(string line)
    {
        string filter;
        string bgName;
        Match m = RegexMatch(@"(@bg\|)([a-zA-z0-9_-]*)(#)([a-zA-z0-9_-]*)", line);
        if(m.Success)
        {
            filter = m.Groups[4].ToString();
        }
        else
        {
            m = RegexMatch(@"(@bg\|)([a-zA-z0-9_-]*)", line);
            filter = "default";
        }

        bgName = m.Groups[2].ToString();

        if(m.Success)
        {
            qLog("[@bg] 将背景图片改变成 " + bgName + " ，并添加 " + filter + " 滤镜。");
            bg2d.ChangeImg("img/bgp/" + bgName);
        }
    }
    
    //@cg命令
    void cCG(string line)
    {
        string filter = ""; //滤镜名称
        string cgName; //CG图片名称
        Match m = RegexMatch(@"(@cg\|)([a-zA-z0-9_-]*)(#)([a-zA-z0-9_-]*)", line); //正则表达式
        if(m.Success) 
        {
            //如果匹配成功，则获取滤镜名
            filter = m.Groups[4].ToString();
        }
        else
        {
            //如果匹配不成功，则滤镜名为默认
            filter = "default";
        }

        cgName = m.Groups[2].ToString();

        if(m.Success)
        {
            qLog("[@cg] 显示CG图片 " + cgName + " ，并添加 " + filter + " 滤镜。");
            bg2d.ApplyFilter(filter);
            bg2d.ChangeImg("img/bgp/" + cgName, true);
        }
    }

    void cDialogue(string line)
    {
        string method;
        Match m = RegexMatch("(@dia#)(.*)", line);
        if(m.Success)
        {
            method = m.Groups[2].ToString();

            switch(method)
            {
                case "show":
                    qLog("[@dia] 显示对话框");
                    break;
                case "hide":
                    qLog("[@dia] 隐藏对话框");
                    break;
                default:
                    qLog("[@dia] 发动了特效 [" + method + "]。");
                    break;
            }
        }

    }

    void cCharacter(string line)
    {
        string character;
        string method;
        Match m = RegexMatch(@"(@c\|)([a-z0-9_-]*)(\|)([a-z0-9_-]*)", line);
        
        if(m.Success)
        {
            character = m.Groups[2].ToString();
            method = m.Groups[4].ToString();

            switch(method)
            {

                case "say":
                    string sayContent;
                    string voice;
                    m = RegexMatch(@"(@c\|)([a-z0-9_-]*)(\|say)(#)(.*)(\*)([a-z0-9_-]*)", line);
                    if (m.Success)
                    {
                        sayContent = m.Groups[5].ToString();
                        voice = m.Groups[7].ToString();

                        qLog("[@c] 角色[" + character + "]说：" + sayContent + "；");

                        if (voice == "0")
                        {
                            qLog("[@c] 不播放语音音频。");
                        }
                        else
                        {
                            audio.PlayVoice(voice);
                            qLog("[@c] 播放了语音音频[" + voice + "]。");
                        }
                    }
                    break;
                case "show":
                    string wayShow;
                    string positionShow;
                    m = RegexMatch(@"(@c\|)([a-z0-9_-]*)(\|show)(#)([a-z0-9_-]*)(\*)([a-z0-9_-]*)", line);
                    if(m.Success)
                    {
                        wayShow = m.Groups[5].ToString();
                        positionShow = m.Groups[7].ToString();
                        qLog("[@c] 角色[" + character + "]以[" + wayShow + "]的方式出现在了[" + positionShow + "]位置。");
                    }
                    break;
                case "hide":
                    string wayHide;
                    m = RegexMatch(@"(@c\|)([a-z0-9_-]*)(\|hide)(#)([a-z0-9_-]*)", line);
                    if(m.Success)
                    {
                        wayHide = m.Groups[5].ToString();
                        qLog("[@c] 角色[" + character + "]以[" + wayHide + "]的方式退场了。");
                    }
                    break;
                case "move":
                    m = RegexMatch(@"(@c\|)([a-z0-9_-]*)(\|move)(#)([a-z0-9_-]*)", line);
                    if (m.Success)
                    {
                        string positionMove;
                        positionMove = m.Groups[5].ToString();
                        qLog("[@c] 角色[" + character + "]移动到了 " + positionMove + " 位置。");
                    }
                    break;
                case "clean":
                    qLog("[@c] 角色[" + character + "]被清除了。");
                    break;
                case "face":
                    string face;
                    m = RegexMatch(@"(@c\|)([a-z0-9_-]*)(\|face)(#)([a-z0-9_-]*)", line);
                    if(m.Success)
                    {
                        face = m.Groups[5].ToString();
                        qLog("[@c] 角色[" + character + "]的表情变成了" + face + "。");
                    }
                    break;
                case "fx":
                    string fx;
                    m = RegexMatch(@"(@c\|)([a-z0-9_-]*)(\|fx)(#)([a-z0-9_-]*)", line);
                    if (m.Success)
                    {
                        fx = m.Groups[5].ToString();
                        qLog("[@c] 对角色[" + character + "]应用了[" + fx + "]特效。");
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //@Log 命令
    void cLog(string line)
    {
        Match m = RegexMatch("(@log#)(.*)", line);
        if (m.Success)
        {
            qLog("[@log] 在控制台输出：" + m.Groups[2].ToString());
        }
    }

}
