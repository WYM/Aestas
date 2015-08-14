using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class GallerySoundtrackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{

    public string musicTitle; //音乐标题
    public string artist; //艺术家
    public string musicFile; //音乐文件名
    //public AudioClip musicClip; //音乐文件名

    public GameObject textNormal;
    public GameObject textActive;
    public GameObject underline;

    public GallerySoundtrackControl soundtrackControl;

    float durationIn = 0.2f;
    float durationOut = 0.4f;

    void Start ()
    {
        textNormal.GetComponent<Text>().text = musicTitle + " <size=18><color=#888>  " + artist + "</color></size>";
        textActive.GetComponent<Text>().text = musicTitle + " <size=18>  " + artist + "</size>";
        textActive.GetComponent<Text>().DOFade(0, durationIn);
    }
	
	void Update ()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //underline.GetComponent<Image>().CrossFadeColor(new Color(200, 200, 200, 180), durationIn, true, true);
        textNormal.GetComponent<Text>().DOFade(0, durationIn);
        textActive.GetComponent<Text>().DOFade(1, durationIn);
        underline.GetComponent<RectTransform>().DOLocalMoveY(0, durationIn);
        underline.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1400, 60), durationIn);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //underline.GetComponent<Image>().CrossFadeColor(new Color(100, 100, 100, 240), durationOut, true, true);
        textNormal.GetComponent<Text>().DOFade(1, durationOut);
        textActive.GetComponent<Text>().DOFade(0, durationOut);
        underline.GetComponent<RectTransform>().DOLocalMoveY(-30, durationOut);
        underline.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1400, 1), durationOut);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        soundtrackControl.PlayNew(musicTitle, artist, musicFile);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        underline.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1400, 60), durationOut);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        underline.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1400, 80), durationIn / 2);
    }
}
