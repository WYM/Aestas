using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using DG.Tweening;

public class GalleryEventButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{

    public Image bg;
    public Image cg;

	void Start ()
    {
        bg.rectTransform.DOSizeDelta(new Vector2(385, 217), 0);
	}
	
	void Update ()
    {
	
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.rectTransform.DOSizeDelta(new Vector2(425, 247), 0.2f);
        cg.DOFade(0.9f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.rectTransform.DOSizeDelta(new Vector2(385, 217), 0.3f);
        cg.DOFade(1f, 0.3f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        bg.rectTransform.DOSizeDelta(new Vector2(385, 217), 0.3f);
        cg.DOFade(1f, 0.3f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bg.rectTransform.DOSizeDelta(new Vector2(425, 247), 0.3f);
        cg.DOFade(0.9f, 0.3f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bg.rectTransform.DOSizeDelta(new Vector2(435, 257), 0.2f);
        cg.DOFade(0.7f, 0.2f);
    }

}
