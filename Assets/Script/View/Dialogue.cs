using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    public GameObject dialogueTextObj;
    public GameObject dialogueBackgroundObj;
    public GameObject dialogueAvatarObj;

    Text diaText;
    Image diaBackground;
    Image diaAvatar;

    bool isTyping = false;
    string typingText = "";

    float timer = 0f;

    float textSpeed = 0.03f;

	// Use this for initialization
	void Start () 
    {
        diaText = dialogueTextObj.GetComponent<Text>();
        diaAvatar = dialogueAvatarObj.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        if (timer > textSpeed)
        {
            if (isTyping)
            {
                if (typingText.Length < 2)
                {
                    diaText.text += typingText;
                    typingText = "";
                    isTyping = false;
                }
                else
                {
                    diaText.text += typingText.Substring(0, 1);
                    typingText = typingText.Substring(1);
                }
            }

            timer -= textSpeed;
        }
	}

    public void UpdateText(string text)
    {
        diaText.text = "";
        isTyping = true;
        typingText = text;
    }

    public void ClearText()
    {
        diaText.text = "";
        isTyping = false;
        typingText = "";
    }

    public void UpdateAvatar(string cNameE)
    {
        diaAvatar.overrideSprite = ResourcesOperator.LoadSprite(G.avatarPath + cNameE);
        diaAvatar.CrossFadeAlpha(1f, 0.2f, true);
    }

    public void ClearAvatar()
    {
        diaAvatar.CrossFadeAlpha(0f, 0.2f, true);
    }

    public void HideDialogue()
    { 
        
    }

    public void ShowDialogue()
    { 
        
    }

    //Test...
    int onet = 0;
    public void OutTest()
    {
        switch (onet)
        {
            case 0:
                UpdateAvatar("ao");
                UpdateText("内置测试开始：对话。接下来将清除对话框内容（场景载入时过曝为特效演示，非Bug）（由于AGSParser存在问题，该测试并非是由AGS载入的。）");
                break;
            case 1:
                ClearAvatar();
                ClearText();
                break;
            case 2:
                UpdateAvatar("ao2");
                UpdateText("更换了角色头像为ao2，接下来测试文本。");
                break;
            case 3:
                UpdateAvatar("ao");
                UpdateText("国务院新闻办公室今日举行新闻发布会，国家卫生计生委副主任、国家中医药管理局局长王国强介绍《中国居民营养与慢性病状况报告(2015)》，并答记者问。");
                break;
            case 4:
                UpdateAvatar("ao");
                UpdateText("日前，北京市人社局和市统计局发布了“关于公布2014年度北京市职工平均工资的通知”，通知显示，2014年度全市职工平均工资为77560元，月平均工资为6463元。");
                break;
            case 5:
                UpdateAvatar("ao");
                UpdateText("据悉，本次北京统计的工资总额为职工个人税前工资，还包括了单位代扣代缴的个人应缴纳的社会保险基金和住房公积金的个人缴纳部分等。");
                break;
            case 6:
                UpdateAvatar("ao");
                UpdateText("记者进行了粗略计算，按照现行社保缴纳比例，职工个人每月实际拿到手的可支配收入不足5000元。");
                break;
            case 7:
                UpdateAvatar("ao");
                UpdateText("据同花顺财经统计，股民在这期间人均亏损14 .7万元，相当于去年平均工资将近三倍。还能赚钱的股民只有4%，而八成的股民亏损超过10%。");
                break;
            case 8:
                UpdateText("测试结束。面对停职、拘留5日的处理决定，在交警赶来之前，我对奥迪车涉嫌套牌的一系列质疑遭到数名不明身份男子的威胁。");
                break;
            default: 
                break;
        }

        onet++;
    }
}
