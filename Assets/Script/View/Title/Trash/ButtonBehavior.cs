using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {

    public GameObject blackMask;
    public AudioSource bgm;
    public GameObject blurCamera;

    public GameObject buttonPanel;
    public GameObject loadPanel;
    public GameObject memoPanel;
    public GameObject optionPanel;
    public GameObject exitPanel;

    float pressTimer = 0f;
    float volume = 1.0f;

    bool isPressed = false;

    string toScene = "";

	// Use this for initialization
	void Start () {
        blackMask.SetActive(false);
        blackMask.GetComponent<Image>().CrossFadeAlpha(0, 0, false);

	}
	
	// Update is called once per frame
	void Update () {
        if (isPressed)
        {
            pressTimer += Time.deltaTime;
        }

        if (pressTimer > 1 && toScene != "")
        {
            switch (toScene)
            {
                case "Exit":
                    Application.Quit();
                    break;
                default:
                    Application.LoadLevel(toScene);
                    break;
            }
        }
	}

    public void Newgame_OnClick()
    {
        isPressed = true;
        toScene = "Game";
        BlackCurtain();
        InvokeRepeating("MuteBGM", 0f, 0.1f);
    }

    public void Load_OnClick()
    {
        blurCamera.SetActive(true);
        loadPanel.SetActive(true);
    }

    public void LoadPanel_Back_OnClick()
    {
        blurCamera.SetActive(false);
        loadPanel.SetActive(false);
    }

    public void Memo_OnClick()
    {
        buttonPanel.SetActive(false);
        memoPanel.SetActive(true);
    }

    public void MemoPanel_Back_OnClick()
    {
        buttonPanel.SetActive(true);
        memoPanel.SetActive(false);
        
    }

    public void MemoPanel_CG_OnClick()
    {
        isPressed = true;
        toScene = "Memories";
        BlackCurtain();
        InvokeRepeating("MuteBGM", 0f, 0.1f);
    }

    public void Option_OnClick()
    {
        blurCamera.SetActive(true);
        optionPanel.SetActive(true);
    }

    public void OptionPanel_Back_OnClick()
    {
        blurCamera.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void OptionPanel_Confirm_OnClick()
    {
        blurCamera.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void OptionPanel_Initialize_OnClick()
    {
    }


    public void Exit_OnClick()
    {
        //blurCamera.GetComponent<Camera>().fieldOfView = 0.5f;//viewport = new Rect(0.13f, 0.36f, 0.74f, 0.28f);
        blurCamera.SetActive(true);
        exitPanel.SetActive(true);
    }

    public void ExitPanel_Exit_OnClick()
    {
        isPressed = true;
        toScene = "Exit";
        BlackCurtain();
    }

    public void ExitPanel_Cancel_OnClick()
    {
        blurCamera.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void BlackCurtain()
    {
        blackMask.SetActive(true);
        blackMask.GetComponent<Image>().CrossFadeAlpha(1, 0.5f, false);
    }

    public void MuteBGM()
    {
        volume -= 0.1f;
        bgm.volume = volume;
    }
}
