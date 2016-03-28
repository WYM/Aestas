using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class TalkText : MonoBehaviour{



    public string content = "这个 Switch 步骤@是或多或少?玩!家都会感受到新的故事内的系统。只要不断的做任务，然后按自已喜好换上认为较强的卡片与衣服（想白金的玩家用到的卡片阶级有分为十级，七级，四级，初段）。";
    char[] chars;

    public GameObject letterPrefab;
    GameObject[] letters;

    float lastX = 0;
    float lastY = 0;

    int strIndex = 0;


    public enum AnimationType { Fade, Top, Bottom, Left, Right, Monochrome, Colorful };
    AnimationType aniType = AnimationType.Monochrome;
    bool isAnimating = false;
    float animationTimer = 0;

    float textSpeed = 0.06f;

    void Start()
    {

        content = "前两天下载更新的时候终于提示硬盘空间不够了，作为立志要成为坑王的我，但又不想删游戏，干脆换硬盘。翻了站内外的心得教程贴，最大的问题就是找硬盘了。";

        //创建 Letters 数组，实例化各单字符(原为150)
        letters = new GameObject[120];
        for (int i = 0; i < 120; i++)
        {
            GameObject letterObj = (Instantiate(letterPrefab, Vector3.zero, Quaternion.identity) as GameObject);
            letterObj.transform.parent = this.gameObject.GetComponent<RectTransform>();
            letterObj.transform.localScale = Vector3.one;
            letterObj.GetComponent<RectTransform>().localPosition = localVector3(0, 0);
            letters[i] = letterObj;
        }
        //ShowText(content);
    }
    
    void Update()
    {
        if (isAnimating)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer > textSpeed)
            {
                animationTimer -= textSpeed;
                NextLetter();
            }
        }
    }

    public void ShowText(string text)
    {
        //如果上一条信息仍未显示完成，先显示上一条信息
        /*
        if (isAnimating)
        {
            ShowComplete();
            ExecuteEvents.Execute<ITextEndEvent>(this.gameObject, null, (x, y) => x.onTextEnd());
            return;
        } 
        */

        Clean();
        content = text;
        isAnimating = true;
    }

    public void ShowComplete()
    {

        AnimationReset();
        for (int i = strIndex; i < content.Length; i++)
        {
            NextLetter(false);
            letters[strIndex - 1].GetComponent<Text>().DOFade(1, 0.2f);
        }
    }

    public void ShowInstantly()
    {
        AnimationReset();
        for (int i = strIndex; i < content.Length; i++)
        {
            NextLetter(false);
            letters[strIndex - 1].GetComponent<Text>().DOFade(1, 0.2f);
        }
    }

    void NextLetter(bool animate = true)
    {
        //Debug.Log(strIndex);
        if (strIndex == content.Length || lastY <= -180)
        {
            AnimationReset();
            ExecuteEvents.Execute<ISequenceEndEvent>(this.gameObject, null, (x, y) => x.onSequenceEnd());
            return;
        }

        if (IsChinese(content[strIndex]))
        {
            letters[strIndex].GetComponent<TalkLetter>().SwitchTo(TalkLetter.TalkLetterType.Double);
        }
        else
        {
            letters[strIndex].GetComponent<TalkLetter>().SwitchTo(TalkLetter.TalkLetterType.Single);
        }

        float thisX = lastX;
        float thisY = lastY;

        letters[strIndex].GetComponent<Text>().text = content[strIndex].ToString();
        letters[strIndex].GetComponent<RectTransform>().localPosition = localVector3(lastX, lastY);

        lastX += (!IsChinese(content[strIndex]) && !IsChinese(content[strIndex + 1])) ? 25 + ( (content[strIndex] > 64 && content[strIndex] < 90) ? 8 : 0) : 48;

        if (lastX > 1150)
        {
            lastX = 0;
            lastY -= 60;
        }

        //letters[strIndex].SetActive(true);
        if(animate) Animate(letters[strIndex], thisX, thisY);
        strIndex++;
    }

    void Animate(GameObject l, float x, float y)
    {
        switch (aniType)
        {
            case AnimationType.Fade:
                l.GetComponent<Text>().DOFade(1, textSpeed * 3);
                break;
            case AnimationType.Top:
                l.GetComponent<RectTransform>().DOLocalMoveY(y + 100 + 100, 0f);
                l.GetComponent<RectTransform>().DOLocalMoveY(y + 100, textSpeed * 2.5f);
                l.GetComponent<Text>().DOFade(1, textSpeed * 4);
                break;
            case AnimationType.Bottom:
                l.GetComponent<RectTransform>().DOLocalMoveY(y - 100 + 100, 0f);
                l.GetComponent<RectTransform>().DOLocalMoveY(y + 100, textSpeed * 2.5f);
                l.GetComponent<Text>().DOFade(1, textSpeed * 5);
                break;
            case AnimationType.Left:
                l.GetComponent<RectTransform>().DOLocalMoveX(x - 500 - 580, 0f);
                l.GetComponent<RectTransform>().DOLocalMoveX(x - 580, textSpeed * 3);
                l.GetComponent<Text>().DOFade(1, textSpeed * 4);
                break;
            case AnimationType.Right:
                l.GetComponent<RectTransform>().DOLocalMoveX(x + 500 - 580, 0f);
                l.GetComponent<RectTransform>().DOLocalMoveX(x - 580, textSpeed * 3);
                l.GetComponent<Text>().DOFade(1, textSpeed * 4);
                break;
            case AnimationType.Monochrome:
                l.GetComponent<Text>().DOColor(new Color(0.3f, 0.3f, 0.3f), 0f);
                l.GetComponent<Text>().DOColor(new Color(1, 1, 1), textSpeed * 3);
                letters[strIndex].GetComponent<Text>().DOFade(1, 0);
                break;
            case AnimationType.Colorful:
                l.GetComponent<Text>().DOColor(new Color(Random.Range(0.2f, 0.5f), Random.Range(0.2f, 0.5f), Random.Range(0.2f, 0.5f)), 0f);
                l.GetComponent<Text>().DOColor(new Color(1, 1, 1), textSpeed * 3);
                letters[strIndex].GetComponent<Text>().DOFade(1, textSpeed);
                break;
            default:
                break;
        }
    }

    void AnimationReset()
    {
        isAnimating = false;
        animationTimer = 0;
    }

    //判断Char是否为汉字
    bool IsChinese(char c)
    {
        return (c > 127) ? true : false;
    }

    //初始化Letters
    void Clean()
    {
        lastX = 0;
        lastY = 0;
        strIndex = 0;
        foreach (GameObject l in letters)
        {
            l.GetComponent<Text>().DOFade(0, 0f);
            l.SetActive(false);
        }
    }

    //根据本地坐标修正的Vector3
    Vector3 localVector3(float x = 0, float y = 0, float z = 0)
    {
        return new Vector3(x - 580, y + 100, z);
    }
}
