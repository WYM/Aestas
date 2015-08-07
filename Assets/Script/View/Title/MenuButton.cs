using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler {

    public Image bgr;
    public Image bgl;
    public Image textNormal;
    public Image textActive;
    public GameObject blackMask;

    public ParticleSystem p1;
    public ParticleSystem p2;
    public ParticleSystem pb;
    public ParticleSystem pg;

    public AudioSource sfx;

    Button btn;
    bool isActive = false;
    bool isAnimating = false;
    float durationIn = 0.2f;
    float durationOut;

    void Start ()
    {
        durationOut = durationIn * 1.5f;

        btn = this.GetComponent<Button>();
        Reset();
    }
	
	void Update () 
    {
	
	}

    public void Reset()
    {
        bgl.CrossFadeColor(new Color(1, 1, 1, 0), 0, false, true);
        bgl.rectTransform.DOScaleY(1, 0);
        bgl.rectTransform.DOLocalMoveX(-250, 0);
        bgr.CrossFadeColor(new Color(1, 1, 1, 0), 0, false, true);
        bgr.rectTransform.DOScaleY(1, 0);
        bgr.rectTransform.DOLocalMoveX(290, 0);
        textActive.CrossFadeColor(new Color(1, 1, 1, 0), 0, false, true);
        textNormal.CrossFadeColor(new Color(1, 1, 1, 1), 0, false, true);
        p1.emissionRate = 0;
        p2.emissionRate = 0;
        pb.emissionRate = 0;
    }

    public void OnPointerClick(PointerEventData e)
    {

        isAnimating = true;
        p1.emissionRate = 0;
        p2.emissionRate = 0;
        pb.emissionRate = 0;

        bgl.CrossFadeColor(new Color(0, 0, 0, 1), durationIn, false, false);
        //bgl.rectTransform.DOLocalMoveX(-250, durationIn);
        bgr.CrossFadeColor(new Color(0, 0, 0, 1), durationIn, false, false);
        //bgr.rectTransform.DOLocalMoveX(255, durationIn);
        textActive.CrossFadeColor(new Color(1, 1, 1, 0), durationIn, false, true);
        textNormal.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, false, true);
        
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            blackMask.SetActive(true);
            blackMask.GetComponent<Image>().DOFade(1, 1f);
        }, 0.5f));

    }

    public void OnPointerDown(PointerEventData e)
    {
        bgl.CrossFadeColor(new Color(0, 0, 0, 1), durationIn, false, false);
        bgr.CrossFadeColor(new Color(0, 0, 0, 1), durationIn, false, false);
        textActive.CrossFadeColor(new Color(1, 1, 1, 0), durationIn, false, true);
        textNormal.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, false, true);
    }

    public void OnPointerUp(PointerEventData e)
    {
        bgl.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, false, false);
        bgr.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, false, false);
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (isAnimating) return;
        Reset();
        isActive = true;


        p1.emissionRate = 10;
        p2.emissionRate = 10;
        pb.emissionRate = 30;

        bgl.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, false, true);
        bgl.rectTransform.DOLocalMoveX(-10, durationIn);
        bgr.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, false, true);
        bgr.rectTransform.DOLocalMoveX(255, durationOut);
        textActive.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, false, true);
        textNormal.CrossFadeColor(new Color(1, 1, 1, 0), durationIn, false, true);

        sfx.clip = ResourcesOperator.LoadSFX("page");
        sfx.Play();
    }

    public void OnPointerExit(PointerEventData e)
    {
        if (isAnimating) return;
        isAnimating = true;
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            isAnimating = false;
        }, durationOut));
        
        isActive = false;


        p1.emissionRate = 0;
        p2.emissionRate = 0;
        pb.emissionRate = 0;

        bgl.CrossFadeColor(new Color(1, 1, 1, 0), durationOut, false, true);
        bgl.rectTransform.DOScaleY(0, durationOut);
        bgr.CrossFadeColor(new Color(1, 1, 1, 0), durationOut, false, true);
        bgr.rectTransform.DOScaleY(0, durationOut);
        textActive.CrossFadeColor(new Color(1, 1, 1, 0), durationOut, false, true);
        textNormal.CrossFadeColor(new Color(1, 1, 1, 1), durationOut, false, true);
    }

}
