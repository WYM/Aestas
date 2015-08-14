using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class GallerySoundtrackControl : MonoBehaviour {

    public Text playingTitle;
    public Text playingArtist;
    public AudioClip playingClip;

    public AudioSource bgm;

    void Start ()
    {
	    
	}
	
	void Update ()
    {
	
	}

    public void PlayNew(string title, string artist, string clip)
    {
        playingTitle.text = title;
        playingArtist.text = artist;

        bgm.Stop();
        bgm.clip = ResourcesOperator.LoadAudioClip(G.bgmPath + clip);
        bgm.Play();
    }
}
