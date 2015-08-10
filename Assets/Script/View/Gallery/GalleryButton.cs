using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class GalleryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{


    public Image textNormal;
    public Image textArrow;
    public Image textUnderline;
    public Image textBg;

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

    void Reset(float duration = 0f)
    {
        textArrow.rectTransform.DOLocalMoveX(100, duration);
        textUnderline.CrossFadeAlpha(0f, duration, true);
        textUnderline.rectTransform.DOLocalMoveX(240, duration);


        textNormal.CrossFadeColor(new Color(1, 1, 1, 1), duration, true, true);
        textArrow.CrossFadeColor(new Color(1, 1, 1, 0), duration, true, true);
        textBg.rectTransform.DOSizeDelta(new Vector2(294, 0), duration);
        
        textUnderline.CrossFadeColor(new Color(1, 1, 1, 0), duration, true, true);
        textUnderline.rectTransform.DOLocalMoveX(240, duration);
    }


    public void OnPointerClick(PointerEventData e)
    {
        /*
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            isAnimating = true;
        }, durationOut));
        */

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("SideMenuButton"))
        {
            GalleryButton gb = g.GetComponent<GalleryButton>();
            gb.InActivate();
        }

        isAnimating = true;
        isActive = true;

        textUnderline.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, true, true);
        textUnderline.rectTransform.DOLocalMoveX(27, durationIn);

        textArrow.rectTransform.DOLocalMoveX(50, durationIn);

        textNormal.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, true, true);
        textArrow.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, true, true);
        textBg.rectTransform.DOSizeDelta(new Vector2(294, 0), durationIn);
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (isAnimating || isActive) return;
        textBg.rectTransform.DOSizeDelta(new Vector2(294, 66), durationIn);
    }

    public void OnPointerUp(PointerEventData e)
    {
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (isAnimating || isActive) return;
        Reset();
        
        textArrow.rectTransform.DOLocalMoveX(50, durationIn);

        textNormal.CrossFadeColor(new Color(0, 0, 0, 1), durationIn, true, true);
        textArrow.CrossFadeColor(new Color(0, 0, 0, 1), durationIn, true, true);
        textBg.rectTransform.DOSizeDelta(new Vector2(294, 50), durationIn * 0.5f);
    }

    public void OnPointerExit(PointerEventData e)
    {
        Debug.Log("Exit");
        if (isAnimating || isActive) return;
        Debug.Log("Exit True");
        isAnimating = true;
        isActive = false;

        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            isAnimating = false;
        }, durationOut));
        
        textArrow.rectTransform.DOLocalMoveX(100, durationOut);

        textNormal.CrossFadeColor(new Color(1, 1, 1, 1), durationIn, true, true);
        textArrow.CrossFadeColor(new Color(1, 1, 1, 0), durationIn, true, true);
        textBg.rectTransform.DOSizeDelta(new Vector2(294, 0), durationIn);
    }

    public void InActivate()
    {
        Debug.Log(this.name + " InActivate();");
        isAnimating = false;
        isActive = false;
        Reset(durationIn);
    }

}
