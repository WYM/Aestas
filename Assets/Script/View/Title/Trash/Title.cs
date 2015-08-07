using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Title : MonoBehaviour 
{

    public GameObject splash01;
    public GameObject splash02;
    public GameObject splashWhite;

    public GameObject logo;
    public GameObject button;

    public GameObject memo;

    public AudioSource bgmSource;

    bool isSplash01Show = false;
    bool isSplash02Show = false;
    bool isSplashWhiteShow = false;

    float timer = 0f;

    bool isSplashOn = true;
    bool isBgmPlaying = false;
    bool isButtonOn = false;
    bool isLogoOn = false;
    Image[] buttonImage;

	// Use this for initialization
	void Start ()
    {
        splash01.GetComponent<Image>().CrossFadeAlpha(0.0f, 0f, false);
        splash02.GetComponent<Image>().CrossFadeAlpha(0.0f, 0f, false);
        logo.GetComponent<Image>().CrossFadeAlpha(0.0f, 0f, false);
        buttonImage = button.GetComponentsInChildren<Image>();
        button.SetActive(false);

        ClearAllSplash();
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;

        if (isSplashOn && timer < 12)
        {
            if (timer > 0 && timer < 1 && !isSplash01Show)
            {
                Debug.Log("01" + timer.ToString());
                isSplash01Show = true;
                splash01.GetComponent<Image>().CrossFadeAlpha(1.0f, 1.0f, false);
            }

            if (timer > 3 && timer < 4 && isSplash01Show)
            {
                isSplash01Show = false;
                splash01.GetComponent<Image>().CrossFadeAlpha(0.0f, 1.0f, false);
            }

            if (timer > 4.5 && timer < 5.5 && !isSplash02Show)
            {
                isSplash02Show = true;
                splash02.GetComponent<Image>().CrossFadeAlpha(1.0f, 1.0f, false);
            }

            if (timer > 7.5 && timer < 8.5 && isSplash02Show)
            {
                isSplash02Show = false;
                splash02.GetComponent<Image>().CrossFadeAlpha(0.0f, 1.0f, false);
            }

            if (timer > 9 && timer < 10 && !isSplashWhiteShow)
            {
                splashWhite.GetComponent<Image>().CrossFadeAlpha(0.0f, 1.0f, false);
                isSplashOn = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Mouse Down.");
                ClearAllSplash();
            }
        }

        if (!isBgmPlaying && timer > 10)
        {
            Debug.Log("Play BGM.");
            bgmSource.Play();
            isBgmPlaying = true;
        }

        if (timer > 10.2 && !isLogoOn)
        {

            logo.GetComponent<Image>().CrossFadeAlpha(1.0f, 0.5f, false);
            isLogoOn = true;
        }
        if (timer > 12 && !isButtonOn)
        {
            button.SetActive(true);
            isButtonOn = true;
        }

	}

    public void ClearAllSplash()
    {
        timer += 10;
        isSplashOn = false;
        splash01.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.5f, false);
        splash02.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.5f, false);
        splashWhite.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.5f, false);
    }
}
