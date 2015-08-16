using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class GameControlButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{

    public Image bg;

    public bool startWithActive = false;
    bool isActive = false;

    float durationIn = 0.2f;
    float durationOut = 0.3f;

	void Start ()
    {
        bg.DOFade(0.1f, 0);
        
        if (startWithActive)
        {
            Click();
        }
	}
	
	void Update ()
    {
	
	}

    void Click()
    {
        if (isActive)
        {
            isActive = false;
            bg.DOFade(0.1f, durationIn);
        }
        else
        {
            isActive = true;
            bg.DOFade(0.8f, durationOut);
        }
    }

    public void OnPointerClick(PointerEventData e)
    {
        Click();
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (isActive) return;
        bg.DOFade(0.8f, durationIn);
    }

    public void OnPointerExit(PointerEventData e)
    {
        if (isActive) return;
        bg.DOFade(0.1f, durationOut);

    }

    public void OnPointerUp(PointerEventData e)
    {
        if (isActive) return;
        bg.DOFade(0.8f, durationOut);

    }
    public void OnPointerDown(PointerEventData e)
    {
        if (isActive) return;
        bg.DOFade(1f, durationIn);

    }
}
