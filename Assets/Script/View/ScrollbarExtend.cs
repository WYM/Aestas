using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ScrollbarExtend : MonoBehaviour{

    public GameObject handle;

    bool isScrollBarHide = false;

    Scrollbar scrollBar;
    float scrollFadeTimer;

	// Use this for initialization
	void Start ()
    {
        scrollBar = this.GetComponent<Scrollbar>(); //获取当前对象上的 ScrollBar 组件
        scrollBar.value = 1; //设定滚动条的初始值为1，即位于最顶端（不设定时自动为0.5）
	}
	
	// Update is called once per frame
	void Update ()
    {
        scrollFadeTimer += Time.deltaTime;

        //滚动条显示1秒后自动消失
        if (scrollFadeTimer > 1 && isScrollBarHide == false)
        {
            HideScrollbar();
        }

        //每10秒显示一次被隐藏的滚动条，提示用户可滚动
        if (scrollFadeTimer > 10 && isScrollBarHide == true)
        {
            ShowScrollbar();
        }
    }

    void OnGUI()
    {

    }

    public void OnValueChanged()
    {
        ShowScrollbar();
    }

    //隐藏滚动条并设定 isScrollBarHide 为假
    public void HideScrollbar()
    {
        Debug.Log("Hide");
        if (!isScrollBarHide) handle.GetComponent<Image>().CrossFadeAlpha(0, 0.5f, true);
        isScrollBarHide = true;
    }

    //显示滚动条并设定 isScrollBarHide 为真
    public void ShowScrollbar()
    {
        Debug.Log("Show");
        scrollFadeTimer = 0;
        if (isScrollBarHide) handle.GetComponent<Image>().CrossFadeAlpha(1, 0.5f, true);
        isScrollBarHide = false;
    }
}
