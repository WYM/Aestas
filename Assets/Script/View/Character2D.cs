using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Character2D : MonoBehaviour {

    public Image stand; //立绘
    public Image emotion; //表情
    //public Sprite standSprite;

    public int cId = 0;
    public string cName = "";
    public string cNameE = "";
    public string sName = "";
    public string sNameE = "";

    Dictionary<string, Sprite> emotions; //表情Sprite数组

    private float emoPosX = 0; //表情位置XY
    private float emoPosY = 0;
    private float emoSizeX = 0; //表情大小XY
    private float emoSizeY = 0;


    void Start ()
    {
	}

    //初始化角色
    public void InitCharacter(int chaId, string name, string ename, string[] suitArray, string[][] emoArray)
    {
        cId = chaId;
        cName = name;
        cNameE = ename;
        sName = suitArray[0];
        sNameE = suitArray[1];
        try
        {
            emoPosX = float.Parse(suitArray[3]);
            emoPosY = float.Parse(suitArray[4]);
            emoSizeX = float.Parse(suitArray[5]);
            emoSizeY = float.Parse(suitArray[6]);
        }
        catch (Exception ex)
        {
            Debug.Log("套装表情位置或大小配置错误！");
        }
        emotions = new Dictionary<string, Sprite>();

        stand.overrideSprite = ResourcesOperator.LoadSprite(G.suitPath + cNameE + "_" + sNameE);
        emotion.rectTransform.localPosition = new Vector3(emoPosX, emoPosY, 0);
        emotion.rectTransform.sizeDelta = new Vector2(emoSizeX, emoSizeY);
        
        //根据表情数组载入角色表情图片，存储在emotions字典内
        for (int i = 0; i < emoArray.Length; i++)
        {
            emotions.Add(emoArray[i][0], ResourcesOperator.LoadSprite(G.emotionPath + cNameE + "_" + sNameE + "_" + emoArray[i][1]));
        }
        SuddenHide();
    }
	
	void Update () 
    {
	
	}

    //改变表情
    public void UpdateEmotion(string emo)
    {
        Debug.Log(cName + emo);
        emotion.overrideSprite = emotions[emo];
    }

    //立即隐藏
    public void SuddenHide()
    {
        stand.CrossFadeAlpha(0f, 0f, false);
        emotion.CrossFadeAlpha(0f, 0f, false);
    }

    //隐藏立绘
    public void Hide()
    {
        stand.CrossFadeAlpha(0f, 0.3f, false);
        emotion.CrossFadeAlpha(0f, 0.3f, false);
    }
    
    //显示立绘
    public void Show()
    {
        stand.CrossFadeAlpha(1f, 0.3f, false);
        emotion.CrossFadeAlpha(1f, 0.3f, false);
    }

    //移动角色（硬）
    public void MoveTo(string toPos)
    {
        string[] pos;
        try
        {
            pos = G.characterPos[toPos].Split(',');
        }
        catch(Exception ex)
        {
            Debug.Log("移动目标点错误！");
            return;
        }

        this.GetComponent<RectTransform>().localPosition = new Vector2(float.Parse(pos[0]), float.Parse(pos[1]));
    }

    public void FX_Shake()
    {
        Vector3 originPos = this.GetComponent<RectTransform>().localPosition;
        System.Random ran = new System.Random();
        for (float i = 0; i < 0.8f; i += 0.02f)
        {
            StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
            {
                this.GetComponent<RectTransform>().localPosition = new Vector3(originPos.x + ran.Next(0, 80) - 40, originPos.y + ran.Next(0, 80) - 40, originPos.z);
            }, i));
        }
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
           this.GetComponent<RectTransform>().localPosition = new Vector3(originPos.x, originPos.y, originPos.z);
        }, 0.8f));
    }
}
