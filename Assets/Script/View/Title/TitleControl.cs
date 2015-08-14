using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using DG.Tweening;

public class TitleControl : MonoBehaviour {
    
    public GameObject blackMask;
    public GameObject whiteMask;
    public GameObject fxCamera;
    public AudioSource bgm;

    public Image c1;
    public Image c2;
    public Image c3;

    void Start ()
    {
        blackMask.SetActive(true);
        StartTitle();

        ChangeCharacter();
        c1.CrossFadeAlpha(0, 0, true);
        c2.CrossFadeAlpha(0, 0, true);
        c3.CrossFadeAlpha(0, 0, true);
    }
	
	void Update ()
    {
	
	}

    public void StartTitle()
    {

        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            blackMask.GetComponent<Image>().DOFade(0, 0.8f);
            bgm.Play();
        }, 0.5f));

        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            blackMask.SetActive(false);
        }, 1.4f));

        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            c1.CrossFadeAlpha(1, 1f, true);
        }, 1.5f));

        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            c2.CrossFadeAlpha(1, 1f, true);
        }, 2f));

        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            c3.CrossFadeAlpha(1, 1f, true);
        }, 2.5f));
    }

    public void ChangeCharacter()
    {
        System.Random r = new System.Random();
        int rKey = r.Next(1, 7);
        int rKey2 = r.Next(1, 5);

        //TODO: 更加丰富的显示种类（如：只显示欧阳羽病号服）
        switch (rKey)
        {
            case 1:
            case 2:
            case 3:
                c1.overrideSprite = ResourcesOperator.LoadSprite(G.titleCharacterPath + "xk" + rKey.ToString());
                c2.overrideSprite = ResourcesOperator.LoadSprite(G.titleCharacterPath + "oyy" + rKey.ToString());
                c3.overrideSprite = ResourcesOperator.LoadSprite(G.titleCharacterPath + "sy" + rKey.ToString());
                break;
            case 4:
                c1.gameObject.SetActive(false);
                c3.gameObject.SetActive(false);
                c2.rectTransform.DOLocalMoveX(210, 0);
                c2.overrideSprite = ResourcesOperator.LoadSprite(G.titleCharacterPath + "xk" + rKey2.ToString());
                break;
            case 5:
                c1.gameObject.SetActive(false);
                c3.gameObject.SetActive(false);
                c2.rectTransform.DOLocalMoveX(210, 0);
                c2.overrideSprite = ResourcesOperator.LoadSprite(G.titleCharacterPath + "oyy" + rKey.ToString());
                break;
            case 6:
                c1.gameObject.SetActive(false);
                c3.gameObject.SetActive(false);
                c2.rectTransform.DOLocalMoveX(210, 0);
                c2.overrideSprite = ResourcesOperator.LoadSprite(G.titleCharacterPath + "sy" + rKey2.ToString());
                break;
            case 7:
                Debug.Log("Error 7 !!!!");
                break;
            default:
                break;
        }


    }
}
