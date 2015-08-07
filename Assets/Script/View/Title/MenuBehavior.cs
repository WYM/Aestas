using UnityEngine;
using System.Collections;

public class MenuBehavior : MonoBehaviour {

    public AudioSource bgm;
    float volume = 1;

    void Start ()
    {
	
	}
	
	void Update ()
    {

    }

    public void Menu_NewClick()
    {
        WaitAndLoadLevel("Game");
        FadeBGM();
    }

    public void Menu_ExitClick()
    {
        WaitAndLoadLevel("Exit");
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
