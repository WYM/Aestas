using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GalleryMenuBehavior : MonoBehaviour {

    public AudioSource bgm;
    public Image black;
    float volume = 1;

    void Start ()
    {
	}
	
	void Update ()
    {
	
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
