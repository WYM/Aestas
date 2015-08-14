using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GalleryMenuBehavior : MonoBehaviour {

    public AudioSource bgm;
    public GameObject bgFade;

    public Image black;
    private float volume = 1;

    private GameObject panelCurrent;
    public GameObject panelEvent;
    public GameObject panelSoundtrack;

    void Start ()
    {
        //隐藏并关闭切换遮罩层
        bgFade.GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        bgFade.SetActive(false);

        //默认位于 Event 界面
        Event_Click();
        panelCurrent = panelEvent;
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
