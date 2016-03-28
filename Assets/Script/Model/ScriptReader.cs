using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ScriptReader : MonoBehaviour, ISequenceEndEvent {

    string scriptPath;

    //CsvParser cps;
    CharacterManager cm;

    Character2D c2d;

    AgcParser sAp; //scriptAgcParser 当前剧本ap
    int nl = 1; //nowLine 当前行数

    public Background2D bg2d;
    public GameObject blackMask;

    public bool stopable = true;
    public bool isSequencing = false;

	void Start () 
    {
        /*
        scriptPath = Application.streamingAssetsPath + "/ags";
        CsvParser cp = new CsvParser(scriptPath + "/CharacterInfo.agc");
        cp.Load();
        Debug.Log(cp.GetDataByIdAndName(3, "角色名"));
         * */

        //载入剧本
        /*
        scriptPath = Application.streamingAssetsPath + "/ags/";
        cps = new CsvParser(scriptPath + "/Script.agc");
        cps.Load();
        Debug.Log(cps.GetDataByIdAndName(10, "参数2"));
        */

        //AgcParser aps = ResourcesOperator.LoadAgc("suit");
        //Debug.Log(aps.cps.GetDataByIdAndName(4, "emoPosX"));

        //载入角色管理
        cm = this.GetComponent<CharacterManager>();

        //载入剧本
        sAp = new AgcParser("gt1", true, G.agcEncryptKey);

        //载入角色
        /*
        GameObject objPrefab = (Instantiate(cPrefab, Vector3.zero, Quaternion.identity) as GameObject);
        objPrefab.transform.parent = foreTransform;
        objPrefab.transform.localScale = Vector3.one;

        c2d = objPrefab.GetComponent<Character2D>();

        CharacterManager cm = new CharacterManager();
        string c2dName = "梧桐娘";
         * */
        //cm.Start();
        //cm.CreateCharacter("梧桐娘");
        //cm.UpdateEmotion("梧桐娘", "哭泣");
        //cm.GetSuitByName("");

        /*
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            cm.CreateCharacter("夏空", "校服");
            cm.UpdateEmotion("夏空", "哭泣");
            cm.Fade("夏空", true);
        }, 0.5f));

        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            cm.CreateCharacter("东方信", "校服");
            cm.UpdateEmotion("东方信", "紧张");
            cm.Fade("夏空", false);
            cm.Fade("东方信", true);
        }, 1f));

        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            cm.MoveTo("东方信", "2l");
            cm.TalkAvatar("东方信");
        }, 2f));
        */
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            NextLine();
        }, 0.1f));

    }
	
	void Update () 
    {
	
	}

    public void onSequenceEnd()
    {
        isSequencing = false;
    }

    public void ButtonHandler()
    {
        if (!isSequencing)
        {
            NextLine();
        }
        else
        {
            ExecuteEvents.Execute<ISequenceEndEvent>(this.gameObject, null, (x, y) => x.onSequenceEnd());
            switch (sAp.Q("command"))
            {
                case "c":
                case "Character":
                    switch (sAp.Q("function"))
                    {
                        case "s":
                        case "say":
                            cm.SayInstantly(); //立即显示未完成动画
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void NextLine()
    {
        sAp.nl = nl;
        switch (sAp.Q("command"))
        {
            case "load":
                switch (sAp.Q("function"))
                {
                    case "complete":
                        blackMask.GetComponent<Image>().CrossFadeAlpha(0, 0.6f, false);
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            blackMask.GetComponent<RectTransform>().localPosition = new Vector3(0, 1080);
                        }, 0.6f));
                        break;
                    default:
                        break;
                }
                break;
            case "goto":
                switch (sAp.Q("function"))
                {
                    case "scene":
                        sAp = new AgcParser(sAp.Q("object"), true, G.agcEncryptKey);
                        nl = 0;
                        blackMask.GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
                        blackMask.GetComponent<Image>().CrossFadeAlpha(1, 0.6f, false);


                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            nl++;
                            NextLine();
                        }, 1f));
                        /*
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            blackMask.GetComponent<Image>().CrossFadeAlpha(0, 0.6f, false);
                        }, 1f));
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            blackMask.GetComponent<RectTransform>().localPosition = new Vector3(0, 1080);
                        }, 1.6f));
                        */
                        break;
                    default:
                        break;
                }

                break;
            case "bg":
            case "Background":
                switch (sAp.Q("function"))
                {
                    case "show":
                        bg2d.ChangeImg(sAp.Q("object"));
                        break;
                    default:
                        break;
                }
                break;
            case "c":
            case "Character":
                switch (sAp.Q("function"))
                {
                    case "load":
                        //Log("载入角色" + sAp.Q("object") + "，服装为" + sAp.Q("para1"));
                        cm.CreateCharacter(sAp.Q("object"), sAp.Q("para1"));
                        break;
                    case "destory":
                        //Log("销毁角色" + sAp.Q("object"));
                        cm.DestoryCharacter(sAp.Q("object"));
                        break;
                    case "face":
                        //Log("改变表情：" + sAp.Q("object") + " > " + sAp.Q("para1"));
                        cm.UpdateEmotion(sAp.Q("object"), sAp.Q("para1"));
                        break;
                    case "move":
                        //Log("移动角色" + sAp.Q("object"));
                        cm.MoveTo(sAp.Q("object"), sAp.Q("para2"));
                        break;
                    case "show":
                        //Log("显示角色" + sAp.Q("object"));
                        if (sAp.Q("para1") == "fade") cm.Fade(sAp.Q("object"), true);
                        break;
                    case "hide":
                        //Log("隐藏角色" + sAp.Q("object"));
                        if (sAp.Q("para1") == "fade") cm.Fade(sAp.Q("object"), false);
                        break; 
                    case "s":
                    case "say":
                        //Log("角色说话" + sAp.Q("object"));
                        if (sAp.Q("para1") != "")
                        {
                            cm.PlayAudio(sAp.Q("para1"));
                        }
                        cm.Say(sAp.Q("object"), sAp.Q("para2"));
                        isSequencing = true;
                        break;
                    case "fx":
                        if (sAp.Q("para1") == "shake") cm.FX_Shake(sAp.Q("object"));
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        nl++;
        if (sAp.Q("flow") == "0")
        {
            NextLine();
        }
    }

    public void Log(string content)
    {
        Debug.Log("[SR][" + nl + "] " + content);
    }

    public void BackToTitle()
    {
        Application.LoadLevelAsync("Title2");
    }
}
