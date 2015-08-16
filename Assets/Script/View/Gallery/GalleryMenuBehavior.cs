using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GalleryMenuBehavior : MonoBehaviour {
    
    public string MenuType;

    public AudioSource bgm;
    public GameObject bgFade;

    public Image black;
    private float volume = 1;

    private GameObject panelCurrent;


    public GameObject panelEvent;
    public GameObject panelSoundtrack;
    public GameObject panelTrophy;
    public GameObject panelMemo;

    public GameObject panelSave;
    public GameObject panelLoad;
    public GameObject panelBasic;
    public GameObject panelPersonalize;
    public GameObject panelAccount;

    void Start ()
    {
        //隐藏并关闭切换遮罩层
        bgFade.GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        bgFade.SetActive(false);

        switch (MenuType)
        {
            case "Gallery":
                //默认位于 Event 界面
                Event_Click();
                panelCurrent = panelEvent;
                break;
            case "Option":
                //默认位于 Event 界面
                Basic_Click();
                panelCurrent = panelBasic;
                break;
            case "Save":
                //默认位于 Event 界面
                Save_Click();
                panelCurrent = panelSave;
                break;
            case "Load":
                //默认位于 Event 界面
                Load_Click();
                panelCurrent = panelLoad;
                break;
            default:
                break;
        }
    }
	
	void Update ()
    {

    }

    public void Event_Click()
    {
        LoadSubScene(panelEvent);
    }

    public void Soundtrack_Click()
    {
        LoadSubScene(panelSoundtrack);
    }

    public void Trophy_Click()
    {
        LoadSubScene(panelTrophy);
    }

    public void Memo_Click()
    {
        LoadSubScene(panelMemo);
    }

    public void Save_Click()
    {
        LoadSubScene(panelSave);
    }

    public void Load_Click()
    {
        LoadSubScene(panelLoad);
    }

    public void Basic_Click()
    {
        LoadSubScene(panelBasic);
    }

    public void Personalize_Click()
    {
        LoadSubScene(panelPersonalize);
    }

    public void Account_Click()
    {
        LoadSubScene(panelAccount);
    }

    public void Back_Click()
    {
        black.gameObject.SetActive(true);
        black.CrossFadeColor(new Color(0, 0, 0, 0), 0f, true, true);
        black.CrossFadeColor(new Color(0, 0, 0, 1), 1f, true, true);
        WaitAndLoadLevel("Title2");
        FadeBGM();

    }

    public void MuteBGM()
    {
        if (volume > 0) volume -= 0.1f;
        bgm.volume = volume;
    }

    public void FadeBGM()
    {
        InvokeRepeating("MuteBGM", 0f, 0.1f);
    }

    public void LoadSubScene(GameObject toPanel)
    {
        bgFade.SetActive(true);
        bgFade.GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        bgFade.GetComponent<Image>().CrossFadeAlpha(1, 0.6f, true);
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            panelCurrent.SetActive(false);
            toPanel.SetActive(true);
            panelCurrent = toPanel;
        }, 0.8f));
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            bgFade.GetComponent<Image>().CrossFadeAlpha(0, 0.6f, true);
        }, 1.2f));
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            bgFade.SetActive(false);
        }, 1.8f));
    }

    public void WaitAndLoadLevel(string levelName)
    {
        if (levelName == "Exit")
        {
            StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
            {
                Application.Quit();
            }, 1.8f));
        }
        else
        {
            StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
            {
                Application.LoadLevel(levelName);
            }, 1.8f));
        }
    }
}
